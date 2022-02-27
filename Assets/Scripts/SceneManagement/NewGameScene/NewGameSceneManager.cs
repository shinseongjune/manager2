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
    [Header("감독 생성창")]
    [SerializeField] GameObject makingManagerWindow;
    [SerializeField] TMP_InputField userNameInputField;
    [SerializeField] TextMeshProUGUI userNameText;

    [Header("팀 생성창")]
    [SerializeField] GameObject makingTeamWindow;
    [SerializeField] TMP_InputField teamNameInputField;
    [SerializeField] TextMeshProUGUI teamNameText;

    [Header("선수 선택창")]
    [SerializeField] GameObject selectPlayerWindow;
    [SerializeField] Transform playerCard1;
    [SerializeField] Transform playerCard2;
    [SerializeField] Transform playerCard3;

    readonly Player[] players = new Player[3];
    const int INDEPENDENT_PLAYER_COUNT = 100;

    readonly string[] teamNamePrefix = { "대구", "서울", "인천", "드렁큰", "스칸디나비안" };
    readonly string[] teamNameSuffix = { "라이온즈", "베어스", "타이거즈", "공벌레단", "로켓단" };
    readonly string[] playerNamePrefix = { "김", "이", "박", "오", "제갈", "탁", "홍", "존", "조나", "송", "임", "손", "닐" };
    readonly string[] playerNameSuffix = { "태수", "차돈", "홍수", "헨리", "공명", "탁", "제임스", "우현", "종엽", "인석", "전형", "첨지", "문수", "드럭만" };

    #endregion Variables

    private void Start()
    {
        GameManager.Instance.Initialize();
    }

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

        Maker.MakeTeam(teamName);

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
            LoadOuterGameScene();
        }
        else
        {
            MakeRandomPlayers();
            playerCard1.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[0].Name;
            playerCard2.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[1].Name;
            playerCard3.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[2].Name;
        
            playerCard1.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[0].Age + "세";
            playerCard2.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[1].Age + "세";
            playerCard3.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[2].Age + "세";

            playerCard1.GetComponent<PlayerCardClickHandler>().player = players[0];
            playerCard2.GetComponent<PlayerCardClickHandler>().player = players[1];
            playerCard3.GetComponent<PlayerCardClickHandler>().player = players[2];
        }
    }

    void MakeExtraTeams()
    {
        List<string> composedTeamNames = new();

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

    private void OnGUI()
    {
        userNameInputField.characterLimit = 5;
        teamNameInputField.characterLimit = 8;

        userNameInputField.onValueChanged.AddListener((c) => userNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z가-힣]", ""));
        teamNameInputField.onValueChanged.AddListener((c) => teamNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z가-힣]", ""));
    }
}
