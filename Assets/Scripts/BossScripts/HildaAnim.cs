using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HildaAnim : MonoBehaviour
{
    Animator anim;
    Phase1Attacks phase1;
    Phase2Attacks phase2;
    Phase3Attacks phase3;
    GlobalController global;
    UFOSpawn UFO;

    void Start()
    {
        anim = GetComponent<Animator>();
        phase1 = GetComponent<Phase1Attacks>();
        phase3 = GetComponent<Phase3Attacks>();
        global = GetComponent<GlobalController>();
        UFO = GetComponent<UFOSpawn>();
    }

    void Update()
    {
        if (phase1.GetIsLaughing())
        {
            anim.SetBool("Laughing", true);
        }
        else
        {
            anim.SetBool("Laughing", false);
        }

        if (UFO.GetIsSpawning())
        {
            anim.SetBool("UFO", true);
        }
        else
        {
            anim.SetBool("UFO", false);
        }

        switch (global.GetPhase())
        {
            case 2:
                anim.SetBool("Phase2", true);
                break;
            case 3:
                anim.SetBool("Phase3", true);
                break;
            default:
                break;
        }
    }
}
