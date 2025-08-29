using UnityEngine;
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void DetecterZombie()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    public void Normal()
    {
        audioSource.Stop();
    }
}
