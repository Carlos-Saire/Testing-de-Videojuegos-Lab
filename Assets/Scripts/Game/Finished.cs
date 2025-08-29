using UnityEngine;
using System;
public class Finished : MonoBehaviour
{
    static public event Action eventFinished;
    [SerializeField] private DoorController doorController;
    private bool boolfinished;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && doorController.transform.rotation==Quaternion.Euler(doorController.RotationOpen)&&!boolfinished)
        {
            eventFinished?.Invoke();
            boolfinished = true;
        }
    }
}
