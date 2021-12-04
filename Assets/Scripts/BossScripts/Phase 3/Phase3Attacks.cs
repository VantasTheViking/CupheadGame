using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3Attacks : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] GameObject starSpawn;
    [SerializeField] float starSpeed;
    //hundred = second
    [SerializeField] int minStarRateOfFire;
    [SerializeField] int maxStarRateOfFire;

    float starDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSummonStar())
        {
            SummonStars();
        }
    }

    void SummonStars()
    {
        var star = Instantiate(starPrefab, starSpawn.transform.position, Quaternion.Euler(0, 0, 0));

        star.GetComponent<Rigidbody2D>().velocity = transform.right * starSpeed * -1;

        Destroy(star, 12);
    }
    bool CanSummonStar()
    {
        int randomRateofFire = Random.Range(minStarRateOfFire, maxStarRateOfFire);
        
        if (starDelay < Time.realtimeSinceStartup)
        {
            //Debug.Log(waterBulletsLeft);
            starDelay = Time.realtimeSinceStartup + (randomRateofFire / 100);
            return true;
        }
        else
        {
            return false;
        }
    }
}
