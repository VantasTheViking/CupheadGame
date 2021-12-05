using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3Attacks : MonoBehaviour
{
    [Tooltip("Star prefab (GameObject)")]
    [SerializeField] GameObject starPrefab;

    [Tooltip("Speed of the star (float)")]
    [SerializeField] float starSpeed;

    [Tooltip("Time between firing a star in x/100 seconds (float)")]
    [SerializeField] int minStarRateOfFire;

    [Tooltip("Time between firing a star in x/100 seconds (float)")]
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
        Vector3 starSpawn = new Vector3(GameObject.Find("TopSpawn").transform.position.x, Random.Range(GameObject.Find("BottomSpawn").transform.position.y, GameObject.Find("TopSpawn").transform.position.y), 0); 

        var star = Instantiate(starPrefab, starSpawn, Quaternion.Euler(0, 0, 0));

        star.GetComponent<Rigidbody2D>().velocity = -transform.right * starSpeed;

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
