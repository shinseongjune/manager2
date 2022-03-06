using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

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
    [SerializeField] GameObject leagueItemPrefab;

    [Header("����")]
    [SerializeField] Transform calendarWindow;
    [SerializeField] GameObject calendarItemPrefab;
    public int calendarNowYear = GameManager.Instance.NowDate.Year;

    [Header("�ý���������")]
    [SerializeField] Transform systemWindow;

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
        SetMyTeamAndReload();

        GetIndependentPlayersArray();
        CalculateScoutWindowMaxPage();
    }
    #endregion UnityFunctions

    #region PageReload
    public void SetMyTeamAndReload()
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
        //TODO: ��ư�� ���� �ѱ�
    }

    void CloseAllWindow()
    {
        myPlayersListWindow.gameObject.SetActive(false);
        scoutWindow.gameObject.SetActive(false);
        teamListWindow.gameObject.SetActive(false);
        leagueWindow.gameObject.SetActive(false);
        calendarWindow.gameObject.SetActive(false);
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

        DeleteAllItemsInLeagueWindow();

        leagueWindow.gameObject.SetActive(true);

        List<League> leagueList = new(GameManager.Instance.Leagues.Values);

        for (int i = 0; i < leagueList.Count; i++)
        {
            League league = leagueList[i];

            GameObject item = Instantiate(leagueItemPrefab);
            item.GetComponent<LeagueItemClickHandler>().league = league;
            item.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = league.Name;
            item.transform.SetParent(leagueWindow.GetChild(0).GetChild(0));
        }
    }

    void DeleteAllItemsInLeagueWindow()
    {
        Transform[] allItems = leagueWindow.GetComponentsInChildren<Transform>();
        foreach (Transform t in allItems)
        {
            if (t.CompareTag("LeagueWindow")) continue;
            Destroy(t.gameObject);
        }
    }
    #endregion League

    #region Calendar
    public void ActivateCalendarWindow()
    {
        CloseAllWindow();

        DeleteAllItemsInCalendarWindow();

        calendarNowYear = GameManager.Instance.NowDate.Year;
        calendarWindow.gameObject.SetActive(true);

        WriteYearInCalendarWindow();

        MakeCalendarItems();
    }

    void WriteYearInCalendarWindow()
    {
        calendarWindow.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = calendarNowYear + "��";
    }

    void MakeCalendarItems()
    {
        Transform CalendarEmptyObject = calendarWindow.GetChild(1);
        for (int i = 1; i < 13; i++)
        {
            GameObject item = Instantiate(calendarItemPrefab);
            CalendarItemClickHandler handler = item.GetComponent<CalendarItemClickHandler>();
            handler.month = i;
            foreach (Match m in GameManager.Instance.Matches.Values)
            {
                if (m.DDay.Year > calendarNowYear) break;
                if (m.DDay.Year == calendarNowYear)
                {
                    if (m.DDay.Month > i) break;
                    if (m.DDay.Month == i)
                    {
                        handler.matches.Add(m.IDNumber);
                    }
                }
            }
            item.GetComponent<CalendarItemContentsWriter>().WriteContents();
            item.transform.SetParent(CalendarEmptyObject);
        }
    }

    void DeleteAllItemsInCalendarWindow()
    {
        Transform[] allItems = calendarWindow.GetComponentsInChildren<Transform>();
        foreach (Transform t in allItems)
        {
            if (t.CompareTag("CalendarWindow")) continue;
            Destroy(t.gameObject);
        }
    }

    public void ClickButtonInCalendarWindow(int i)
    {
        switch (i) // PrevYearButton => i = -1, NextYearButton => i = 1
        {
            case 1:
                if (calendarNowYear >= GameManager.Instance.NowDate.Year + 1) return;
                calendarNowYear += 1;
                WriteYearInCalendarWindow();
                DeleteAllItemsInCalendarWindow();
                MakeCalendarItems();
                break;
            case -1:
                if (calendarNowYear <= GameManager.Instance.NowDate.Year) return;
                calendarNowYear -= 1;
                WriteYearInCalendarWindow();
                DeleteAllItemsInCalendarWindow();
                MakeCalendarItems();
                break;
        }
    }
    #endregion Calendar

    #region DateProgress
    public void DateProgress()
    {
        GameManager.Instance.NowDate++;

        //TODO:��������,����,�������,���Լ��� ���
        //TODO:�ű���,����ü ���
        //TODO:�űԸ���, ������ü ���
        //TODO:��Ⱑ ���� ���->��� ����->����ݿ�
        TextMeshProUGUI systemText = systemWindow.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        systemText.text = "";
        foreach (Match m in GameManager.Instance.Matches.Values)
        {
            if (!(m.DDay == GameManager.Instance.NowDate))
            {
                continue;
            }
            int winTeam = GamePlay();
            systemWindow.gameObject.SetActive(true);
            switch (winTeam)
            {
                case 0:
                    systemText.text += GameManager.Instance.Leagues[m.League].Name + " ���� : " + GameManager.Instance.Teams[m.Team1].Name + " (vs " + GameManager.Instance.Teams[m.Team2].Name + ")\n";
                    break;
                case 1:
                    systemText.text += GameManager.Instance.Leagues[m.League].Name + " ���� : " + GameManager.Instance.Teams[m.Team2].Name + " (vs " + GameManager.Instance.Teams[m.Team1].Name + ")\n";
                    break;
            }
        }

        //TODO:�ذ� �Ѿ�� ���� �� ��� ����? / ���� ���� ���

        //TODO:��Ÿ �ʿ��Ѱ�

        SetMyTeamAndReload();
    }

    int GamePlay()
    {
        int random = Random.Range(0, 2);
        return random;
    }
    #endregion DateProgress
}
