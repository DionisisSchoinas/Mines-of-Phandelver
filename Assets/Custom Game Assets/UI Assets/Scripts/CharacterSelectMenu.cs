using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviour
{
    public CanvasGroup startMenu;
    public CanvasGroup startGamePanel;
    public LoadingScreen loadingScreenPanel;

    private Image backgroundImage;
    private CanvasGroup canvasGroup;
    private Button backToMenu;
    private Button startGameButton;

    private SelectedCharacterScript.Character selectedCharacter;
    private List<CharacterSelectButton> characterSelectButtons;

    private AsyncOperation loadingOperation;

    private void Awake()
    {
        backgroundImage = gameObject.GetComponentsInParent<Image>()[1];

        SelectedCharacterScript[] scripts = FindObjectsOfType<SelectedCharacterScript>();
        foreach (SelectedCharacterScript s in scripts)
            Destroy(s.gameObject);

        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        backToMenu = buttons[buttons.Length-1];

        characterSelectButtons = new List<CharacterSelectButton>();

        startGameButton = startGamePanel.gameObject.GetComponentInChildren<Button>();
        startGameButton.interactable = false;

        backToMenu.onClick.AddListener(BackToMenu);
        startGameButton.onClick.AddListener(StartGame);
    }

    private void Start()
    {
        OverlayControls.SetCanvasState(false, canvasGroup);
        OverlayControls.SetCanvasState(false, startGamePanel);
        loadingScreenPanel.Hide();
    }

    public void PickCharacter(SelectedCharacterScript.Character character)
    {
        foreach (CharacterSelectButton button in characterSelectButtons)
        {
            if (character.Equals(button.character)) // Open lights for selected character
                button.SetLights(true);
            else  // Close lights for all others
                button.SetLights(false);
        }

        selectedCharacter = character;

        startGameButton.interactable = true;
    }

    public void AddButton(CharacterSelectButton button)
    {
        characterSelectButtons.Add(button);
    }

    private void StartGame()
    {
        GameObject gm = new GameObject();
        SelectedCharacterScript script = gm.AddComponent<SelectedCharacterScript>();
        script.SetCharacter(selectedCharacter);
        ChangeToGameScene();
    }

    private void ChangeToGameScene()
    {
        loadingOperation = SceneManager.LoadSceneAsync("MainGameScene");

        HideCharacterSelect();
        loadingScreenPanel.Show(loadingOperation);
    }

    public void BackToMenu()
    {
        // Enable background and menu
        backgroundImage.enabled = true;
        OverlayControls.SetCanvasState(true, startMenu);

        HideCharacterSelect();
    }

    private void HideCharacterSelect()
    {
        // Disable all selections
        OverlayControls.SetCanvasState(false, canvasGroup);
        OverlayControls.SetCanvasState(false, startGamePanel);
        startGameButton.interactable = false;
        foreach (CharacterSelectButton button in characterSelectButtons)
            button.SetLights(false);
    }
}
