using UnityEngine;
using UnityEngine.UI;
public class PanelHeal : MonoBehaviour
{
    [SerializeField] private Image heal;
    private void Heal(float value)
    {
        heal.fillAmount = value;
    }
    private void OnEnable()
    {
        MedikitController.eventHeal += Heal;
    }
    private void OnDisable()
    {
        MedikitController.eventHeal -= Heal;

    }
}
