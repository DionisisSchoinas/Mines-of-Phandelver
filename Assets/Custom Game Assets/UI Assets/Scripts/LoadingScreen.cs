using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Image loadingCircle;
    public float secondsTillFill;

    private CanvasGroup canvasGroup;
    private Coroutine coroutine;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        // Check required since the same script is used on the StartGameScene where UIEventSystem is not used
        if (UIEventSystem.current != null)
        {
            UIEventSystem.current.onShowLoadingScreen += Show;
            UIEventSystem.current.onHideLoadingScreen += Hide;
        }
    }

    private void OnDestroy()
    {
        // Check required since the same script is used on the StartGameScene where UIEventSystem is not used
        if (UIEventSystem.current != null)
        {
            UIEventSystem.current.onShowLoadingScreen -= Show;
            UIEventSystem.current.onHideLoadingScreen -= Hide;
        }
    }

    public void Show()
    {
        OverlayControls.SetCanvasState(true, canvasGroup);
        coroutine = StartCoroutine(Loading());
    }

    public void Show(AsyncOperation loadingOperation)
    {
        OverlayControls.SetCanvasState(true, canvasGroup);
        coroutine = StartCoroutine(Loading(loadingOperation));
    }

    public void Hide()
    {
        OverlayControls.SetCanvasState(false, canvasGroup);
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    // Used for the MainGameScene
    private IEnumerator Loading()
    {
        int state = 0;
        loadingCircle.fillClockwise = true;

        while (canvasGroup.alpha == 1)
        {
            state = SpinCircle(state);
            yield return new WaitForSecondsRealtime(secondsTillFill / 100f);
        }
    }

    // Used for the transition from StartGameSceen to MainGameScene
    private IEnumerator Loading(AsyncOperation loadingOperation)
    {
        int state = 0;
        loadingCircle.fillClockwise = true;

        while (!loadingOperation.isDone)
        {
            state = SpinCircle(state);
            yield return new WaitForSecondsRealtime(secondsTillFill / 100f);
        }
    }

    private int SpinCircle(int oldState)
    {
        int state = oldState;

        if (state == 0 && loadingCircle.fillAmount < 1)
        {
            loadingCircle.fillAmount += 0.01f;
        }
        else if (state == 0)
        {
            state = 1;
            loadingCircle.fillClockwise = false;
        }

        if (state == 1 && loadingCircle.fillAmount > 0)
        {
            loadingCircle.fillAmount -= 0.01f;
        }
        else if (state == 1)
        {
            state = 0;
            loadingCircle.fillClockwise = true;
        }

        return state;
    }
}
