using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxManager : MonoBehaviour
{
    public bool active;
    public MovementLockManager lockManager;
    public CanvasGroup dialogBox;
    public Text textBox;
    public Text blinkingPrompt;
    public List<DialogBox> dialogBoxes;
    private int counter = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (dialogBox != null)
            SetCanvasState(false, dialogBox);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            showNextDialog();
        }
    }

    void showNextDialog()
    {
        counter++;
        if (counter < dialogBoxes.Count)
        {
            textBox.text = dialogBoxes[counter].dialog;
        }
        else
        {
            SetCanvasState(false, dialogBox);
            lockManager.lockMovement = false;
        }
    }

    void SetCanvasState(bool show, CanvasGroup canvasGroup)
    {
        if (show)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void StartDialog()
    {
        SetCanvasState(true, dialogBox);
        textBox.text = dialogBoxes[counter].dialog;
        lockManager.lockMovement = true;
    }



}
