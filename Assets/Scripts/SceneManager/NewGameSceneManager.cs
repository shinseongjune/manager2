using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class NewGameSceneManager : MonoBehaviour
{
    [Header("감독 생성창")]
    [SerializeField] GameObject MakingManagerWindow;
    [SerializeField] TMP_InputField userNameInputField;
    [SerializeField] TextMeshProUGUI userNameText;

    [Header("팀 생성창")]
    [SerializeField] GameObject MakingTeamWindow;
    [SerializeField] TMP_InputField teamNameInputField;
    [SerializeField] TextMeshProUGUI teamNameText;

    [Header("선수 선택창")]
    [SerializeField] GameObject SelectPlayerWindow;
    [SerializeField] Transform PlayerCard1;
    [SerializeField] Transform PlayerCard2;
    [SerializeField] Transform PlayerCard3;

    Manager user;
    Team team;
    Player[] players = new Player[3];

    public void MakeUserAndMakingTeamWindowActive()
    {
        string userName = userNameText.text;
        userName = userName.Substring(0, userName.Length - 1);

        if (userName.Length < 1) return;
        
        user = Maker.MakeManager(userName);
        GameManager.Instance.AddManager(user);
        
        MakingManagerWindow.SetActive(false);
        MakingTeamWindow.SetActive(true);
    }

    public void MakeTeamAndSelectPlayerWindowActive(int selectedPlayerId)
    {
        string teamName = teamNameText.text;
        teamName = teamName.Substring(0, teamName.Length - 1);

        if (teamName.Length < 1) return;

        team = Maker.MakeTeam(teamName);
        GameManager.Instance.AddTeam(team);

        MakingTeamWindow.SetActive(false);
        SelectPlayerWindow.SetActive(true);
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
        MakeRandomPlayers();
        PlayerCard1.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[0].Name;
        PlayerCard2.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[1].Name;
        PlayerCard3.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[2].Name;
        
        PlayerCard1.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[0].Age + "세";
        PlayerCard2.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[1].Age + "세";
        PlayerCard3.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[2].Age + "세";
    }

    private void OnGUI()
    {
        userNameInputField.characterLimit = 10;
        teamNameInputField.characterLimit = 10;

        userNameInputField.onValueChanged.AddListener((c) => userNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z가-힣]", ""));
        teamNameInputField.onValueChanged.AddListener((c) => teamNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z가-힣]", ""));
    }
}
