using UnityEngine;
using DG.Tweening;

public class ButtonUi : MonoBehaviour
{

    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private float duration = 0.5f;   

    private void Start()
    {
        transform.DOScale(Vector3.one * scaleMultiplier, duration)
            .SetLoops(-1, LoopType.Yoyo) 
            .SetEase(Ease.InOutSine).SetUpdate(true);  
    }
}
