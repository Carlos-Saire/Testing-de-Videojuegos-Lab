using UnityEngine;

public class Spanwer 
{
    public GameObject Create(Vector3 pos)
    {
        return GameObject.Instantiate(Resources.Load("Prefabs/Player") as GameObject, pos, Quaternion.identity);
    }
}
