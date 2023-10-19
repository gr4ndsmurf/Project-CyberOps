using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Dynamic;

public class MorsCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public MorsCardInfo morsCardInfo;

    public TextMeshProUGUI cardName;
    public int cardID;

    [SerializeField] private RectTransform cardRect;

    private Vector3 firstPos;

    public void Start()
    {
        cardRect = gameObject.GetComponent<RectTransform>();
        cardName.text = morsCardInfo.cardName;
        cardID = morsCardInfo.cardID;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        firstPos = cardRect.position;
        cardRect.localScale = new Vector3(1, 1, 1);
    }
    public void OnDrag(PointerEventData eventData)
    {
        cardRect.position = eventData.position;
        cardRect.localScale = new Vector3(1, 1, 1);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        cardRect.position = firstPos;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        cardRect.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        cardRect.localScale = new Vector3(1, 1, 1);
    }
}
