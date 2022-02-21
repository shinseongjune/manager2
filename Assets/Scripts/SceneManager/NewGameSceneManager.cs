using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class NewGameSceneManager : MonoBehaviour
{
    [Header("���� ����â")]
    [SerializeField] GameObject MakingManagerWindow;
    [SerializeField] TMP_InputField userNameInputField;
    [SerializeField] TextMeshProUGUI userNameText;

    [Header("�� ����â")]
    [SerializeField] GameObject MakingTeamWindow;
    [SerializeField] TMP_InputField teamNameInputField;
    [SerializeField] TextMeshProUGUI teamNameText;

    [Header("���� ����â")]
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
        players[0] = Maker.MakePlayer("��÷��");
        players[1] = Maker.MakePlayer("�ڹ���");
        players[2] = Maker.MakePlayer("�ҵ巰��");
    }

    public void RefreshSelectPlayerWindow()
    {
        MakeRandomPlayers();
        PlayerCard1.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[0].Name;
        PlayerCard2.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[1].Name;
        PlayerCard3.GetChild(0).GetComponent<TextMeshProUGUI>().text = players[2].Name;
        
        PlayerCard1.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[0].Age + "��";
        PlayerCard2.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[1].Age + "��";
        PlayerCard3.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[2].Age + "��";
    }

    private void OnGUI()
    {
        userNameInputField.characterLimit = 10;
        teamNameInputField.characterLimit = 10;

        userNameInputField.onValueChanged.AddListener((c) => userNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z��-�R]", ""));
        teamNameInputField.onValueChanged.AddListener((c) => teamNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z��-�R]", ""));
    }
}
