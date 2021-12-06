using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3Attacks : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
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
        Vector3 starSpawn = new Vector3(GameObject.Find("TopSpawn").transform.position.x, Random.Range(GameObject.Find("BottomSpawn").transform.position.y, GameObject.Find("TopSpawn").transform.position.y), 0);
        var star = Instantiate(starPrefab, starSpawn, Quaternion.Euler(0, 0, 0));

        star.GetComponent<Rigidbody2D>().velocity = transform.right * starSpeed * -1;

        Destroy(star, 12);
    }
    bool CanSummonStar()
    {
        int randomRateofFire = Random.Range(minStarRateOfFire, maxStarRateOfFire);
        
        if (starDelay < Time.realtimeSinceStartup)
        {
            starDelay = Time.realtimeSinceStartup + (randomRateofFire / 100);
            return true;
        }
        else
        {
            return false;
        }
    }
}
