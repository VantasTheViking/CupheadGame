using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalController : MonoBehaviour
{
    [Tooltip("The fraction of max health/x to change phase (float)")]
    [SerializeField] private float _phaseDivider;
    CameraBorder cam;

    GameObject hildaObject;
    [SerializeField] GameObject background;

    int _health;
    int maxHealth;

    bool phase1;
    bool phase2;
    bool phase3;

    public float darkTimer;
    public bool nightTime;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = GetComponent<BossHealth>().getHealth();

        hildaObject = GameObject.Find("Hilda");
        cam = GameObject.Find("Main Camera").GetComponent<CameraBorder>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _health = GetComponent<BossHealth>().getHealth();

        if (_health <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
            GameObject.Find("Player").GetComponent<PolygonCollider2D>().enabled = false;

            gameObject.GetComponent<UFOSpawn>().enabled = false;
            gameObject.GetComponent<Phase3Attacks>().enabled = false;
        }

        if (maxHealth - (maxHealth / _phaseDivider) >= _health && (!phase2 || !phase3))
        {
            endPhase1();
            startPhase2();
        }

        if(maxHealth - (maxHealth / _phaseDivider)*2 >= _health && (!phase1 || !phase3))
        {
            endPhase2();
            startPhase3();

            if (!nightTime)
            {
                DarkenScene();
            }
            else
            {
                cam.SetShader(0);
                Destroy(background);
            }

            
        }
    }

    void endPhase1()
    {
        phase1 = false;
        gameObject.GetComponent<Phase1Attacks>().enabled = false;

        gameObject.GetComponents<PolygonCollider2D>()[0].enabled = false;
        gameObject.GetComponents<PolygonCollider2D>()[1].enabled = true;

        //since we used multiple components I think this is necessary
        for(int i = 0; i < hildaObject.GetComponents<EnemyPlaneSpawn>().Length; i++)
        {
            hildaObject.GetComponents<EnemyPlaneSpawn>()[i].enabled = false;
        }

    }

    

    void startPhase2()
    {
        phase2 = true;

        gameObject.GetComponent<Phase2Attacks>().enabled = true;
    }

    void endPhase2()
    {
        phase2 = false;

        gameObject.GetComponent<Phase2Attacks>().enabled = false;

        gameObject.GetComponents<PolygonCollider2D>()[1].enabled = false;
        gameObject.GetComponents<PolygonCollider2D>()[2].enabled = true;

        gameObject.transform.position = new Vector3(3, 0, 0);
        gameObject.transform.localScale = new Vector3(3, 3, 1);
    }

    void startPhase3()
    {
        phase3 = true;
        gameObject.GetComponent<Phase3Attacks>().enabled = true;
        hildaObject.GetComponent<UFOSpawn>().enabled = true;

    }

    void DarkenScene()
    {
        darkTimer += Time.deltaTime;

        cam.SetShader(darkTimer/5);

        if (darkTimer > 3f)
        {
            nightTime = true;
        }
    }

    public int GetPhase()
    {
        if (phase2)
        {
            return 2;
        }
        if (phase3)
        {
            return 3;
        }
        else
        {
            return 1;
        }
    }
}
