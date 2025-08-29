using UnityEngine;
using System.Collections;

public class BulletGrenadeController : MonoBehaviour
{
    [SerializeField] private float timeExplotion;
    private SimpleLinkList<EnemyControl> gameDestruciones = new SimpleLinkList<EnemyControl>();
    private EnemyControl currentEnemyControl;
    private ParticleSystem particle;
    private Rigidbody myRB;

    [Header("Sound")]
    [SerializeField] private AudioClipSO soundExplotion;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        myRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float angle = Mathf.Atan2(myRB.linearVelocity.y, myRB.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameDestruciones.InsertAtEnd(other.GetComponent<EnemyControl>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameDestruciones.Remove(other.GetComponent<EnemyControl>());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            ActiveExplocion();
        }
    }
    private void ActiveExplocion()
    {
        for (int i = 0; i < gameDestruciones.GetCount(); i++)
        {
            currentEnemyControl = gameDestruciones.GetAtPosition(i);
            if (Vector3.Distance(transform.position,currentEnemyControl.gameObject.transform.position) < 3)
            {
                currentEnemyControl.UpdateLife(-Mathf.Infinity);
            }
            else if (Vector3.Distance(transform.position,currentEnemyControl.gameObject.transform.position) < 5)
            {
                currentEnemyControl.UpdateLife(-3);
            }
            else
            {
                currentEnemyControl.UpdateLife(-1);
            }
        }
        particle.Play();
        currentEnemyControl = null;
        soundExplotion.PlayOneShoot();
        StartCoroutine(TimeExplotion());
    }
    private IEnumerator TimeExplotion()
    {
        yield return new WaitForSeconds(timeExplotion);
        Destroy(gameObject);
    }
}
