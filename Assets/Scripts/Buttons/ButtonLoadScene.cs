using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonLoadScene : MyButton
{
    [SerializeField] private string scene;

    protected override void OnClick()
    {
        SceneManager.LoadScene(scene);
    }
}
