using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCardClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Image cardBackgroundImage;
    public Player player;

    private void Awake()
    {
        cardBackgroundImage = GetComponent<Image>();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        cardBackgroundImage.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardBackgroundImage.color = Color.white;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.Teams[0].AddPlayer(player.IDNumber);
        player.SetTeam(0);
        NewGameSceneManager.Instance.RefreshSelectPlayerWindow();
    }
}
