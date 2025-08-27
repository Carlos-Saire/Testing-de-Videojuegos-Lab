using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    static public void NewScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void CloseGame()
    {
        print("Saliste");
        Application.Quit();
    }
}
