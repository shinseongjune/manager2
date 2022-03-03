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

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
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
        OuterGameSceneManager.Instance.SetNowTeamAndReload(team.IDNumber);
        OuterGameSceneManager.Instance.ActivateMyPlayersListWindow();
    }
}
