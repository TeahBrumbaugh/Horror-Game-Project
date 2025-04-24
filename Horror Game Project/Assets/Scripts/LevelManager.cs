using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Level2Button()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level3Button()
    {
        SceneManager.LoadScene("Level3");
    }
}
