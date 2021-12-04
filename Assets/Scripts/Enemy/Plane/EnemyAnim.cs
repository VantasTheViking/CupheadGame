using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    EnemyPlaneFunction enemy;
    Animator anim;
    private void Start()
    {
        enemy = GetComponent<EnemyPlaneFunction>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemy.GetIsShooting())
        {
            anim.SetBool("Shoot", true);
        }
        else
        {
            anim.SetBool("Shoot", false);
        }
    }
}
