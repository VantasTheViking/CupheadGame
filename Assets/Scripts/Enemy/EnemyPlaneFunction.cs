using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPlaneFunction : MonoBehaviour
{
    [Tooltip("Bullet prefab (GameObject)")]
    [SerializeField] private GameObject _bulletPrefab;

    [Tooltip("Bullet spawn position (GameObject)")]
    [SerializeField] private GameObject _bulletSpawn;

    [Tooltip("Speed of spawned bullet (float)")]
    [SerializeField] private float _bulletSpeed;

    [Tooltip("Speed of plane, keep consistent with speed on spawn object (float)")]
    float _planeSpeed;

    [Tooltip("This Game Object (GameObject)")]
    [SerializeField] private GameObject _self;


    private bool shot = false;

    Transform _trans;
    Rigidbody2D _rigid;
    GameObject _position;

    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
        _rigid = GetComponent<Rigidbody2D>();
        _planeSpeed = Camera.main.GetComponent<EnemyPlaneSpawn>()._planeSpeed;
        _position = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    { 
        if (_trans.position.x < 1.4f && !shot)
        {
            _rigid.velocity = new Vector3(0, 0, 0);
            StartCoroutine(waitToShoot(1));

        }

        if (shot)
        {
            StartCoroutine(waitToMove(3));
        }

        if(_trans.position.x > 13)
        {
            Destroy(_self, 0);
        }
    }

    IEnumerator waitToMove(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        _trans.rotation = Quaternion.Euler(0, 180, 90);
        _rigid.velocity = transform.up * _planeSpeed;
    }

    IEnumerator waitToShoot(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (!shot)
        {
            shoot();
        }
    }

    void shoot()
    {
        if (this.tag.Equals("Purple Enemy"))
        {
            var Bullet = Instantiate(_bulletPrefab, _bulletSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));

            Vector3 velocity = _position.transform.position - _bulletSpawn.transform.position;
            Bullet.GetComponent<Rigidbody2D>().velocity = velocity * _bulletSpeed;
            Destroy(Bullet, 5);
        }

        else if (this.tag.Equals("Green Enemy"))
        {
            float spreadAngle = 20;

            for (int i = 0; i < 4; i++)
            {
                var Bullet = Instantiate(_bulletPrefab, _bulletSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));

                var dir = _position.transform.position - _bulletSpawn.transform.position;
                float angle = 0;

                switch (i)
                {
                    case 0:
                        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - (spreadAngle / (float) 2.5);
                        break;
                    case 1:
                        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - spreadAngle * (float)1.25;
                        break;
                    case 2:
                        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + (spreadAngle / (float) 2.5);
                        break;
                    case 3:
                        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + spreadAngle * (float)1.25;
                        break;
                    default:
                        break;
                }

                
                Bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


                Vector3 velocity = Bullet.transform.right * 5;

                Bullet.GetComponent<Rigidbody2D>().velocity = velocity * _bulletSpeed;
                Destroy(Bullet, 5);

                
            }
            
            

        }

        shot = true;
    }
}
