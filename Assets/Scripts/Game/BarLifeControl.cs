using UnityEngine;
using UnityEngine.UI;
public class BarLifeControl : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private Image barLife;
    private int totalLife;
    private void Awake()
    {
        barLife = GetComponent<Image>();
    }
    private void Start()
    {
        totalLife = player.life;
    }
    private void ShowLife(int life)
    {
        barLife.fillAmount = ((float)life) / totalLife;
    }
    private void OnEnable()
    {
        player.eventLife += ShowLife;
    }
    private void OnDisable()
    {
        player.eventLife -= ShowLife;
    }
}
