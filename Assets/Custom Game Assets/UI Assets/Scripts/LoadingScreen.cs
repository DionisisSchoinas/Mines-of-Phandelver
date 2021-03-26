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

    public void Show()
    {
        OverlayControls.SetCanvasState(true, canvasGroup);
    }

    public void Hide()
    {
        OverlayControls.SetCanvasState(false, canvasGroup);
    }
}
