using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
