using UnityEngine;
using DG.Tweening;
public class DoSetting : MonoBehaviour
{

    private Vector2 currentAnchoredPosition;
    [SerializeField] private float time;
    [SerializeField] private float overshoot;
    [SerializeField] private Ease ease;

    private RectTransform rectTransform;

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        currentAnchoredPosition = rectTransform.anchoredPosition;
    }

    public void OpenSetting()
    {
        rectTransform.DOAnchorPos(Vector2.zero, time).SetEase(ease, overshoot).SetUpdate(true);
    }

    public void CloseSetting()
    {
        rectTransform.DOAnchorPos(currentAnchoredPosition, time).SetEase(ease, overshoot).SetUpdate(true);
    }
}
