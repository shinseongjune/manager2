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

    Manager user;
    Team team;
    Player[] players = new Player[3];
    #endregion Variables

    private void Start()
    {
        GameManager.Instance.Initialize();
    }

    public void MakeUserAndMakingTeamWindowActive()
    {
        string userName = userNameText.text;
        userName = userName.Substring(0, userName.Length - 1);

        if (userName.Length < 1) return;
        
        user = Maker.MakeManager(userName);
        
        makingManagerWindow.SetActive(false);
        makingTeamWindow.SetActive(true);
    }

    public void MakeTeamAndSelectPlayerWindowActive(int selectedPlayerId)
    {
        string teamName = teamNameText.text;
        teamName = teamName.Substring(0, teamName.Length - 1);

        if (teamName.Length < 1) return;

        team = Maker.MakeTeam(teamName);

        makingTeamWindow.SetActive(false);
        selectPlayerWindow.SetActive(true);
        RefreshSelectPlayerWindow();
    }

    public void MakeRandomPlayers()
    {
        players[0] = Maker.MakePlayer("김첨지");
        players[1] = Maker.MakePlayer("박문수");
        players[2] = Maker.MakePlayer("닐드럭만");
    }

    public void RefreshSelectPlayerWindow()
    {
        if(team.Players.Count >= 6)
        {
            selectPlayerWindow.SetActive(false);

            SceneManager.LoadScene("OuterGameScene");
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

    private void OnGUI()
    {
        userNameInputField.characterLimit = 5;
        teamNameInputField.characterLimit = 8;

        userNameInputField.onValueChanged.AddListener((c) => userNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z가-힣]", ""));
        teamNameInputField.onValueChanged.AddListener((c) => teamNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z가-힣]", ""));
    }
}
