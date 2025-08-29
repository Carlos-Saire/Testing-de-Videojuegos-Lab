using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    
    [Header("Enemy")]
    private Vector3 positionToMove;
    [SerializeField] private float life;
    [SerializeField] private float aceleration;
    [SerializeField] private SphereCollider handColliderZombie;
    private float acelerationinicial;

    private GameObject positionPlayer = null;

    [SerializeField] private float damageInterval = 1f; 
    private float damageTimer;

    NavMeshAgent navMeshAgent;

    [Header("Raycast")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float distance;
    private RaycastHit hit;
    [SerializeField] private float height;
    [SerializeField] private float coneAngle;
    [SerializeField] private int rayCount;
    private Vector3 raycastOrigin;

    bool playerDetected = false;

    [Header("Sound")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip soundDetected;

    private Animator animator;

    [SerializeField] private float timeDamage;
    [SerializeField] private float distanceTarjet;
    private BoxCollider boxCollider;

    private bool alive;

    //Eventos
    static public event Action <AudioSource> eventDead;
    static public event Action <AudioSource> eventDetected;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        alive = true;
    }

    private void Update()
    {
        if (alive)
        {
            if (positionPlayer == null)
            {
                Destination(positionToMove);
                raycastOrigin = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
            }
            else
            {
                navMeshAgent.stoppingDistance = distanceTarjet;
                if (aceleration > acelerationinicial)
                {
                    acelerationinicial += Time.deltaTime;
                    navMeshAgent.acceleration = animationCurve.Evaluate(acelerationinicial);
                }
                if (Vector3.Distance(transform.position, positionPlayer.transform.position) > distanceTarjet)
                {
                    Destination(positionPlayer.transform.position);
                    animator.Play("Run");
                    damageTimer = 0;
                    handColliderZombie.enabled = false;
                }
                else
                {
                    damageTimer -= Time.deltaTime;
                    if (damageTimer <= 0f)
                    {
                        animator.Play("Attack");
                        StartCoroutine(TimeDamage());
                        damageTimer = damageInterval;
                    }
                }
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (!playerDetected&&alive)
        {
            for (int i = -rayCount / 2; i < rayCount / 2; i++)
            {
                float angle = i * (coneAngle / rayCount);
                Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
                Debug.DrawRay(raycastOrigin, direction * distance, Color.black);
                if (Physics.Raycast(raycastOrigin, direction, out hit, distance, layerMask))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        positionPlayer = hit.collider.gameObject;
                        playerDetected = true;
                        audioSource.clip = soundDetected;
                        audioSource.Play();
                        eventDetected?.Invoke(audioSource);
                        break;
                    }
                }
            }
        }
    }
    public void UpdateLife(float life)
    {
        this.life += life;
        if (this.life <= 0)
        {
            alive = false;
            handColliderZombie.enabled = false;
            eventDead?.Invoke(audioSource);
            animator.Play("Dead");
            boxCollider.enabled = false;
        }
    }

    private void Destination(Vector3 position)
    {
        navMeshAgent.destination=position;
    }

    public void SetNewPosition(Vector3 newPosition)
    {
        positionToMove = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            SetNewPosition(other.GetComponent<NodeControl>().GetAdjacentNode().transform.position);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            UpdateLife(-1);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Player"))
        {
            handColliderZombie.enabled = false;
        }
    }
    private IEnumerator TimeDamage()
    {
        yield return new WaitForSeconds(timeDamage);
        handColliderZombie.enabled = true;
    }
}
