using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaneMovement : MonoBehaviour
{
    [Tooltip("Bullet prefab (GameObject)")]
    [SerializeField] private GameObject _bulletPrefab;

    [Tooltip("Bullet spawn position (GameObject)")]
    [SerializeField] private GameObject _bulletSpawn;

    [Tooltip("Speed of spawned bullet (float)")]
    [SerializeField] private float _bulletSpeed;

    [Tooltip("Speed of plane, keep consistent with speed on spawn object (float)")]
    [SerializeField] private float _planeSpeed;

    [Tooltip("This Game Object (GameObject)")]
    [SerializeField] private GameObject _self;

    private bool shot = false;

    Transform _trans;
    Rigidbody2D _rigid;
    Vector3 _position;

    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (_trans.position.x < 1.5f && !shot)
        {
            _rigid.velocity = new Vector3(0, 0, 0);
            shoot();
        }

        if (shot)
        {
            StartCoroutine(wait(5));
        }

        if(_trans.position.x > 13)
        {
            Destroy(_self, 0);
        }
    }

    IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        _rigid.velocity = -transform.up * _planeSpeed;
    }

    void shoot()
    {
        _position = GameObject.Find("Player").transform.position;
        var Bullet = Instantiate(_bulletPrefab, _bulletSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));

        Vector3 velocity = _position - _bulletSpawn.transform.position;
        Bullet.GetComponent<Rigidbody2D>().velocity = velocity * _bulletSpeed;
        Destroy(Bullet, 5);

        shot = true;
    }
}
