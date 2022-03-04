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
    Transform[] quartersContent = new Transform[4];
    [SerializeField] GameObject calendarPopUpItemPrefab;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        calendarPopUpWindow = GameObject.Find("PopUps").transform.Find("CalendarPopUpWindow");
        for (int i = 0; i < 4; i++)
        {
            quartersContent[i] = calendarPopUpWindow.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetChild(1).GetChild(0).GetChild(0);
        }
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
        calendarPopUpWindow.Find("Title").Find("TitleText").GetComponent<TextMeshProUGUI>().text = month + "월";

        DeleteAllItems();

        for (int i = 0; i < matches.Count; i++)
        {
            Match m = GameManager.Instance.Matches[matches[i]];
            GameObject item = Instantiate(calendarPopUpItemPrefab);
            item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Leagues[m.League].Name;
            string team1 = m.Team1 == -1 ? "미정" : GameManager.Instance.Teams[m.Team1].Name;
            string team2 = m.Team2 == -1 ? "미정" : GameManager.Instance.Teams[m.Team2].Name;
            item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = team1 + " vs " + team2;

            item.transform.SetParent(quartersContent[m.DDay.Quarter - 1]);
        }
    }

    void DeleteAllItems()
    {
        for (int i = 0; i < 4; i++)
        {
            Transform[] items = quartersContent[i].GetComponentsInChildren<Transform>();
            foreach (Transform t in items)
            {
                if (t.CompareTag("CalendarPopUpWindow")) continue;
                Destroy(t.gameObject);
            }

        }
    }
}
