using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarItemContentsWriter : MonoBehaviour
{
    public void WriteContents()
    {
        CalendarItemClickHandler handler = GetComponent<CalendarItemClickHandler>();
        int month = handler.month;
        List<int> matches = handler.matches;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = month + "¿ù";
        int nowYear = OuterGameSceneManager.Instance.calendarNowYear;

        for (int i = 1; i < 5; i++)
        {
            for (int j = 0; j < matches.Count; j++)
            {
                Match m = GameManager.Instance.Matches[matches[j]];
                if (m.DDay.Quarter == i)
                {
                    GameObject quarterTextGameObject = transform.GetChild(i).GetChild(1).gameObject;
                    quarterTextGameObject.SetActive(true);
                    string enemy;
                    if (m.Team1 == -1 || m.Team2 == -1) enemy = "¹ÌÁ¤";
                    else if (m.Team1 == GameManager.Instance.Managers[0].Team) enemy = GameManager.Instance.Teams[m.Team2].Name;
                    else enemy = GameManager.Instance.Teams[m.Team1].Name;
                    quarterTextGameObject.GetComponent<TextMeshProUGUI>().text = "vs" + " " + enemy;
                    break;
                }
            }
            
        }
    }
}
