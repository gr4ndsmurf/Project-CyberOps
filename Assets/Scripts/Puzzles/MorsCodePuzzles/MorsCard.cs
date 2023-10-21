using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Dynamic;

public class MorsCard : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public MorsCardInfo morsCardInfo;

    public TextMeshProUGUI cardName;
    public int cardID;

    [SerializeField] private RectTransform cardRect;
    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;

    [HideInInspector] public Transform parentAfterDrag;

    public void Start()
    {
        cardRect = gameObject.GetComponent<RectTransform>();
        cardName.text = morsCardInfo.cardName;
        cardID = morsCardInfo.cardID;
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        cardRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parentAfterDrag);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
