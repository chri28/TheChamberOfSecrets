using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelMenu levelMenu;
    public void PlayGame(int level)
    {
        SceneManager.LoadSceneAsync("Scenes/level" + level);
    }

    public void QuitGame()
    {
        levelMenu.ResetLevels();
        Application.Quit();
    }
}
