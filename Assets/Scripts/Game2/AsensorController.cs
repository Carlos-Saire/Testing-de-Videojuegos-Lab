using UnityEngine;
using DG.Tweening;
public class AsensorController : MonoBehaviour
{
    [SerializeField] private Transform Tarjet;
    [SerializeField] private float time;
    private Vector2 mytransform;

    private void Start()
    {
        mytransform = transform.position;
    }
    public void Subir()
    {
        transform.DOMove(Tarjet.position,time);
    }
    public void Bajar()
    {
        transform.DOMove(mytransform,time);
    }
    private void OnEnable()
    {
        ButtonAcensor.evetSubir += Subir;
        ButtonAcensor.evetBajar += Bajar;
    }
    private void OnDisable()
    {
        ButtonAcensor.evetBajar -= Bajar;
        ButtonAcensor.evetSubir -= Subir;
    }
}
