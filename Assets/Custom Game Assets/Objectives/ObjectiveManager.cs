using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectiveManager : MonoBehaviour
{
    public Objective objective;
 
    public Goal goal;
    public GameObject ObjectiveWindow;
    public Text titleText;
    public Text descriptionText;
    public OverlayControls controlls;
    public void SetUpQuest()
    {
        titleText.text = objective.title;
        descriptionText.text = objective.description;
        controlls.ObjectiveMenu();
    }
    public void AcceptQuest()
    {
        objective.isActive = true;
        controlls.ObjectiveMenu();
    }
    public void FinishQuest()
    {
        objective.isActive = false;
        Debug.Log("Objective Completed");
   
    }
    public void Update()
    {
        if (objective.horde.enemies.Count == 0 && objective.isActive)
        {
            FinishQuest();
        }
    }
  
}
