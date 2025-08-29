using UnityEngine;
public class ButtonCloseGame : MyButton
{
    protected override void OnClick()
    {
        Debug.Log("Se Cerro el Juego");
        Application.Quit();
    }
}
