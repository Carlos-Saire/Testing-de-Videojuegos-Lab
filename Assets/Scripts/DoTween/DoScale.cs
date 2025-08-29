using DG.Tweening;
using UnityEngine;
public class DoScale : MonoBehaviour
{
    [SerializeField] private Option option;
    [SerializeField] Vector3 scale;
    [SerializeField] private float time;
    private RectTransform rectTransform;

    private Tweener tweener;
    private void Awake()
    {
        switch (option)
        {
            case Option.Transform:
                tweener = transform.DOScale(scale, time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case Option.RectTransform:
                rectTransform = GetComponent<RectTransform>();

                tweener = rectTransform.DOScale(scale, time).SetLoops( -1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
        }
    }

    private void OnDisable()
    {
        tweener.Kill();
    }
}
public enum Option
{
    Transform,
    RectTransform
}
