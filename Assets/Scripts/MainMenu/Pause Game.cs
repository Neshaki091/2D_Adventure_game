using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    public void Pause()
    {
        PauseMenu.SetActive(true);
    }
    public void Exit ()
    {
        PauseMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
        PauseMenu.SetActive(false);
    }
}
