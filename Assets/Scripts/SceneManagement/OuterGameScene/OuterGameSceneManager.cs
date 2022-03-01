using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OuterGameSceneManager : MonoBehaviour
{
    #region Singleton
    private static OuterGameSceneManager _instance;

    public static OuterGameSceneManager Instance => _instance;

    private OuterGameSceneManager() { }
    #endregion Singleton

    #region Variables
    int nowTeam;

    [Header("Upper Bar")]
    [SerializeField] TextMeshProUGUI upperBarTeamNameText;
    [SerializeField] TextMeshProUGUI upperBarDateText;

    [Header("선수단")]
    [SerializeField] Transform myPlayersListWindow;
    [SerializeField] GameObject myPlayersListItemPrefab;
    Transform myPlayersListWindowContent;

    [Header("선수 영입")]
    [SerializeField] Transform scoutWindow;
    [SerializeField] GameObject scoutItemPrefab;
    [SerializeField] TMP_InputField scoutWindowPagingInputField;
    [SerializeField] TextMeshProUGUI scoutWindowMaxPageText;
    int scoutWindowNowPage = 1;
    int scoutWindowMaxPage = 1;
    const int SCOUT_WINDOW_ITEM_COUNT = 12;
    Player[] independentPlayers;

    [Header("팀 목록")]
    [SerializeField] Transform teamListWindow;
    [SerializeField] GameObject teamListItemPrefab;

    [Header("리그")]
    [SerializeField] Transform leagueWindow;
    #endregion Variables

    #region UnityFunctions
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }

        myPlayersListWindowContent = myPlayersListWindow.Find("Viewport").Find("Content");
    }

    private void Start()
    {
        MyTeamReload();

        GetIndependentPlayersArray();
        CalculateScoutWindowMaxPage();
    }
    #endregion UnityFunctions

    #region PageReload
    public void MyTeamReload()
    {
        nowTeam = GameManager.Instance.Managers[0].Team;

        CloseAllWindow();

        UpperBarUpdate();
        SideBarUpdate();
    }

    public void SetNowTeamAndReload(int teamId)
    {
        nowTeam = teamId;
        
        CloseAllWindow();
        
        UpperBarUpdate();
        SideBarUpdate();
    }

    void UpperBarUpdate()
    {
        switch (nowTeam)
        {
            case -1:
                upperBarTeamNameText.text = "소속팀 없음";
                break;
            default:
                upperBarTeamNameText.text = GameManager.Instance.Teams[nowTeam].Name;
                break;
        }
        upperBarDateText.text = GameManager.Instance.NowDate.ToString();
    }

    void SideBarUpdate()
    {
        switch (nowTeam)
        {
            case -1:
                break;
            default:
                break;
        }
        //버튼들 끄고 켜기
    }

    void CloseAllWindow()
    {
        myPlayersListWindow.gameObject.SetActive(false);
        scoutWindow.gameObject.SetActive(false);
        teamListWindow.gameObject.SetActive(false);
        leagueWindow.gameObject.SetActive(false);
    }
    #endregion PageReload

    #region MyPlayersList
    public void ActivateMyPlayersListWindow()
    {
        CloseAllWindow();

        DeleteAllItemsInMyPlayersListWindow();

        myPlayersListWindow.gameObject.SetActive(true);

        int playerCount = GameManager.Instance.Teams[nowTeam].Players.Count;

        //Content 크기 조정 //(기본크기) - (여백 + 아이템크기) * (계수) - (여백)
        int yOffset = 747 - (15 + 168) * ((playerCount - 1) / 3) - 15;
        myPlayersListWindowContent.GetComponent<RectTransform>().offsetMin = new(0, yOffset);

        for(int i = 0; i < playerCount; i++)
        {
            //선수 가져오기
            Player player = GameManager.Instance.Players[GameManager.Instance.Teams[nowTeam].Players[i]];
            
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

    void DeleteAllItemsInMyPlayersListWindow()
    {
        Transform[] allItems = myPlayersListWindow.GetComponentsInChildren<Transform>();
        foreach (Transform t in allItems)
        {
            if (t.CompareTag("MyPlayersListWindow")) continue;
            Destroy(t.gameObject);
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

    void DeleteAllItemsInScoutWindow()
    {
        Transform[] allItems = scoutWindow.GetComponentsInChildren<Transform>();
        foreach (Transform t in allItems)
        {
            if (t.CompareTag("ScoutWindow")) continue;
            Destroy(t.gameObject);
        }
    }

    void RefreshScoutWindow()
    {
        DeleteAllItemsInScoutWindow();

        for(int i = 0 + (SCOUT_WINDOW_ITEM_COUNT * (scoutWindowNowPage - 1)); i < Mathf.Min((SCOUT_WINDOW_ITEM_COUNT * scoutWindowNowPage), independentPlayers.Length); i++)
        {
            Player player = independentPlayers[i];

            GameObject item = Instantiate(scoutItemPrefab);
            item.GetComponent<ScoutItemClickHandler>().player = player;

            Transform itemTransform = item.transform;
            itemTransform.SetParent(scoutWindow);

            //선수 정보 기입
            itemTransform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = player.Name;

            //아이템 위치 조정
            itemTransform.GetComponent<RectTransform>().anchoredPosition = new(15 + (15 + 390) * (i % 3), -15 - (15 + 168) * (i % 12 / 3));
        }
        scoutWindowPagingInputField.text = scoutWindowNowPage.ToString();
        scoutWindowPagingInputField.placeholder.GetComponent<TextMeshProUGUI>().text = scoutWindowNowPage.ToString();
    }

    public void ScoutWindowPageChangeByInputField()
    {
        int temp = scoutWindowNowPage;
        if (int.TryParse(scoutWindowPagingInputField.text, out scoutWindowNowPage))
        {
            ScoutWindowNowPageClearing();
            scoutWindowPagingInputField.text = scoutWindowNowPage.ToString();
            scoutWindowPagingInputField.placeholder.GetComponent<TextMeshProUGUI>().text = scoutWindowNowPage.ToString();
            RefreshScoutWindow();
        }
        else
        {
            scoutWindowNowPage = temp;
        }
    }

    public void ScoutWindowPageChangeByButton(int i)
    {
        scoutWindowNowPage += i; // PrevButton => i = -1, NextButton => i = 1
        ScoutWindowNowPageClearing();
        scoutWindowPagingInputField.text = scoutWindowNowPage.ToString();
        scoutWindowPagingInputField.placeholder.GetComponent<TextMeshProUGUI>().text = scoutWindowNowPage.ToString();
        RefreshScoutWindow();
    }

    void ScoutWindowNowPageClearing()
    {
        if (scoutWindowNowPage < 1) scoutWindowNowPage = 1;
        else if (scoutWindowNowPage > scoutWindowMaxPage) scoutWindowNowPage = scoutWindowMaxPage;
    }
    #endregion ScoutWindow

    #region TeamList
    public void ActivateTeamListWindow()
    {
        CloseAllWindow();

        DeleteAllItemsInTeamListWindow();

        teamListWindow.gameObject.SetActive(true);

        List<Team> teamList = new(GameManager.Instance.Teams.Values);

        for (int i = 0; i < teamList.Count; i++)
        {
            Team team = teamList[i];

            GameObject item = Instantiate(teamListItemPrefab);
            item.GetComponent<TeamListItemClickHandler>().team = team;
            item.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = team.Name;
            item.transform.SetParent(teamListWindow.GetChild(0).GetChild(0));
        }
    }

    void DeleteAllItemsInTeamListWindow()
    {
        Transform[] allItems = teamListWindow.GetComponentsInChildren<Transform>();
        foreach (Transform t in allItems)
        {
            if (t.CompareTag("TeamListWindow")) continue;
            Destroy(t.gameObject);
        }
    }
    #endregion TeamList

    #region League
    public void ActivateLeagueWindow()
    {
        CloseAllWindow();

        leagueWindow.gameObject.SetActive(true);
    }
    #endregion League
}
