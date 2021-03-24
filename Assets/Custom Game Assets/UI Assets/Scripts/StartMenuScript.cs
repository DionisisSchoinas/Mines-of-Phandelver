using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public CanvasGroup characterSelect;

    private Image backgroundImage;
    private CanvasGroup canvasGroup;
    private Button startButton;
    private Button exitButton;

    private int mode;

    private void Awake()
    {
        backgroundImage = gameObject.GetComponentsInParent<Image>()[1];
        backgroundImage.enabled = true;

        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        OverlayControls.SetCanvasState(true, canvasGroup);

        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        startButton = buttons[0];
        exitButton = buttons[1];

        startButton.onClick.AddListener(StartGameClick);
        exitButton.onClick.AddListener(ExitButtonClick);

        mode = -1;
    }

    private void Start()
    {
        YesNoDialog.current.onResponded += Response;
    }

    private void OnDestroy()
    {
        YesNoDialog.current.onResponded -= Response;
    }

    private void StartGameClick()
    {
        backgroundImage.enabled = false;
        OverlayControls.SetCanvasState(true, characterSelect);
        OverlayControls.SetCanvasState(false, canvasGroup);
    }

    private void ExitButtonClick()
    {
        mode = 0;
        OverlayControls.SetCanvasState(0.2f, canvasGroup);
        YesNoDialog.SetDialogText("Exit the Game");
    }

    private void Response(bool response)
    {
        if (response)
        {
            if (mode == 0)
            {
                ExitGame();
            }
        }
        OverlayControls.SetCanvasState(true, canvasGroup);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
