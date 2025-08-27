using UnityEngine;
using System;
public class ButtonAcensor : MonoBehaviour
{
    public static event Action evetSubir;
    public static event Action evetBajar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Caja"))
        {
            evetSubir?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        evetBajar?.Invoke();
    }
}
