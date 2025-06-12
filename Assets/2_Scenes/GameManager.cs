using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float elapsedTime = 0f;
    private float fatestTime = float.MaxValue;
    private bool isGameStarted = false; // 게임 시작 여부

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isGameStarted) return; // 게임 시작 전에는 Update 실행 안 함

        elapsedTime += Time.deltaTime;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateTimeText(FormatElapsedTime(elapsedTime));
        }
    }

    public void GameStart()
    {
        isGameStarted = true;
        Time.timeScale = 1f;
    }

    public void GameStop()
    {
        if (!isGameStarted) return;
        isGameStarted = false;
        Time.timeScale = 0f;

        UIManager.Instance.HidePanel();

        Time.timeScale = 1f; 

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );

        if (elapsedTime < fatestTime)
        {
            fatestTime = elapsedTime;
            Debug.Log("New fastest time: " + FormatElapsedTime(fatestTime));
        }

        UIManager.Instance.UpdateCurrentTimeText("Current Time : " + FormatElapsedTime(elapsedTime));
        UIManager.Instance.UpdateFastTimeText("Fastest time : " + FormatElapsedTime(fatestTime));

        UIManager.Instance.ShowPanel();
        elapsedTime = 0f;
    }

    public void GameRestart()
    {
        if (!isGameStarted) return;
        isGameStarted = false;
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        elapsedTime = 0f;
        UIManager.Instance.HidePanel();
    }

    private string FormatElapsedTime(float time)
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        int milliseconds = (int)((time * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}



