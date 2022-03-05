using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Text;

public class LeagueItemClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public League league;
    Image backgroundImage;
    Transform leaguePopUpWindow;
    Transform participantsContent;
    Transform matchesContent;
    [SerializeField] GameObject leaguePopUpWindowParticipantsItemPrefab;
    [SerializeField] GameObject leaguePopUpWindowMatchesItemPrefab;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        leaguePopUpWindow = GameObject.Find("PopUps").transform.Find("LeaguePopUpWindow");
        participantsContent = leaguePopUpWindow.GetChild(2).GetChild(1).GetChild(1).GetChild(0).GetChild(0);
        matchesContent = leaguePopUpWindow.GetChild(2).GetChild(2).GetChild(1).GetChild(0).GetChild(0);
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
        StringBuilder title = new();
        title.Append("제" + league.OrdinalNumber + "회 " + league.Name);
        leaguePopUpWindow.Find("Title").Find("TitleText").GetComponent<TextMeshProUGUI>().text = title.ToString();
        StringBuilder rule = new();
        rule.Append("대회 방식 : ");
        switch (league.RegularSeason)
        {
            case LeagueSystem.RoundRobin:
                rule.Append("라운드 로빈");
                break;
            case LeagueSystem.SingleElimination:
                rule.Append("싱글 엘리미네이션");
                break;
            case LeagueSystem.DoubleElimination:
                rule.Append("더블 엘리미네이션");
                break;
        }
        rule.Append("\n플레이 오프 : ");
        switch (league.PlayOff)
        {
            case LeagueSystem.RoundRobin:
                rule.Append("라운드 로빈(상위" + league.PlayOffTeamCount + "팀)");
                break;
            case LeagueSystem.SingleElimination:
                rule.Append("싱글 엘리미네이션(상위" + league.PlayOffTeamCount + "팀)");
                break;
            case LeagueSystem.DoubleElimination:
                rule.Append("더블 엘리미네이션(상위" + league.PlayOffTeamCount + "팀)");
                break;
            case LeagueSystem.None:
                rule.Append("없음");
                break;
        }
        leaguePopUpWindow.Find("Contents").Find("RuleText").GetComponent<TextMeshProUGUI>().text = rule.ToString();

        DeleteAllItemsInLeaguePopUpWindow();

        foreach (int i in league.Entry)
        {
            Team t = GameManager.Instance.Teams[i];
            GameObject item = Instantiate(leaguePopUpWindowParticipantsItemPrefab);
            Transform itemT = item.transform;
            itemT.GetChild(1).GetComponent<TextMeshProUGUI>().text = t.Name;
            itemT.SetParent(participantsContent);
        }

        foreach (int i in league.Matches)
        {
            Match m = GameManager.Instance.Matches[i];
            GameObject item = Instantiate(leaguePopUpWindowMatchesItemPrefab);
            Transform itemT = item.transform;
            itemT.GetChild(0).GetComponent<TextMeshProUGUI>().text = m.DDay.ToString();
            StringBuilder vs = new();
            string team1 = m.Team1 == -1 ? "미정" : GameManager.Instance.Teams[m.Team1].Name;
            string team2 = m.Team2 == -1 ? "미정" : GameManager.Instance.Teams[m.Team2].Name;
            vs.Append(team1 + " vs " + team2);
            itemT.GetChild(1).GetComponent<TextMeshProUGUI>().text = vs.ToString();
            itemT.SetParent(matchesContent);
        }
    }

    void DeleteAllItemsInLeaguePopUpWindow()
    {
        Transform[] items = participantsContent.GetComponentsInChildren<Transform>();
        foreach (Transform t in items)
        {
            if (!t.CompareTag("LeaguePopUpWindow"))
            {
                Destroy(t.gameObject);
            }
        }
        items = matchesContent.GetComponentsInChildren<Transform>();
        foreach (Transform t in items)
        {
            if (!t.CompareTag("LeaguePopUpWindow"))
            {
                Destroy(t.gameObject);
            }
        }
    }
}
