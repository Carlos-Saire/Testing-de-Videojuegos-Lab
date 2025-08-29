using TMPro;
using UnityEngine;

public class MostrarPuntaje : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textos;
    [SerializeField] private PointsCounterSO counterSO;
    public void Inprimir()
    {
        int numberOfScores = Mathf.Min(textos.Length, counterSO.Point.GetCount());

        for (int i = 0; i < numberOfScores; i++)
        {
            textos[i].text =(int)(i + 1) + " : " + counterSO.Point.GetAtPosition(i);
        }
        for (int i = numberOfScores; i < textos.Length; i++)
        {
            textos[i].text = (i + 1) + " :";
        }
    }
}
