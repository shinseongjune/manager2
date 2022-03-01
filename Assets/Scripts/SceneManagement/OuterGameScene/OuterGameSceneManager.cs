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

    [Header("������")]
    [SerializeField] Transform myPlayersListWindow;
    [SerializeField] GameObject myPlayersListItemPrefab;
    Transform myPlayersListWindowContent;

    [Header("���� ����")]
    [SerializeField] Transform scoutWindow;
    [SerializeField] GameObject scoutItemPrefab;
    [SerializeField] TMP_InputField scoutWindowPagingInputField;
    [SerializeField] TextMeshProUGUI scoutWindowMaxPageText;
    int scoutWindowNowPage = 1;
    int scoutWindowMaxPage = 1;
    const int SCOUT_WINDOW_ITEM_COUNT = 12;
    Player[] independentPlayers;

    [Header("�� ���")]
    [SerializeField] Transform teamListWindow;
    [SerializeField] GameObject teamListItemPrefab;

    [Header("����")]
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
                upperBarTeamNameText.text = "�Ҽ��� ����";
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
        //��ư�� ���� �ѱ�
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

        //Content ũ�� ���� //(�⺻ũ��) - (���� + ������ũ��) * (���) - (����)
        int yOffset = 747 - (15 + 168) * ((playerCount - 1) / 3) - 15;
        myPlayersListWindowContent.GetComponent<RectTransform>().offsetMin = new(0, yOffset);

        for(int i = 0; i < playerCount; i++)
        {
            //���� ��������
            Player player = GameManager.Instance.Players[GameManager.Instance.Teams[nowTeam].Players[i]];
            
            //���� ���� ������ ���� �� �θ� ����
            GameObject myPlayersListItem = Instantiate(myPlayersListItemPrefab);
            Transform itemTransform = myPlayersListItem.transform;
            itemTransform.SetParent(myPlayersListWindowContent);

            //���� ���� ����
            itemTransform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = player.Name;
            itemTransform.Find("PlayerAgeText").GetComponent<TextMeshProUGUI>().text = player.Age + "��";

            //Ŭ�� �ڵ鷯�� ���� ���
            MyPlayersListItemClickHandler clickHandler = myPlayersListItem.GetComponent<MyPlayersListItemClickHandler>();
            clickHandler.player = player;

            //������ ��ġ ����
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

            //���� ���� ����
            itemTransform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = player.Name;

            //������ ��ġ ����
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
