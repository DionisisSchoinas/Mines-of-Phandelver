using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public CanvasGroup characterSelect;
    public CanvasGroup settingsGroup;
    private CharacterSelectMenu characterSelectScript;

    private Image backgroundImage;
    private CanvasGroup canvasGroup;
    private Button startButton;
    private Button settings;
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
        settings = buttons[1];
        exitButton = buttons[2];

        startButton.onClick.AddListener(StartGameClick);
        settings.onClick.AddListener(SettingsClick);
        exitButton.onClick.AddListener(ExitButtonClick);

        characterSelectScript = characterSelect.GetComponent<CharacterSelectMenu>();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsGroup.alpha == 1f)
            {
                OverlayControls.SetCanvasState(false, settingsGroup);
            }
            else if (characterSelect.alpha == 1f)
            {
                characterSelectScript.BackToMenu();
            }
        }
    }

    private void StartGameClick()
    {
        backgroundImage.enabled = false;
        OverlayControls.SetCanvasState(true, characterSelectScript.startGamePanel);
        OverlayControls.SetCanvasState(true, characterSelect);
        OverlayControls.SetCanvasState(false, canvasGroup);
    }

    private void SettingsClick()
    {
        OverlayControls.SetCanvasState(true, settingsGroup);
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
        mode = -1;
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
