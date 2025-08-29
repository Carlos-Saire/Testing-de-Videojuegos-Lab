using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
public class CreditosController : MonoBehaviour
{
    [SerializeField] private CreditosSO[] creditos;
    private int index;
    [Header("Characteristics")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text mytext;
    [SerializeField] private TMP_Text description;

    private bool interactue;
    public void Next(InputAction.CallbackContext context)
    {
        if (context.performed&&interactue)
        {
            NextButton();
        }
    }
    public void Former(InputAction.CallbackContext context)
    {
        if (context.performed&&interactue)
        {
            FormerButton();
        }
    }
    private void NextButton()
    {
        if(index<creditos.Length-1)
        {
            ++index;
        }
        else
        {
            index=0;
        }
        GetInformation(creditos[index]);
    }
    private void FormerButton()
    {
        if (0<index)
        {
            --index;
        }
        else
        {
            index = creditos.Length-1;
        }
        GetInformation(creditos[index]);
    }
    public void GetInformation(CreditosSO creditos)
    {
        image.sprite = creditos.Image;
        mytext.text = creditos.Name;
        description.text = creditos.Description;
    }
    public void Open()
    {
        interactue = true;
        index = 0;
        GetInformation(creditos[index]);
    }
    public void Close()
    {
        interactue = false;
    }
}
