using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OuterGameSceneManager : MonoBehaviour
{
    #region Singleton
    private static readonly Lazy<OuterGameSceneManager> _instance = new(() => new OuterGameSceneManager());

    public static OuterGameSceneManager Instance
    {
        get
        {
            return _instance.Value;
        }
    }

    private OuterGameSceneManager() { }
    #endregion Singleton

    #region Variables
    [Header("Upper Bar")]
    [SerializeField] TextMeshProUGUI upperBarTeamNameText;
    [SerializeField] TextMeshProUGUI upperBarDateText;

    [Header("선수단")]
    [SerializeField] Transform myPlayersListWindow;
    [SerializeField] GameObject myPlayersListItemPrefab;
    Transform myPlayersListWindowContent;

    [Header("선수 영입")]
    [SerializeField] Transform scoutWindow;
    [SerializeField] TMP_InputField scoutWindowInputField;
    [SerializeField] TextMeshProUGUI scoutWindowMaxPageText;
    int scoutWindowNowPage = 1;
    int scoutWindowMaxPage = 1;
    const int SCOUT_WINDOW_ITEM_COUNT = 12;
    Player[] independentPlayers;
    #endregion Variables

    #region Properties

    #endregion Properties

    private void Awake()
    {
        myPlayersListWindowContent = myPlayersListWindow.Find("Viewport").Find("Content");
    }

    private void Start()
    {
        UpperBarUpdate();

        GetIndependentPlayersArray();
        CalculateScoutWindowMaxPage();
    }

    void UpperBarUpdate()
    {
        upperBarTeamNameText.text = GameManager.Instance.Teams[GameManager.Instance.Managers[0].Team].Name;
        upperBarDateText.text = GameManager.Instance.NowDate.ToString();
    }

    void CloseAllWindow()
    {
        myPlayersListWindow.gameObject.SetActive(false);
        scoutWindow.gameObject.SetActive(false);
    }

    #region MyPlayersList
    public void ActivateMyPlayersListWindow()
    {
        CloseAllWindow();
        myPlayersListWindow.gameObject.SetActive(true);
        
        Manager user = GameManager.Instance.Managers[0];
        Team userTeam = GameManager.Instance.Teams[user.Team];
        int playerCount = userTeam.Players.Count;

        //Content 크기 조정 //(기본크기) - (여백 + 아이템크기) * (계수) - (여백)
        int yOffset = 747 - (15 + 168) * ((playerCount - 1) / 3) - 15;
        myPlayersListWindowContent.GetComponent<RectTransform>().offsetMin = new(0, yOffset);

        for(int i = 0; i < playerCount; i++)
        {
            //선수 가져오기
            Player player = GameManager.Instance.Players[userTeam.Players[i]];
            
            //선수 넣을 아이템 생성 및 부모 설정
            GameObject myPlayersListItem = Instantiate(myPlayersListItemPrefab);
            Transform itemTransform = myPlayersListItem.transform;
            itemTransform.SetParent(myPlayersListWindowContent);

            //선수 정보 기입
            itemTransform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = player.Name;
            itemTransform.Find("PlayerAgeText").GetComponent<TextMeshProUGUI>().text = player.Age + "세";

            //클릭 핸들러에 선수 등록
            MyPlayersListItemClickHandler clickHandler = myPlayersListItem.GetComponent<MyPlayersListItemClickHandler>();
            clickHandler.player = player;

            //아이템 위치 조정
            itemTransform.GetComponent<RectTransform>().anchoredPosition = new(15 + (15 + 390) * (i % 3), -15 - (15 + 168) * (i / 3));
        }
    }
    #endregion MyPlayersList

    #region ScoutWindow
    void CalculateScoutWindowMaxPage()
    {
        scoutWindowMaxPage = independentPlayers.Length;
        if (scoutWindowMaxPage % SCOUT_WINDOW_ITEM_COUNT == 0)
        {
            scoutWindowMaxPage /= SCOUT_WINDOW_ITEM_COUNT;
        }
        else
        {
            scoutWindowMaxPage /= SCOUT_WINDOW_ITEM_COUNT;
            scoutWindowMaxPage++;
        }

        scoutWindowMaxPageText.text = "/" + scoutWindowMaxPage;
    }

    public void ActivateScoutWindow()
    {
        CloseAllWindow();
        scoutWindow.gameObject.SetActive(true);
        scoutWindowNowPage = 1;
        RefreshScoutWindow();
    }

    void GetIndependentPlayersArray()
    {
        Dictionary<int, Player> independentPlayersDictionary = new(GameManager.Instance.Players);

        foreach (Team team in GameManager.Instance.Teams.Values)
        {
            foreach (int playerId in team.Players)
            {
                independentPlayersDictionary.Remove(playerId);
            }
        }

        independentPlayers = new Player[independentPlayersDictionary.Count];
        independentPlayersDictionary.Values.CopyTo(independentPlayers, 0);
    }

    void RefreshScoutWindow()
    {
        Transform[] allItems = scoutWindow.GetComponentsInChildren<Transform>();
        foreach (Transform t in allItems)
        {
            if (t.CompareTag("ScoutWindow")) continue;
            Destroy(t.gameObject);
        }

        for(int i = 0 + (SCOUT_WINDOW_ITEM_COUNT * (scoutWindowNowPage - 1)); i < Mathf.Min((SCOUT_WINDOW_ITEM_COUNT * scoutWindowNowPage), independentPlayers.Length); i++)
        {
            Player player = independentPlayers[i];

            GameObject item = Instantiate(myPlayersListItemPrefab);
            item.GetComponent<MyPlayersListItemClickHandler>().player = player;

            Transform itemTransform = item.transform;
            itemTransform.SetParent(scoutWindow);

            //선수 정보 기입
            itemTransform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = player.Name;
            itemTransform.Find("PlayerAgeText").GetComponent<TextMeshProUGUI>().text = player.Age + "세";

            //아이템 위치 조정
            itemTransform.GetComponent<RectTransform>().anchoredPosition = new(15 + (15 + 390) * (i % 3), -15 - (15 + 168) * (i % 12 / 3));
        }
        scoutWindowInputField.text = scoutWindowNowPage.ToString();
        scoutWindowInputField.placeholder.GetComponent<TextMeshProUGUI>().text = scoutWindowNowPage.ToString();
    }

    public void ScoutWindowPageChangeByInputField()
    {
        scoutWindowNowPage = int.Parse(scoutWindowInputField.text);
        ScoutWindowNowPageClearing();
        scoutWindowInputField.text = scoutWindowNowPage.ToString();
        scoutWindowInputField.placeholder.GetComponent<TextMeshProUGUI>().text = scoutWindowNowPage.ToString();
        RefreshScoutWindow();
    }

    public void ScoutWindowPageChangeByButton(int i)
    {
        scoutWindowNowPage += i; // PrevButton => i = -1, NextButton => i = 1
        ScoutWindowNowPageClearing();
        RefreshScoutWindow();
    }

    void ScoutWindowNowPageClearing()
    {
        if (scoutWindowNowPage < 1) scoutWindowNowPage = 1;
        else if (scoutWindowNowPage > scoutWindowMaxPage) scoutWindowNowPage = scoutWindowMaxPage;
    }
    #endregion ScoutWindow
}
