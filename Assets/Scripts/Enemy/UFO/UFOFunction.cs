using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UFOFunction : MonoBehaviour
{
    [Tooltip("Prefab for laser (GameObject)")]
    [SerializeField] private GameObject _laserPrefab;

    [Tooltip("Laser spawnpoint (GameObject)")]
    [SerializeField] private GameObject _laserSpawn;

    [Tooltip("Layer to collide with (int)")]
    [SerializeField] private int _layer;

    [Tooltip("The x cordinate for the laser to stop at (float)")]
    [SerializeField] private float _despawn;

    float initialY;
    Transform _trans;
    GameObject laser;
    float initialTime;
    bool shot = false;

    // Start is called before the first frame update
    void Start()
    {
        initialY = GameObject.Find("Hilda").GetComponent<UFOSpawn>()._spawnPoint.transform.position.y;
        initialTime = Time.realtimeSinceStartup;
        _trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_trans.position.x < -14)
        {
            Destroy(gameObject, 0);
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialY + Mathf.Sin(Time.realtimeSinceStartup * 6)/4, gameObject.transform.position.z);

        StartCoroutine(waitToShoot((float)(Random.Range(1000, 1600) / 100)));


        gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialY + Mathf.Sin(Time.realtimeSinceStartup * 6) / 4, gameObject.transform.position.z);
    }

    IEnumerator waitToShoot(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (!shot)
        {
            Shoot();
            shot = true;
            StartCoroutine(WaitToDespawn(1f));
        }
    }

    IEnumerator WaitToDespawn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(laser);
    }

    void Shoot()
    {
        laser = Instantiate(_laserPrefab, _laserSpawn.transform.position, Quaternion.Euler(0,0,0));
        laser.transform.parent = gameObject.transform;
        //laser.transform.localScale = new Vector3(0.5f, 22f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == _layer)
        {
            Destroy(gameObject, 0);
        }
    }
}