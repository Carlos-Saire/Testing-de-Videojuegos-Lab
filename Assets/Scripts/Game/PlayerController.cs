using Unity.Mathematics;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody2D RB2D;
    private float horizontal;
    [SerializeField] private float forceJump;
    [SerializeField] private float forceSpeed;
    private bool isjump;
    [SerializeField] private float deceleration = 5.0f;

    [Header("Raycast")]
    [SerializeField] private LayerMask layer;

    [Header("Limit")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private float currentX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private float currentY;

    [Header("Sound")]
    [SerializeField] private AudioClipSO audioClip;
    [SerializeField] private AudioClipSO audioJump;

    static public event Action eventWin;
    static public event Action eventDefeat;

    private void Awake()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isjump = true;
            audioJump.PlayOneShoot();
        }
        else if (Input.GetKeyUp(KeyCode.Space)) 
        {
            isjump = false;
        }

        currentY=math.clamp(transform.position.y,minY,maxY);
        currentX=math.clamp(transform.position.x,minX,maxX);
        transform.position=new Vector2 (currentX,currentY);

        if (transform.position.y == minY)
        {
            ActiveEvenDefeat();
        }
    }

    private void FixedUpdate()
    {
        if(horizontal!=0)
        {
            RB2D.AddForce(new Vector2(horizontal * forceSpeed, 0),ForceMode2D.Force);
        }
        else
        {
            if (Mathf.Abs(RB2D.linearVelocity.x) > 0.1f) 
            {
                float decelerationForce = deceleration * Mathf.Sign(RB2D.linearVelocity.x);
                RB2D.AddForce(new Vector2(-decelerationForce, 0), ForceMode2D.Force);
            }
            else
            {
                RB2D.linearVelocity = new Vector2(0, RB2D.linearVelocity.y);
            }
        }

        if (Physics2D.Raycast(transform.position, Vector3.down, 1.03f, layer))
        {
            if (isjump)
            {
                RB2D.AddForce(new Vector3(0, forceJump, 0), ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            GameManager.Instance.AddPoint();
            audioClip.PlayOneShoot();
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Puas"))
        {
            ActiveEvenDefeat();

        }
        if (collision.gameObject.CompareTag("Meta"))
        {
            ActiveEventWin();
        }
    }
    private void ActiveEventWin()
    {
        eventWin?.Invoke();
    }
    private void ActiveEvenDefeat()
    {
        eventDefeat?.Invoke();
    }
}
