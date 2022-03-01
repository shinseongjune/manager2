using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LeagueItemClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public LeagueName league;
    Image backgroundImage;
    Transform leaguePopUpWindow;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        leaguePopUpWindow = GameObject.Find("PopUps").transform.Find("LeaguePopUpWindow");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backgroundImage.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backgroundImage.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        leaguePopUpWindow.gameObject.SetActive(true);
        switch (league)
        {
            case LeagueName.Champions:
                leaguePopUpWindow.Find("Title").Find("TitleText").GetComponent<TextMeshProUGUI>().text = "è�Ǿ� ����";
                break;
            case LeagueName.GBPro:
                leaguePopUpWindow.Find("Title").Find("TitleText").GetComponent<TextMeshProUGUI>().text = "��������� ���θ���";
                break;
            case LeagueName.ChallengersCup:
                leaguePopUpWindow.Find("Title").Find("TitleText").GetComponent<TextMeshProUGUI>().text = "ç������ ��";
                break;
        }
    }
}