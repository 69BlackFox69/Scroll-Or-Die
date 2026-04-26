using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject RadUI;
    public GameObject PlayerUI;


    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        RadUI.SetActive(false);
        PlayerUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; // разблокировать
        Cursor.visible = true; // показать
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        RadUI.SetActive(true);
        PlayerUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // снова залочить (для FPS)
        Cursor.visible = false; // спрятать
    }

    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }
}