using System;
using UnityEngine;

abstract public class Item : InteractiveObject
{
    [Header("Weapon")]
    [SerializeField] protected Vector3 weaponPosition;
    [SerializeField] protected Vector3 weaponRotation;
    [SerializeField] protected Sprite logo;

    protected Transform cameraMain;

    static public event Action<GameObject, Sprite> eventInformation;
    static public event Action<GameObject> eventRemove;
    protected void Awake()
    {
        cameraMain = Camera.main.transform;
    }
    protected override void Interactive()
    {
        transform.SetParent(cameraMain);
        transform.localPosition = weaponPosition;
        transform.localRotation = Quaternion.Euler(weaponRotation);
        eventInformation?.Invoke(this.gameObject, logo);
        input = true;
    }
    protected virtual void ActiveEventRemove(GameObject go)
    {
        eventRemove?.Invoke(go);
    }
}
