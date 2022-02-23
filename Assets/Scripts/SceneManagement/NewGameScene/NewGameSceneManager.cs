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
    [Header("°¨µ¶ »ý¼ºÃ¢")]
    [SerializeField] GameObject makingManagerWindow;
    [SerializeField] TMP_InputField userNameInputField;
    [SerializeField] TextMeshProUGUI userNameText;

    [Header("ÆÀ »ý¼ºÃ¢")]
    [SerializeField] GameObject makingTeamWindow;
    [SerializeField] TMP_InputField teamNameInputField;
    [SerializeField] TextMeshProUGUI teamNameText;

    [Header("¼±¼ö ¼±ÅÃÃ¢")]
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
        GameManager.Instance.Players.Clear();
        GameManager.Instance.Teams.Clear();
        GameManager.Instance.Contracts.Clear();
        GameManager.Instance.Managers.Clear();
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
        players[0] = Maker.MakePlayer("±èÃ·Áö");
        players[1] = Maker.MakePlayer("¹Ú¹®¼ö");
        players[2] = Maker.MakePlayer("´Òµå·°¸¸");
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
        
            playerCard1.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[0].Age + "¼¼";
            playerCard2.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[1].Age + "¼¼";
            playerCard3.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[2].Age + "¼¼";

            playerCard1.GetComponent<PlayerCardClickHandler>().player = players[0];
            playerCard2.GetComponent<PlayerCardClickHandler>().player = players[1];
            playerCard3.GetComponent<PlayerCardClickHandler>().player = players[2];
        }
    }

    private void OnGUI()
    {
        userNameInputField.characterLimit = 5;
        teamNameInputField.characterLimit = 8;

        userNameInputField.onValueChanged.AddListener((c) => userNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z°¡-ÆR]", ""));
        teamNameInputField.onValueChanged.AddListener((c) => teamNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z°¡-ÆR]", ""));
    }
}
