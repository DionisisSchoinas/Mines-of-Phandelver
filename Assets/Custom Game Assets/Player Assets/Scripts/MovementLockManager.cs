using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLockManager : MonoBehaviour
{
    PlayerMovementScriptWarrior playerController;
    public bool lockMovement;
    bool locked;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerMovementScriptWarrior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lockMovement && !locked)
        {
            playerController.enabled = false;
            locked = true;
        }
        else
        {
            playerController.enabled = true;
            locked = false;
        }
    }
}
