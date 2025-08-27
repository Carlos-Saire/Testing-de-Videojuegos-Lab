using UnityEngine;
using DG.Tweening;
public class Pruebas : MonoBehaviour
{

    [SerializeField] private float time;
    [SerializeField] Ease ease;

    private void Start()
    {
        transform.DOMove(Vector3.zero, time).SetEase(ease);
    }
}
