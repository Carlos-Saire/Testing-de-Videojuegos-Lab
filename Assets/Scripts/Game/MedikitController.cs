using UnityEngine;
using System;
public class MedikitController : Item
{
    [SerializeField] private int life;
    [SerializeField] private GameObject panel;
    [SerializeField] private PlayerController player;
    [SerializeField] private float timeHeal;
    [SerializeField] private float newSpeedPlayer;

    private float speed;
    private float value;
    private float currentValue;

    public static event Action<float> eventHeal;
    private bool shoot;

    private void OnEnable()
    {
        InputReader.shoot2 += Shoot;
    }
    private void OnDisable()
    {
        InputReader.shoot2 -= Shoot;
        player.Speed = speed;
        Debug.Log(panel);
        panel.SetActive(false);
    }
    private void Start()
    {
        speed = player.Speed;
    }
    private void Update()
    {
        if (input)
        {
            if (shoot)
            {
                panel.SetActive(true);
                this.value += Time.deltaTime;
                currentValue = this.value / timeHeal;
                eventHeal?.Invoke(currentValue);
                player.Speed=newSpeedPlayer;
            }
            else
            {
                panel.SetActive(false);
                this.value = 0;
                player.Speed=speed;
            }
            if (timeHeal <= value)
            {
                player.UpdateLife(life);
                ActiveEventRemove(this.gameObject);
                Destroy(gameObject);
                input = false;
            }
        }

    }
    public void Shoot(bool value)
    {
        if (input)
        {
           shoot = value;
        }
    }

}
