using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PauseMenuController : MonoBehaviour
{
    public TextMeshProUGUI pauseText;
    public GameObject pauseButtonObject;
    public GameObject panelObject;


    private PlayerAgent player;
    private float savedTimeScale = 1f;
    private bool isPaused = false;
    private bool isGameOver = false;

    public void Start()
    {
        player = FindFirstObjectByType<PlayerAgent>();
        player.OnPlayerDeath.AddListener(OnGameOver);
        panelObject.SetActive(false);
    }

    public void Update()
    {
        if (isGameOver)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                OnResume();
            }
            else
            {
                OnPause();
            }
            isPaused = !isPaused;
        }
    }

    public void OnPause()
    {
        panelObject.SetActive(true);
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void OnResume()
    {
        panelObject.SetActive(false);
        Time.timeScale = savedTimeScale;
    }

    public void OnRestart()
    {
        Time.timeScale = savedTimeScale;
        SceneManager.LoadScene("MainScene");
    }

    public void OnQuit()
    {
        Time.timeScale = savedTimeScale;
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void OnGameOver()
    {
        isGameOver = true;
        panelObject.SetActive(true);
        pauseButtonObject.SetActive(false);
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        pauseText.text = "Game Over!";
    }
}
