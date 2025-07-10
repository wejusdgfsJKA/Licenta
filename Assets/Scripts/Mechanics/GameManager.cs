using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas gameOverScreen;
    [SerializeField] TextMeshProUGUI gameOverText;
    public static GameManager Instance { get; private set; }
    [field: SerializeField] public List<Transform> Stars { get; protected set; } = new();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void GameOver(Transform winner)
    {
        //game over
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverScreen.gameObject.SetActive(true);
        gameOverText.text = $"Game over!{winner.name} won!";
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
