using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)){
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }


        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Speed_jump");
        }
    }
}
