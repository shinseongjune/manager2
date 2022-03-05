using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class NewGameSceneManager : MonoBehaviour
{
    #region Singleton
    static NewGameSceneManager _instance;

    public static NewGameSceneManager Instance => _instance;

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
    }
    #endregion Singleton

    #region Variables
    [Header("���� ����â")]
    [SerializeField] GameObject makingManagerWindow;
    [SerializeField] TMP_InputField userNameInputField;
    [SerializeField] TextMeshProUGUI userNameText;

    [Header("�� ����â")]
    [SerializeField] GameObject makingTeamWindow;
    [SerializeField] TMP_InputField teamNameInputField;
    [SerializeField] TextMeshProUGUI teamNameText;

    [Header("���� ����â")]
    [SerializeField] GameObject selectPlayerWindow;
    [SerializeField] Transform playerCard1;
    [SerializeField] Transform playerCard2;
    [SerializeField] Transform playerCard3;

    readonly Player[] players = new Player[3];
    const int INDEPENDENT_PLAYER_COUNT = 100;

    readonly string[] teamNamePrefix = { "�뱸", "����", "��õ", "�巷ū", "��ĭ�𳪺��" };
    readonly string[] teamNameSuffix = { "���̿���", "���", "Ÿ�̰���", "��������", "���ϴ�" };
    readonly string[] playerNamePrefix = { "��", "��", "��", "��", "����", "Ź", "ȫ", "��", "����", "��", "��", "��", "��" };
    readonly string[] playerNameSuffix = { "�¼�", "����", "ȫ��", "�", "����", "Ź", "���ӽ�", "����", "����", "�μ�", "����", "÷��", "����", "�巰��" };
    readonly string[] leagueNamePrefix = { "GA", "���������", "���ְ��׹�", "������̵�", "�׷���", "����", "��������", "LA", "����", "õ������", "����" };
    readonly string[] leagueNameSuffix = { "���θ���", "�ڸ�����", "��Ʋ�׶���", "���̸��� ��", "����¯", "���Ӹ���", "���۽�Ÿ��", "���ΰ��� ç����", "�� ���̹�", "���� �����", "����Ʈ����" };

    readonly List<string> composedTeamNames = new();
    readonly List<string> composedLeagueNames = new();
    #endregion Variables

    #region UnityFunctions
    private void Start()
    {
        GameManager.Instance.Initialize();
    }

    private void OnGUI()
    {
        userNameInputField.characterLimit = 5;
        teamNameInputField.characterLimit = 8;

        userNameInputField.onValueChanged.AddListener((c) => userNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z��-�R]", ""));
        teamNameInputField.onValueChanged.AddListener((c) => teamNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z��-�R]", ""));
    }
    #endregion UnityFunctions

    public void MakeUserAndMakingTeamWindowActive()
    {
        string userName = userNameText.text;
        userName = userName[0..^1];

        if (userName.Length < 1) return;
        
        Maker.MakeManager(userName);
        
        makingManagerWindow.SetActive(false);
        makingTeamWindow.SetActive(true);
    }

    public void MakeTeamAndSelectPlayerWindowActive(int selectedPlayerId)
    {
        string teamName = teamNameText.text;
        teamName = teamName[0..^1];

        if (teamName.Length < 1) return;

        composedTeamNames.Add(teamName);
        Team t = Maker.MakeTeam(teamName);
        GameManager.Instance.Managers[0].Team = t.IDNumber;

        makingTeamWindow.SetActive(false);
        selectPlayerWindow.SetActive(true);
        RefreshSelectPlayerWindow();
    }

    public void MakeRandomPlayers()
    {
        string composedPlayerName = playerNamePrefix[Random.Range(0, playerNamePrefix.Length)] + playerNameSuffix[Random.Range(0, playerNameSuffix.Length)];
        players[0] = Maker.MakePlayer(composedPlayerName);
        composedPlayerName = playerNamePrefix[Random.Range(0, playerNamePrefix.Length)] + playerNameSuffix[Random.Range(0, playerNameSuffix.Length)];
        players[1] = Maker.MakePlayer(composedPlayerName);
        composedPlayerName = playerNamePrefix[Random.Range(0, playerNamePrefix.Length)] + playerNameSuffix[Random.Range(0, playerNameSuffix.Length)];
        players[2] = Maker.MakePlayer(composedPlayerName);
    }

    public void RefreshSelectPlayerWindow()
    {
        if(GameManager.Instance.Teams[0].Players.Count >= 6)
        {
            selectPlayerWindow.SetActive(false);

            MakeExtraTeams();
            MakeIndependentPlayers();
            MakeLeagues();

            LoadOuterGameScene();
        }
        else
        {
            MakeRandomPlayers();
            playerCard1.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[0].Name;
            playerCard2.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[1].Name;
            playerCard3.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[2].Name;
        
            playerCard1.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[0].Age + "��";
            playerCard2.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[1].Age + "��";
            playerCard3.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[2].Age + "��";

            playerCard1.GetComponent<PlayerCardClickHandler>().player = players[0];
            playerCard2.GetComponent<PlayerCardClickHandler>().player = players[1];
            playerCard3.GetComponent<PlayerCardClickHandler>().player = players[2];
        }
    }

    void MakeLeagues()
    {
        for (int i = 0; i < 2; i++)
        {
            string composedLeagueName = leagueNamePrefix[Random.Range(0, leagueNamePrefix.Length)] + " " + leagueNameSuffix[Random.Range(0, leagueNameSuffix.Length)];
            if (composedLeagueNames.Contains(composedLeagueName))
            {
                i--;
                continue;
            }
            composedLeagueNames.Add(composedLeagueName);

            int entryMin = Random.Range(4, 5);
            int entryMax = Random.Range(6, 11);
            int leagueSystem = Random.Range(0, 2); //TODO: ������ ����� �ٲٱ� // �� �� �ٲ�ߵɵ�
            int playOffSystem = Random.Range(0, 2);
            League league = Maker.MakeLeague(composedLeagueName, entryMin, entryMax,  (LeagueSystem)leagueSystem, (LeagueSystem)playOffSystem, (playOffSystem == 3 ? 0:3));
            league.SetStartDate(Random.Range(8, 40));

            List<int> tempTeams = new(GameManager.Instance.Teams.Keys);
            tempTeams.Shuffle();
            int maxJ = Random.Range(entryMin + 1, Mathf.Min(entryMax + 1, tempTeams.Count)) - 1;
            for (int j = 0; j < maxJ; j++)
            {
                league.Entry.Add(tempTeams[j]);
            }
            if (!league.Entry.Contains(0)) league.Entry.Add(0);

            Maker.MakeAllMatches(league.IDNumber);
        }
    }

    void MakeExtraTeams()
    {
        for (int i = 0; i < 8; i++)
        {
            string composedTeamName = teamNamePrefix[Random.Range(0, teamNamePrefix.Length)] + teamNameSuffix[Random.Range(0, teamNameSuffix.Length)];
            if (composedTeamNames.Contains(composedTeamName))
            {
                i--;
                continue;
            }
            composedTeamNames.Add(composedTeamName);

            Team team = Maker.MakeTeam(composedTeamName);

            string composedManagerName = playerNamePrefix[Random.Range(0, playerNamePrefix.Length)] + playerNameSuffix[Random.Range(0, playerNameSuffix.Length)];
            Manager manager = Maker.MakeManager(composedManagerName);
            team.EmployManager(manager.IDNumber);
            manager.SetTeam(team.IDNumber);

            int playerCount = Random.Range(6, 9);
            for (int j = 0; j < playerCount; j++) {
                string composedPlayerName = playerNamePrefix[Random.Range(0, playerNamePrefix.Length)] + playerNameSuffix[Random.Range(0, playerNameSuffix.Length)];
                Player player = Maker.MakePlayer(composedPlayerName);
                Contract contract = Maker.MakeContract(team.IDNumber, player.IDNumber, new(Random.Range(2024, 2028), 12, 4), Random.Range(300, 600));
                player.AddContract(contract.IDNumber);
                team.AddContract(contract.IDNumber);
                team.AddPlayer(player.IDNumber);
            }
        }
    }

    void MakeIndependentPlayers()
    {
        for(int i = 0; i < INDEPENDENT_PLAYER_COUNT; i++)
        {
            string composedPlayerName = playerNamePrefix[Random.Range(0, playerNamePrefix.Length)] + playerNameSuffix[Random.Range(0, playerNameSuffix.Length)];
            Maker.MakePlayer(composedPlayerName);
        }
    }

    void LoadOuterGameScene()
    {
        SceneManager.LoadScene("OuterGameScene");
    }
}
