using System;
using UnityEngine;

public class WeaponController : Item
{
    [Header("")]
    [SerializeField] private GameObject[] bulletPrefab;
    [SerializeField] private float fireRate ;
    [SerializeField] private float reloadDuration ;

    [Header("Sound")]
    [SerializeField] private AudioClipSO BulletSound;

    private Stack<GameObject> shoot = new Stack<GameObject>();
    private Stack<GameObject> reload = new Stack<GameObject>();

    private float nextFireTime;
    private float reloadTime; 
    private bool canShoot = true;
    private bool isReloading;

    private DoScale doScale;
    [SerializeField] private GameObject puntero;
    private void Start()
    {
        for (int i = 0; i < bulletPrefab.Length; i++)
        {
            bulletPrefab[i].SetActive(false);
            shoot.Push(bulletPrefab[i]);
        }
    }
    protected override void Interactive()
    {
        base.Interactive();
        puntero.SetActive(true);
    }
    private void Update()
    {
        if (!canShoot)
        {
            nextFireTime -= Time.deltaTime;
            if (nextFireTime <= 0f)
            {
                canShoot = true;
            }
        }
        if (isReloading)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0f)
            {
                isReloading = false;
            }
        }
    }
    private void Shoot()
    {
        if (canShoot && input&& !isReloading)
        {
            try
            {
                Fire();
                canShoot = false; 
                nextFireTime = fireRate;
            }
            catch (NullReferenceException)
            {
                Reload();
            }
        }
    }
    private void Reload()
    {
        if (input&&!isReloading)
        {
            while (reload.Count > 0)
            {
                GameObject bullet = reload.Pop();
                bullet.SetActive(false);
                shoot.Push(bullet);
            }
            isReloading = true;
            reloadTime = reloadDuration;
        }
    }
    private void Fire()
    {
        GameObject bullet = shoot.Pop();

        bullet.SetActive(true);
        bullet.transform.position = transform.position;
        
        reload.Push(bullet);

        BulletController bulletController = bullet.GetComponent<BulletController>();
        
        bulletController.MoveBullet(cameraMain.transform.forward);
        BulletSound.PlayOneShoot();

    }
    private void OnEnable()
    {
        if(input)
        {
            puntero.SetActive(true);
        }
        InputReader.shoot += Shoot;
        InputReader.reload += Reload;
    }
   private void OnDisable()
    {
        puntero.SetActive(false);
        InputReader.reload -= Reload;
        InputReader.shoot -= Shoot;
    }
}
