using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxManager : MonoBehaviour
{
    public List<EntireDialog> entireDialogs;

    private CanvasGroup canvasGroup;
    private Text nameText;
    private Text text;
    private SpriteSwapper blinkingButton;

    private int currentDialog;
    private int currentLine;

    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        OverlayControls.SetCanvasState(false, canvasGroup);

        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        nameText = texts[0];
        text = texts[1];

        blinkingButton = gameObject.GetComponentInChildren<SpriteSwapper>();
        blinkingButton.StopSwapping();

        currentDialog = -1;
        currentLine = -1;
    }

    private void Start()
    {
        UIEventSystem.current.onShowDialog += StartDialog;
    }

    private void OnDestroy()
    {
        UIEventSystem.current.onShowDialog -= StartDialog;
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha == 1 &&  Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextDialog();
        }
    }

    private void ShowNextDialog()
    {
        currentLine++;
        // If there are still lines
        if (currentLine < entireDialogs[currentDialog].dialogBoxes.Count)
        {
            nameText.text = entireDialogs[currentDialog].dialogBoxes[currentLine].speaker;
            text.text = entireDialogs[currentDialog].dialogBoxes[currentLine].dialog;
            return;
        }

        HideDialog();
    }

    public void StartDialog(int dialogIndex)
    {
        currentDialog = dialogIndex;
        currentLine = -1;
        ShowNextDialog();

        blinkingButton.StartSwapping();
        OverlayControls.SetCanvasState(true, canvasGroup);
    }

    private void HideDialog()
    {
        UIEventSystem.current.FinishedDialog(currentDialog);

        currentLine = -1;
        currentDialog = -1;
        blinkingButton.StopSwapping();
        OverlayControls.SetCanvasState(false, canvasGroup);
    }
}
