using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ScaleButtonOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float ScaleFactor;
    public float ScaleTime;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DOTween.Complete(transform);
        transform.localScale = Vector2.one;
        transform.DOScale(Vector2.one * ScaleFactor, ScaleTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Complete(transform);
        transform.DOScale(Vector2.one, ScaleTime);
    }
}
