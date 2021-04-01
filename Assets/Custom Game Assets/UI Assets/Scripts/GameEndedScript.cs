using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndedScript : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Text message;
    private Text countDown;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        OverlayControls.SetCanvasState(false, canvasGroup);

        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        message = texts[0];
        countDown = texts[2];
    }

    private void Start()
    {
        UIEventSystem.current.onPlayerDied += PlayerDied;
        UIEventSystem.current.onGameEnded += GameEnded;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onPlayerDied -= PlayerDied;
        UIEventSystem.current.onGameEnded -= GameEnded;
    }

    private void PlayerDied()
    {
        message.text = "Better luck next time";
        StartCoroutine(StartCountdown());
    }

    private void GameEnded()
    {
        message.text = "Good job, see you again";
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        OverlayControls.SetCanvasState(true, canvasGroup);
        yield return new WaitForSeconds(0.5f);

        for (int i = 5; i > 0; i--)
        {
            countDown.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countDown.text = "Bye ...";

        ExitToMenu();
    }

    private void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("StartGameScene");
    }
}
