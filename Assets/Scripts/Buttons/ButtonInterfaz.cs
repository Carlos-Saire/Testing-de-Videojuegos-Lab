using UnityEngine;
public class ButtonInterfaz : MyButton
{
    [SerializeField] private bool open;
    [SerializeField]private DoUI ui;
    protected override void OnClick()
    {
        if (open)
        {
            ui.Open();
        }
        else
        {
            ui.Close();
        }
    }
}
