
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


    private bool shot = false;
    private float _planeSpeed;
    private float randomShootX;

    Transform _trans;
    Rigidbody2D _rigid;
    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
        _rigid = GetComponent<Rigidbody2D>();
        _planeSpeed = GameObject.Find("Hilda").GetComponent<EnemyPlaneSpawn>()._planeSpeed;
        _player = GameObject.Find("Player");
        if (this.CompareTag("Green Enemy"))
        {
            randomShootX = ((float)Random.Range(-300, 600)) / 100;
        }
        else
        {
            randomShootX = ((float)Random.Range(0, 1100)) / 100;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_trans.position.x < randomShootX && !shot)
        {
            _rigid.velocity = new Vector3(0, 0, 0);
            StartCoroutine(waitToShoot(1));

        }

        if (shot)
        {
            StartCoroutine(waitToMove(3));
        }

        if (_trans.position.x > GameObject.Find("Main Camera").GetComponent<CameraBorder>().tR.x + 3)
        {
            Destroy(gameObject, 0);
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
        if (tag.Equals("Purple Enemy"))
        {
            var Bullet = Instantiate(_bulletPrefab, _bulletSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));

            Vector3 velocity = _player.transform.position - _bulletSpawn.transform.position;
            velocity = velocity.normalized;
            Bullet.GetComponent<Rigidbody2D>().velocity = velocity * _bulletSpeed;
            Destroy(Bullet, 5);
        }

        else if (tag.Equals("Green Enemy"))
        {
            float spreadAngle = 20;

            for (int i = 0; i < 4; i++)
            {
                var Bullet = Instantiate(_bulletPrefab, _bulletSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));

                var dir = _player.transform.position - _bulletSpawn.transform.position;
                float angle = 0;

                switch (i)
                {
                    case 0:
                        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - (spreadAngle / 2.5f);
                        break;
                    case 1:
                        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - spreadAngle * 1.25f;
                        break;
                    case 2:
                        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + (spreadAngle / 2.5f);
                        break;
                    case 3:
                        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + spreadAngle * 1.25f;
                        break;
                    default:
                        break;
                }


                Bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


                Vector3 velocity = Bullet.transform.right;

                Bullet.GetComponent<Rigidbody2D>().velocity = velocity * _bulletSpeed;
                Destroy(Bullet, 5);


            }
        }
        shot = true;
    }
}

