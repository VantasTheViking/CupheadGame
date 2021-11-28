using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject aimerChild;
    [SerializeField] GameObject aimerParent;


    [SerializeField] float rateOfRotation;

    float shotDelay = 0;
    [SerializeField] float waveOfBullets_RateOfFire;
    [SerializeField] GameObject waterBulletPrefab;
    [SerializeField] float waterBulletSpeed;
    [SerializeField] int noOfReloadWaterBullets;
    int waterBulletsLeft = 0;

    [SerializeField] float bubbleMeteorFrequency;
    [SerializeField] GameObject bubbleMeteorPrefab;
    [SerializeField] float bubbleMeteorSpeed;
    [SerializeField] int noOfReloadBubbleMeteor;
    int bubbleMeteorsLeft = 0;
    [SerializeField] GameObject bubbleMeteorSpawn1;
    [SerializeField] GameObject bubbleMeteorSpawn2;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RotateAimer();

        BossEvents();


        if (waveOfBullets_CanShoot())
        {
            ShootAtAimer();
        }
        if (CanSummonBubbleMeteor())
        {
            SummonBubbleMeteor();
        }

    }

    void BossEvents()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddWaterBullets();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddBubbleMeteors();
        }
    }


    void AddWaterBullets()
    {
        waterBulletsLeft += noOfReloadWaterBullets;
        aimerParent.transform.rotation = Quaternion.Euler(0,0,-90);
    }

    void AddBubbleMeteors()
    {
        bubbleMeteorsLeft += noOfReloadBubbleMeteor;
    }

    void RotateAimer()
    {
        
        aimerParent.transform.Rotate(Vector3.forward * Time.deltaTime * rateOfRotation);
    }

    bool waveOfBullets_CanShoot()
    {
        if((shotDelay < Time.realtimeSinceStartup) && (waterBulletsLeft > 0))
        {
            Debug.Log(waterBulletsLeft);
            shotDelay = Time.realtimeSinceStartup + waveOfBullets_RateOfFire;
            return true;
        }
        else
        {
            return false;
        }
    }
    void ShootAtAimer()
    {
        var bullet = Instantiate(waterBulletPrefab, transform.position, aimerParent.transform.rotation);
        bullet.transform.Rotate(Vector3.forward * 90);

        bullet.GetComponent<Rigidbody2D>().velocity = (aimerChild.transform.position - transform.position).normalized * waterBulletSpeed;
        waterBulletsLeft -= 1;

        Destroy(bullet, 7);
    }

    void SummonBubbleMeteor()
    {
        float bubbleXPos = Random.Range(bubbleMeteorSpawn1.transform.position.x, bubbleMeteorSpawn2.transform.position.x);
        float bubbleYPos = Random.Range(bubbleMeteorSpawn1.transform.position.y, bubbleMeteorSpawn2.transform.position.y);

        var bubble = Instantiate(bubbleMeteorPrefab, new Vector2(bubbleXPos,bubbleYPos), Quaternion.Euler(0,0,0));
        

        bubble.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.33f,-0.66f) * bubbleMeteorSpeed;
        bubbleMeteorsLeft -= 1;

        Destroy(bubble, 10);
    }
    bool CanSummonBubbleMeteor()
    {
        if ((shotDelay < Time.realtimeSinceStartup) && (bubbleMeteorsLeft > 0))
        {
            
            shotDelay = Time.realtimeSinceStartup + bubbleMeteorFrequency;
            return true;
        }
        else
        {
            return false;
        }
    }
}

