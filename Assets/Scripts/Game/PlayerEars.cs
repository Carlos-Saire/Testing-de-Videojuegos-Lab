using UnityEngine;

public class PlayerEars : MonoBehaviour
{
    private SimpleLinkList<AudioSource> list=new SimpleLinkList<AudioSource>();
    private AudioSource currentAudioSource;
    private AudioSource closestAudoSource;
    private float closestDistance;
    private float distance;
    private void UpdateSoundEnemy()
    {
        closestDistance = Mathf.Infinity;
        closestAudoSource = null;
        for (int i = 0; i < list.GetCount(); ++i)
        {
            currentAudioSource = list.GetAtPosition(i);
            currentAudioSource.enabled = false;
            distance = Vector3.Distance(transform.position, currentAudioSource.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestAudoSource = currentAudioSource;
            }
        }
        if(closestAudoSource != null)
        {
            closestAudoSource.enabled = true;
        }
    }
    public void Add(AudioSource value)
    {
        list.InsertAtEnd(value);
        UpdateSoundEnemy();
        AudioManager.instance.DetecterZombie();
    }
    public void Remove(AudioSource value)
    {
        value.enabled = false;
        list.Remove(value);
        UpdateSoundEnemy();
        if(list.GetCount() == 0)
        {
            print(list.GetCount());
            AudioManager.instance.Normal();
        }
    }
    private void OnEnable()
    {
        EnemyControl.eventDead += Remove;
        EnemyControl.eventDetected += Add;
    }
    private void OnDisable()
    {
        EnemyControl.eventDead -= Remove;
        EnemyControl.eventDetected -= Add;
    }
}
