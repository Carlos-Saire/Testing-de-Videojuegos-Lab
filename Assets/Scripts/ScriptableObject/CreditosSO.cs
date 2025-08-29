using UnityEngine;
[CreateAssetMenu(fileName = "CreditosSO", menuName = "Scriptable Objects/CreditosSO", order = 1)]

public class CreditosSO : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private string newName;
    [SerializeField] private string description;

    public Sprite Image =>image;
    public string Name => newName;
    public string Description => description;
}
