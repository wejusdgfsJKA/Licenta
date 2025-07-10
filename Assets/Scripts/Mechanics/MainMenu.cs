using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartPlayer()
    {
        GlobalConfig.Player = true;
        SceneManager.LoadScene(1);
    }
    public void StartAI()
    {
        GlobalConfig.Player = false;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
