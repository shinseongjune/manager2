using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewGameSceneManager : MonoBehaviour
{
    [Header("���� ����â")]
    [SerializeField] GameObject MakingManagerWindow;
    [SerializeField] TextMeshProUGUI userNameText;

    [Header("�� ����â")]
    [SerializeField] GameObject MakingTeamWindow;
    [SerializeField] TextMeshProUGUI teamNameText;

    Manager user;
    Team team;
    List<Player> players = new();

    public void MakeUser()
    {
        if (userNameText.text.Length <= 0) return;
        user = Maker.MakeManager(userNameText.text);
        GameManager.Instance.AddManager(user);
        MakingManagerWindow.SetActive(false);
        MakingTeamWindow.SetActive(true);
    }
}
