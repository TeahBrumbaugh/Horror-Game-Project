using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused = false;
    public Image crosshair;
    public LockCursor lockCursor;

    // Update is called once per frame

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        crosshair.enabled = true;
        Time.timeScale = 1f;
        isPaused = false;
        lockCursor.SetCursorState(true);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        crosshair.enabled = false;
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.MainMenu);
    }

    public void Settings()
    {
        SceneManager.LoadScene(SceneNames.Settings);
    }
    
}
