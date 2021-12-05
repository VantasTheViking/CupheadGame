using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Attacks : MonoBehaviour
{
    [Tooltip("Star prefab (GameObject)")]
    [SerializeField] GameObject laughPrefab;

    [Tooltip("Speed of the laugh (float)")]
    [SerializeField] float laughSpeed;

    [Tooltip("Time between firing a laugh in x/100 seconds (float)")]
    [SerializeField] int minLaughRateOfFire;

    [Tooltip("Time between firing a laugh in x/100 seconds (float)")]
    [SerializeField] int maxLaughRateOfFire;

    [Tooltip("Star prefab (GameObject)")]
    [SerializeField] GameObject tornadoPrefab;

    [Tooltip("Speed of the laugh (float)")]
    [SerializeField] float tornadoSpeed;

    [Tooltip("Time between firing a tornado in x/100 seconds (float)")]
    [SerializeField] int minTornadoRateOfFire;

    [Tooltip("Time between firing a tornado in x/100 seconds (float)")]
    [SerializeField] int maxTornadoRateOfFire;

    float laughDelay = 1;
    float tornadoDelay = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanLaugh())
        {
            Laugh();
        }
        if (CanSummonTornado())
        {
            SummonTornado();
        }
    }
    bool CanLaugh()
    {
        int randomRateofFire = Random.Range(minLaughRateOfFire, maxLaughRateOfFire);

        if (laughDelay < Time.realtimeSinceStartup)
        {
            //Debug.Log(waterBulletsLeft);
            laughDelay = Time.realtimeSinceStartup + (randomRateofFire / 100);
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CanSummonTornado()
    {
        int randomRateofFire = Random.Range(minTornadoRateOfFire, maxTornadoRateOfFire);

        if (tornadoDelay < Time.realtimeSinceStartup)
        {
            //Debug.Log(waterBulletsLeft);
            tornadoDelay = Time.realtimeSinceStartup + (randomRateofFire / 100);
            return true;
        }
        else
        {
            return false;
        }
    }

    void Laugh()
    {
        var laugh = Instantiate(laughPrefab, transform.position, Quaternion.Euler(0, 0, 0));

        laugh.GetComponent<Rigidbody2D>().velocity = -transform.right * laughSpeed;

        Destroy(laugh, 12);
    }

    void SummonTornado()
    {
        var tornado = Instantiate(tornadoPrefab, transform.position, Quaternion.Euler(0, 0, 0));

        tornado.GetComponent<Rigidbody2D>().velocity = (GameObject.Find("Player").transform.position - transform.position).normalized * tornadoSpeed;

        Destroy(tornado, 12);
    }
}
