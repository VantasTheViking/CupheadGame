using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Attacks : MonoBehaviour
{
    [Tooltip("Parent of aimer object (GameObject)")]
    [SerializeField] GameObject aimerChild;

    [Tooltip("Aimer object (GameObject)")]
    [SerializeField] GameObject aimerParent;

    [Tooltip("rotation per frame in degrees (float)")]
    [SerializeField] float rateOfRotation;

    [Tooltip("Amount seconds between each bullet (float)")]
    [SerializeField] float waveOfBullets_RateOfFire;

    [Tooltip("Prefab for bullet (GameObject)")]
    [SerializeField] GameObject waterBulletPrefab;

    [Tooltip("Speed of bullet (float)")]
    [SerializeField] float waterBulletSpeed;

    [Tooltip("Amount seconds between each meteor (float)")]
    [SerializeField] float bubbleMeteorFrequency;

    [Tooltip("Prefab for meteor (GameObject)")]
    [SerializeField] GameObject bubbleMeteorPrefab;

    [Tooltip("Speed of meteor (float)")]
    [SerializeField] float bubbleMeteorSpeed;

    [Tooltip("Spawn position 1 of metoer (GameObject)")]
    [SerializeField] GameObject bubbleMeteorSpawn1;

    [Tooltip("Spawn position 2 of metoer (GameObject)")]
    [SerializeField] GameObject bubbleMeteorSpawn2;

    [Tooltip("Amount seconds between each fire support bullet (float)")]
    [SerializeField] float fireSupportRateOfFire;

    [Tooltip("Spawner 1 of fire support object (GameObject)")]
    [SerializeField] GameObject fireSupportSpawn1;

    [Tooltip("Spawner 2 of fire support object (GameObject)")]
    [SerializeField] GameObject fireSupportSpawn2;


    float shotDelay = 0;
    float meteorShotDelay = 0;
    float fireSupportDelay = 0;

    bool meteorActivated = true;
    bool bubbleActivated = true;
    bool fireSupportActivated = true;

    

    int randomNumber;

    private void Start()
    {

        //First number is for initial delay. Second number is for recurring delay.
        InvokeRepeating("RandomEvent", 4, 8);
        
    }
    // Update is called once per frame
    void Update()
    {
        
        RotateAimer();

        if (waveOfBullets_CanShoot())
        {
            ShootAtAimer();
        }
        if (CanSummonBubbleMeteor())
        {
            SummonBubbleMeteor();
        }
        if (fireSupport_CanShoot())
        {
            ShootFireSupport();
        }

        
    }

    

    void RandomEvent()
    {
        meteorActivated = false;
        bubbleActivated = false;
        fireSupportActivated = false;

        randomNumber = Random.Range(1, 4);
        Debug.Log("RandomNumber:");
        Debug.Log(randomNumber);
        switch (randomNumber)
        {
            case 1:
                bubbleActivated = true;
                meteorActivated = true;
                break;

            case 2:
                bubbleActivated = true;
                fireSupportActivated = true;
                break;

            case 3:
                fireSupportActivated = true;
                meteorActivated = true;
                break;

        }
    }

    

    void RotateAimer()
    {
        aimerParent.transform.Rotate(Vector3.forward * Time.deltaTime * rateOfRotation);
    }



    bool waveOfBullets_CanShoot()
    {
        if(shotDelay < Time.realtimeSinceStartup && bubbleActivated == true)
        {
            //Debug.Log(waterBulletsLeft);
            shotDelay = Time.realtimeSinceStartup + waveOfBullets_RateOfFire;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ShootAtAimer()
    {
        var bullet = Instantiate(waterBulletPrefab, transform.position, aimerParent.transform.rotation);
        bullet.transform.Rotate(Vector3.forward * 90);

        bullet.GetComponent<Rigidbody2D>().velocity = aimerChild.transform.right * -1 * waterBulletSpeed;

        Destroy(bullet, 10);
    }

    public void SummonBubbleMeteor()
    {
        float bubbleXPos = Random.Range(bubbleMeteorSpawn1.transform.position.x, bubbleMeteorSpawn2.transform.position.x);
        float bubbleYPos = Random.Range(bubbleMeteorSpawn1.transform.position.y, bubbleMeteorSpawn2.transform.position.y);

        var bubble = Instantiate(bubbleMeteorPrefab, new Vector2(bubbleXPos,bubbleYPos), Quaternion.Euler(0,0,0));
        
        bubble.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.33f,-0.66f) * bubbleMeteorSpeed;

        Destroy(bubble, 12);
    }
    bool CanSummonBubbleMeteor()
    {
        if (meteorShotDelay < Time.realtimeSinceStartup && meteorActivated == true)
        {
            meteorShotDelay = Time.realtimeSinceStartup + bubbleMeteorFrequency;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShootFireSupport()
    {
        var bullet1 = Instantiate(waterBulletPrefab, fireSupportSpawn1.transform.position, fireSupportSpawn1.transform.rotation);
        bullet1.transform.Rotate(Vector3.forward * 90);

        bullet1.GetComponent<Rigidbody2D>().velocity = transform.right * -1 * waterBulletSpeed;

        Destroy(bullet1, 10);


        var bullet2 = Instantiate(waterBulletPrefab, fireSupportSpawn2.transform.position, fireSupportSpawn2.transform.rotation);
        bullet2.transform.Rotate(Vector3.forward * 90);

        bullet2.GetComponent<Rigidbody2D>().velocity = transform.right * -1 * waterBulletSpeed;

        Destroy(bullet2, 12);
    }

    bool fireSupport_CanShoot()
    {
        if (fireSupportDelay < Time.realtimeSinceStartup && fireSupportActivated == true)
        {
            
            fireSupportDelay = Time.realtimeSinceStartup + fireSupportRateOfFire;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetIsSpiral()
    {
        return bubbleActivated;
    }
}

