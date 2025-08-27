using UnityEngine;
using DG.Tweening;
public class DoWin : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private float time;
    [SerializeField] private Ease ease;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.DOAnchorPos(target.anchoredPosition, time).SetEase(ease).SetUpdate(true);
    }
}
