using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalController : MonoBehaviour
{
    [Tooltip("The fraction of max health/x to change to phase 2 (float)")]
    [SerializeField] private float _phase2Divider;

    [Tooltip("The fraction of max health/x to change to phase 3 (float)")]
    [SerializeField] private float _phase3Divider;

    int _health;
    int maxHealth;

    bool phase1;
    bool phase2;
    bool phase3;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = GetComponent<BossHealth>().getHealth();
    }

    // Update is called once per frame
    void Update()
    {
        _health = GetComponent<BossHealth>().getHealth();

        if (_health <= 0)
        {
            Destroy(gameObject, 0);
        }

        if (maxHealth / _phase2Divider <= _health && (!phase2 || !phase3))
        {
            endPhase1();
            startPhase2();
        }

        if(maxHealth / _phase3Divider <= _health && (!phase1 || !phase3))
        {
            endPhase2();
            startPhase3();
        }
    }

    void endPhase1()
    {
        phase1 = false;

        //gameObject.GetComponent<Phase1Attacks>().enabled = false;
    }

    void endPhase2()
    {
        phase2 = false;

        gameObject.GetComponent<Phase2Attacks>().enabled = false;
    }

    void startPhase2()
    {
        phase2 = true;

        gameObject.GetComponent<Phase2Attacks>().enabled = true;
    }

    void startPhase3()
    {
        phase3 = true;

        //gameObject.GetComponent<Phase3Attacks>().enabled = true;
    }
}
