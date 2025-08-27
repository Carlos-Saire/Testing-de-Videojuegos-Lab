using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Objetive Variable")]
    public GameObject Jugador;

    [Header("Posiciones en X")]
    public float minPositionX;
    public float maxPositionX;

    [Header("Posiciones en Y")]
    public float minPositionY;
    public float maxPositionY;

    public float timeToGetObjective;
    private Vector3 velocity = Vector3.zero;

    private Vector3 realObjective;

    private void FixedUpdate()
    {
        float targetX = Jugador.transform.position.x;
        float targetY = Jugador.transform.position.y;

        targetX = Mathf.Clamp(targetX, minPositionX, maxPositionX);
        targetY = Mathf.Clamp(targetY, minPositionY, maxPositionY);

        realObjective = new Vector3(targetX, targetY, -10);

        transform.position = Vector3.SmoothDamp(transform.position, realObjective, ref velocity, timeToGetObjective);
    }
}
