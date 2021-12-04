using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;
    PlayerControl player;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetIsMovingDown())
        {
            anim.SetBool("MovingDown", true);
        }
        else
        {
            anim.SetBool("MovingDown", false);
        }

        if (player.GetIsMovingUp())
        {
            anim.SetBool("MovingUp", true);
        }
        else
        {
            anim.SetBool("MovingUp", false);
        }
    }
}
