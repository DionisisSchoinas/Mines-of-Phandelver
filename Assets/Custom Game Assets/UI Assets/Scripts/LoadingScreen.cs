using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Image loadingCircle;
    public float secondsTillFill;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        // Check required since the same script is used on the StartGameScene where UIEventSystem is not used
        if (UIEventSystem.current != null)
            UIEventSystem.current.onHideLoadingScreen += Hide;
    }

    private void OnDestroy()
    {
        // Check required since the same script is used on the StartGameScene where UIEventSystem is not used
        if (UIEventSystem.current != null)
            UIEventSystem.current.onHideLoadingScreen -= Hide;
    }

    public void Show()
    {
        OverlayControls.SetCanvasState(true, canvasGroup);
    }

    public void Hide()
    {
        OverlayControls.SetCanvasState(false, canvasGroup);
    }
}
