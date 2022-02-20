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
    }

    public void MakeRandomPlayers()
    {
        players[0] = Maker.MakePlayer("��÷��");
        players[1] = Maker.MakePlayer("�ڹ���");
        players[2] = Maker.MakePlayer("�ҵ巰��");
    }

    private void OnGUI()
    {
        userNameInputField.characterLimit = 10;
        teamNameInputField.characterLimit = 10;

        userNameInputField.onValueChanged.AddListener((c) => userNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z��-�R]", ""));
        teamNameInputField.onValueChanged.AddListener((c) => teamNameInputField.text = Regex.Replace(c, @"[^0-9a-zA-Z��-�R]", ""));
    }
}
