using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CalendarItemClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int month;
    Image backgroundImage;
    Transform calendarPopUpWindow;
    public List<int> matches;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        calendarPopUpWindow = GameObject.Find("PopUps").transform.Find("CalendarPopUpWindow");
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
        calendarPopUpWindow.gameObject.SetActive(true);
        calendarPopUpWindow.Find("Title").Find("TitleText").GetComponent<TextMeshProUGUI>().text = month + "¿ù";
    }
}
