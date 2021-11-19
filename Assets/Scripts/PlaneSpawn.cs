using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaneSpawn : MonoBehaviour
{
    [Tooltip("Plane prefab (GameObject)")]
    [SerializeField] private GameObject _planePrefab;

    [Tooltip("Top of spawn range (GameObject)")]
    [SerializeField] private GameObject _top;

    [Tooltip("Bottom of spawn range (GameObject)")]
    [SerializeField] private GameObject _bottom;

    [Tooltip("Time between plane spawns (float)")]
    [SerializeField] private float _spawnDelay;

    [Tooltip("Speed of spawned plane (float)")]
    [SerializeField] private float _planeSpeed;

    private float timeToNext = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn())
        {
            spawnPlane();
        }
    }

    void spawnPlane()
    {
        float x = Random.Range(_bottom.transform.position.x, _top.transform.position.x);
        float y = Random.Range(_bottom.transform.position.y, _top.transform.position.y);
        //Debug.Log($"{x}, {y}, 0");

        var Plane = Instantiate(_planePrefab, new Vector3(x, y, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        Plane.GetComponent<Rigidbody2D>().velocity = -transform.right * _planeSpeed;

        //Destroy(Plane, 8);
    }

    bool canSpawn()
    {
        if (timeToNext < Time.realtimeSinceStartup)
        {
            timeToNext = Time.realtimeSinceStartup + _spawnDelay;
            return true;
        }
        else
        {
            return false;
        }
    }
}
