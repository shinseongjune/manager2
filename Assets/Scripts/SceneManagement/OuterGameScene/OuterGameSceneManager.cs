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
    [Header("선수단")]
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

        //Content 크기 조정 //(기본크기) - (여백 + 아이템크기) * (계수) - (여백)
        int yOffset = 747 - (15 + 168) * ((playerCount - 1) / 3) - 15;
        myPlayersListWindowContent.GetComponent<RectTransform>().offsetMin = new(0, yOffset);

        for(int i = 0; i < playerCount; i++)
        {
            //선수 가져오기
            Player player = GameManager.Instance.Players[userTeam.Players[i]];
            
            //선수 넣을 아이템 생성 및 부모 설정
            GameObject myPlayersListItem = Instantiate(myPlayersListItemPrefab);
            Transform itemTransform = myPlayersListItem.transform;
            itemTransform.SetParent(myPlayersListWindowContent);

            //선수 정보 기입
            itemTransform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = player.Name;
            itemTransform.Find("PlayerAgeText").GetComponent<TextMeshProUGUI>().text = player.Age + "세";

            //클릭 핸들러에 선수 등록
            MyPlayersListItemClickHandler clickHandler = myPlayersListItem.GetComponent<MyPlayersListItemClickHandler>();
            clickHandler.player = player;

            //아이템 위치 조정
            itemTransform.GetComponent<RectTransform>().anchoredPosition = new(15 + (15 + 390) * (i % 3), -15 - (15 + 168) * (i / 3));
        }
    }
}
