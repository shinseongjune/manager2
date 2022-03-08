using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TeamListItemClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Team team;
    Image backgroundImage;
    Transform teamPopUpWindow;
    [SerializeField] GameObject teamPopUpWindowPlayerItemPrefab;
    [SerializeField] GameObject teamPopUpWindowLeagueItemPrefab;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        teamPopUpWindow = GameObject.Find("PopUps").transform.Find("TeamPopUpWindow");
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
        teamPopUpWindow.gameObject.SetActive(true);

        teamPopUpWindow.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = team.Name;

        Transform teamPopUpWindowPlayersScrollViewContent = teamPopUpWindow.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0);
        Transform teamPopUpWindowLeaguesScrollViewContent = teamPopUpWindow.GetChild(2).GetChild(1).GetChild(1).GetChild(0).GetChild(0);

        DeleteAllItemsInTeamPopUpWindow(ref teamPopUpWindowPlayersScrollViewContent, ref teamPopUpWindowLeaguesScrollViewContent);

        for(int i = 0; i < team.Players.Count; i++)
        {
            Player p = GameManager.Instance.Players[team.Players[i]];
            GameObject item = Instantiate(teamPopUpWindowPlayerItemPrefab);
            Transform itemTransform = item.transform;
            itemTransform.GetChild(1).GetComponent<TextMeshProUGUI>().text = p.Name;
            itemTransform.GetChild(2).GetComponent<TextMeshProUGUI>().text = p.Age + "세";
            itemTransform.SetParent(teamPopUpWindowPlayersScrollViewContent);
        }

        //TODO: 팀에 리그 추가 후 리그 아이템 만드는 for문

        string managerName = team.Manager == -1 ? "공석" : GameManager.Instance.Managers[team.Manager].Name;
        teamPopUpWindow.GetChild(2).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text = managerName;
    }

    void DeleteAllItemsInTeamPopUpWindow(ref Transform t1, ref Transform t2)
    {
        Transform[] transforms = t1.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.CompareTag("TeamPopUpWindow")) continue;
            Destroy(t.gameObject);
        }
        transforms = t2.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.CompareTag("TeamPopUpWindow")) continue;
            Destroy(t.gameObject);
        }
    }
}
