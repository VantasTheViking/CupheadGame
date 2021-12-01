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

    [Tooltip("Number of meteors to spawn per spawn (int)")]
    [SerializeField] int meteorCount;

    [Tooltip("Prefab for meteor (GameObject)")]
    [SerializeField] GameObject bubbleMeteorPrefab;

    [Tooltip("Speed of meteor (float)")]
    [SerializeField] float bubbleMeteorSpeed;

    [Tooltip("Spawn position 1 of metoer (GameObject)")]
    [SerializeField] GameObject bubbleMeteorSpawn1;

    [Tooltip("Spawn position 2 of metoer (GameObject)")]
    [SerializeField] GameObject bubbleMeteorSpawn2;

    float shotDelay = 0;
    float meteorShotDelay = 0;

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
            for(int i = 0; i <= meteorCount; i++)
            {
                StartCoroutine(waitForMeteor(((float)Random.Range(20, 150))/100));
            }
        }

    }

    IEnumerator waitForMeteor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SummonBubbleMeteor();
    }

    void RotateAimer()
    {
        aimerParent.transform.Rotate(Vector3.forward * Time.deltaTime * rateOfRotation);
    }

    bool waveOfBullets_CanShoot()
    {
        if(shotDelay < Time.realtimeSinceStartup)
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

        bullet.GetComponent<Rigidbody2D>().velocity = GameObject.Find("Aimer").transform.right * waterBulletSpeed * -1;

        Destroy(bullet, 7);
    }

    public void SummonBubbleMeteor()
    {
        float bubbleXPos = Random.Range(bubbleMeteorSpawn1.transform.position.x, bubbleMeteorSpawn2.transform.position.x);
        float bubbleYPos = Random.Range(bubbleMeteorSpawn1.transform.position.y, bubbleMeteorSpawn2.transform.position.y);

        var bubble = Instantiate(bubbleMeteorPrefab, new Vector2(bubbleXPos,bubbleYPos), Quaternion.Euler(0,0,0));
        
        bubble.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.33f,-0.66f) * bubbleMeteorSpeed;

        Destroy(bubble, 10);
    }
    bool CanSummonBubbleMeteor()
    {
        if (meteorShotDelay < Time.realtimeSinceStartup)
        {
            meteorShotDelay = Time.realtimeSinceStartup + bubbleMeteorFrequency;
            return true;
        }
        else
        {
            return false;
        }
    }
}

