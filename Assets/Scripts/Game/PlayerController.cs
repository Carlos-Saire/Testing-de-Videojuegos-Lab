using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{

    [Header("Player")]
    private Rigidbody Rb;
    private Vector2 movement;
    [SerializeField] private float speed;
    public float Speed { get { return speed; }set { speed = value; } }
    public int life;

    private Transform cameraMain;
    private Vector3 forward;
    private Vector3 right;
    private Vector3 moveDirection;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;

    [Header("Raycast")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce;
    private bool isjump;

    public event Action<int> eventLife;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        cameraMain = Camera.main.transform;
    }
    private void FixedUpdate()
    {
        Rb.linearVelocity = new Vector3(moveDirection.x * speed, Rb.linearVelocity.y, moveDirection.z * speed);
        if (Physics.Raycast(transform.position, Vector3.down, 1.03f, groundLayer))
        {
            if (isjump)
            {
                Rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }    
    }
    private void Update()
    {
        forward = cameraMain.forward;
        right = cameraMain.right;
        moveDirection = forward * movement.y + right * movement.x;

    }
    private void ActiveEventLife()
    {
        eventLife?.Invoke(life);
    }

    public void MovementPlayer(Vector2 value)
    {
        movement = value;
        if (movement != Vector2.zero)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    } 

    public void Jump(bool value)
    {
        isjump = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ManoEnemy"))
        {
            UpdateLife(-5);
        }
    }
    public void UpdateLife(int life)
    {
        this.life += life;
        ActiveEventLife();
    }

    private void OnEnable()
    {
        InputReader.movementPlayer += MovementPlayer;
        InputReader.jump += Jump;
    }

    private void OnDisable()
    {
        InputReader.movementPlayer -= MovementPlayer;
        InputReader.jump -= Jump;
    }

}
