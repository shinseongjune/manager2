using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OuterGameSceneManager : MonoBehaviour
{
    #region Singleton
    private static readonly Lazy<OuterGameSceneManager> _instance = new Lazy<OuterGameSceneManager>(() => new OuterGameSceneManager());

    public static OuterGameSceneManager Instance
    {
        get
        {
            return _instance.Value;
        }
    }

    private OuterGameSceneManager() { }
    #endregion Singleton

    #region Variables
    [Header("������")]
    [SerializeField] Transform myPlayersListWindow;
    [SerializeField] GameObject myPlayersListItemPrefab;
    Transform myPlayersListWindowContent;
    #endregion Variables

    #region Properties

    #endregion Properties

    private void Awake()
    {
        myPlayersListWindowContent = myPlayersListWindow.Find("Viewport").Find("Content");
    }

    void ActivateMyPlayersListWindow()
    {
        myPlayersListWindow.gameObject.SetActive(true);
        
        Manager user = GameManager.Instance.Managers[0];
        Team userTeam = GameManager.Instance.Teams[user.Team];
        int playerCount = userTeam.Players.Count;

        //Content ũ�� ���� //(�⺻ũ��) - (���� + ������ũ��) * (���) - (����)
        int yOffset = 747 - (15 + 168) * ((playerCount - 1) / 3) - 15;
        myPlayersListWindowContent.GetComponent<RectTransform>().offsetMin = new(0, yOffset);

        for(int i = 0; i < playerCount; i++)
        {
            //���� ��������
            Player player = GameManager.Instance.Players[userTeam.Players[i]];
            
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
}
