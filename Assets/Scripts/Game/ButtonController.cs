using UnityEngine;
using DG.Tweening;
public class ButtonController : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform position;
    private Vector2 inicialposition;
    [SerializeField] private float time;
    private void Start()
    {
        inicialposition = transform.position;
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.up * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector2.up, 0.1f, layer))
        {
            GameManager.Instance.ActiveFloor();
            transform.DOMove(position.position, time);
        }
        else
        {
            GameManager.Instance.DesactiveFloor();
            transform.DOMove(inicialposition, time);
        }
    }
}
