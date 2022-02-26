using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MyPlayersListItemClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Player player;
    Image backgroundImage;
    Transform playerPopUpWindow;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        playerPopUpWindow = GameObject.Find("PopUps").transform.Find("PlayerPopUpWindow");
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
        playerPopUpWindow.gameObject.SetActive(true);
        playerPopUpWindow.Find("Title").Find("TitleText").GetComponent<TextMeshProUGUI>().text = player?.Name;
        playerPopUpWindow.Find("Contents").Find("PlayerAgeText").GetComponent<TextMeshProUGUI>().text = player?.Age + "¼¼";
    }
}
