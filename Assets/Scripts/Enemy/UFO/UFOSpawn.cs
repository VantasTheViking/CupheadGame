using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UFOSpawn : MonoBehaviour
{
    [Tooltip("UFO prefab (GameObject)")]
    [SerializeField] private GameObject _UFOPrefab;

    [Tooltip("Spawn point of UFO (GameObject)")]
    [SerializeField] public GameObject _spawnPoint;

    [Tooltip("Time between plane spawns (float)")]
    [SerializeField] private float _spawnDelay;

    [Tooltip("Speed of spawned plane (float)")]
    [SerializeField] public float _UFOSpeed;

    private float timeToNext = 0;

    // Update is called once per frame
    void Update()
    {
        if (canSpawn())
        {
            spawnUFO();
        }
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

    void spawnUFO()
    {
        var Plane = Instantiate(_UFOPrefab, new Vector3(_spawnPoint.transform.position.x + 5, _spawnPoint.transform.position.y, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        Plane.GetComponent<Rigidbody2D>().velocity = -transform.right * _UFOSpeed;
    }
}
