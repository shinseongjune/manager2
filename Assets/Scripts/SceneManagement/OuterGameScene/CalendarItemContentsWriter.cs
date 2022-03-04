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

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = month + "월";

        for (int i = 1; i < 5; i++)
        {
            GameObject quarterTextGameObject = transform.GetChild(i).GetChild(1).gameObject;
            quarterTextGameObject.SetActive(false);
            for (int j = 0; j < matches.Count; j++)
            {
                Match m = GameManager.Instance.Matches[matches[j]];
                if (m.DDay.Quarter == i)
                {
                    quarterTextGameObject.SetActive(true);
                    quarterTextGameObject.GetComponent<TextMeshProUGUI>().text = "";
                    string enemy;
                    if (m.Team1 == GameManager.Instance.Managers[0].Team)
                    {
                        if (m.Team2 == -1) enemy = "미정";
                        else enemy = GameManager.Instance.Teams[m.Team2].Name;

                        quarterTextGameObject.GetComponent<TextMeshProUGUI>().text = "vs " + enemy;
                    }
                    else if (m.Team2 == GameManager.Instance.Managers[0].Team)
                    {
                        if (m.Team1 == -1) enemy = "미정";
                        else enemy = GameManager.Instance.Teams[m.Team1].Name;

                        quarterTextGameObject.GetComponent<TextMeshProUGUI>().text = "vs " + enemy;
                    }
                    break;
                }
            }
            
        }
    }
}
