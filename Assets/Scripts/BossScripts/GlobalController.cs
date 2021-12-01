using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalController : MonoBehaviour
{
    [Tooltip("The fraction of max health/x to change phase (float)")]
    [SerializeField] private float _phaseDivider;

    GameObject hildaObject;

    int _health;
    int maxHealth;

    bool phase1;
    bool phase2;
    bool phase3;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = GetComponent<BossHealth>().getHealth();
        //Debug.Log(maxHealth);

        hildaObject = GameObject.Find("Hilda");
    }

    // Update is called once per frame
    void Update()
    {
        _health = GetComponent<BossHealth>().getHealth();

        if (_health <= 0)
        {
            Destroy(gameObject, 0);
        }

        if (maxHealth - (maxHealth / _phaseDivider) >= _health && (!phase2 || !phase3))
        {
            Debug.Log("Phase 2 start");
            endPhase1();
            startPhase2();
        }

        if(maxHealth - (maxHealth / _phaseDivider)*2 >= _health && (!phase1 || !phase3))
        {
            Debug.Log("Phase 3 start");
            endPhase2();
            startPhase3();
        }
    }

    void endPhase1()
    {
        phase1 = false;
        //since we used multiple components I think this is necessary
        for(int i = 0; i < hildaObject.GetComponents<EnemyPlaneSpawn>().Length; i++)
        {
            hildaObject.GetComponents<EnemyPlaneSpawn>()[i].enabled = false;
        }

        //remove comment once Phase1Attacks is made
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
        hildaObject.GetComponent<UFOSpawn>().enabled = true;

        //remove comment once Phase1Attacks is made
        //gameObject.GetComponent<Phase3Attacks>().enabled = true;
    }
}
