using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ScoutItemClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Player player;
    Image backgroundImage;
    Transform ScoutPopUpWindow;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        ScoutPopUpWindow = GameObject.Find("PopUps").transform.Find("ScoutPopUpWindow");
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
        ScoutPopUpWindow.gameObject.SetActive(true);
        ScoutPopUpWindow.Find("Title").Find("TitleText").GetComponent<TextMeshProUGUI>().text = player?.Name;
        //TODO: �����ϱ� ��ư -> ��Ȳ�� ���� ���ȵ�(������)
    }

    public void Offer()
    {

    }
}
