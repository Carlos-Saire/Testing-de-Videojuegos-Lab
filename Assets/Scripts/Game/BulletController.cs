using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;
    private Rigidbody RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }
    public void MoveBullet(Vector3 shootDirection)
    {
        direction = shootDirection;
    }
    private void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            RB.linearVelocity = Vector3.zero;
            RB.AddForce(direction * speed, ForceMode.Impulse);
        }
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

}
