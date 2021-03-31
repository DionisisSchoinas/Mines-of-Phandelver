using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerGoblinsWell : MonoBehaviour
{
    public Animator animator;
    float timer;
    


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Random.Range(3f, 10f))
        {
            timer=0f;
            int index = Random.Range(1, 4);
      
            switch (index)
            {
                case 1:
                    animator.SetTrigger("Taunt");
                    break;
                case 2:
                    animator.SetTrigger("Taunt Battlecry");
                    break;
                case 3:
                    animator.SetTrigger("Point");
                    break;
            }
        }
    }
}
