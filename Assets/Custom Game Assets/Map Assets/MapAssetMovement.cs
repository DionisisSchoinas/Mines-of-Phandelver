using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAssetMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(1, 0, 0);
        controller.Move(move * Time.deltaTime * 12f);
    }
}
