using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Attacks : MonoBehaviour
{
    [SerializeField] GameObject laughPrefab;
    [SerializeField] float laughSpeed;
    //hundred = second
    [SerializeField] int minLaughRateOfFire;
    [SerializeField] int maxLaughRateOfFire;

    [SerializeField] GameObject tornadoPrefab;
    [SerializeField] float tornadoSpeed;
    //hundred = second
    [SerializeField] int minTornadoRateOfFire;
    [SerializeField] int maxTornadoRateOfFire;

    [SerializeField] GameObject player;
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

        laugh.GetComponent<Rigidbody2D>().velocity = transform.right * laughSpeed * -1;

        Destroy(laugh, 12);
    }

    void SummonTornado()
    {
        var tornado = Instantiate(tornadoPrefab, transform.position, Quaternion.Euler(0, 0, 0));

        tornado.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * laughSpeed;

        Destroy(tornado, 12);
    }
}
