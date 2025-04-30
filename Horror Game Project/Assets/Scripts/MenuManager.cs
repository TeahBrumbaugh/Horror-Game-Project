using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(SceneNames.Level1);
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene(SceneNames.Settings);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
