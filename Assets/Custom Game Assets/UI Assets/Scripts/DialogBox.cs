using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogBox
{
    public string speaker;
    public string dialog;
}

[System.Serializable]
public class EntireDialog 
{
    public List<DialogBox> dialogBoxes;
}
