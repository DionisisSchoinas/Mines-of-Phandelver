using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLogic : MonoBehaviour
{
    public HordeLogic hordeLogic;
    public GameObject questionMark;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] transforms = GameObject.FindGameObjectsWithTag("Player");
        if (transforms.Length != 0)
            target = transforms[0].transform;
        questionMark.SetActive(false);
    }

    public void RotateTowardsPlayer()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.eulerAngles = rotation.eulerAngles;
    }
}
