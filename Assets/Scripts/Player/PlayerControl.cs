using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Transform trans;
    Rigidbody2D body;

    [SerializeField] float moveSpeed;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletSpeed;
    [SerializeField] float shootDelay;

    [SerializeField] GameObject rocketPrefab;

    [SerializeField] float missileSpeed;

    [SerializeField] Sprite rocketSprite;

    [SerializeField] Sprite baseSprite;
    
    bool isColliding;
    float timeToNextShot = 0;

    bool rocket;
    bool movingUp;
    bool movingDown;


    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.N) && GetComponent<PlayerHealth>().GetCardsFull())
        {
            //Debug.Log("test1");

            StartCoroutine(rocketWait(5));
        }

        if (Input.GetKeyDown(KeyCode.N) && canMissile())
        {
            ShootMissile();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && CanShoot())
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position + new Vector3(0, (float)Random.Range(-20, 20) / 100, 0), Quaternion.Euler(new Vector3(0, 0, 0)));

        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
        bullet.layer = 3;

        Destroy(bullet, 5);
    }

    void becomeRocket()
    {
        //Debug.Log("Test3");
        rocket = true;
        GetComponent<SpriteRenderer>().sprite = rocketSprite;
        GetComponent<Animator>().enabled = false;
    }

    IEnumerator rocketWait(float seconds)
    {
        //Debug.Log("test2");
        GetComponent<PlayerHealth>().ResetCards();
        becomeRocket();
        yield return new WaitForSeconds(seconds);

        //Debug.Log("Test4");
        if(Vector3.Distance(GameObject.Find("Hilda").transform.position, trans.position) < 5)
        {
            GameObject.Find("Hilda").GetComponent<BossHealth>().takeDamage(20);
        }

        rocket = false;
        GetComponent<SpriteRenderer>().sprite = baseSprite;
        GetComponent<Animator>().enabled = true;
    }

    bool CanShoot()
    {
        if (timeToNextShot < Time.realtimeSinceStartup && !rocket)
        {
            timeToNextShot = Time.realtimeSinceStartup + shootDelay;
            return true;
        }

        else
        {
            return false;
        }
    }

    void ShootMissile()
    {
        var Missile = Instantiate(rocketPrefab, bulletSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Missile.GetComponent<Rigidbody2D>().velocity = transform.right * missileSpeed;

        Destroy(Missile, 4);
    }

    bool canMissile()
    {
        //Debug.Log("check1");
        for(int i = 0; i < 5; i++)
        {
            //Debug.Log($"check2 {i}");
            if (GetComponent<PlayerHealth>().GetCardGauge(5 - i) >= 50)
            {
                //Debug.Log("check3");
                GetComponent<PlayerHealth>().resetCardGauge(5 - i);

                for(int v = 0; v < 5; v++)
                {
                    if(GetComponent<PlayerHealth>().GetCardGauge(5 - v) <= 50)
                    {
                        //GetComponent<PlayerHealth>().setCardGauge(5 - v, GetComponent<PlayerHealth>().GetCardGauge(5 - v));
                    }
                }
                return true;
            }
        }
        return false;
    }

    void Move()
    {
        List<string> inputs = new List<string>();

        if (Input.GetKey(KeyCode.W))
        {
            inputs.Add("w");
            movingUp = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputs.Add("s");
            movingDown = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputs.Add("d");
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputs.Add("a");
        }

        if (movingDown && movingUp)
        {
            movingUp = false;
            movingDown = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            movingUp = false;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            movingDown = false;
        }

        AssignInput(inputs);

        if (inputs.Count == 0 && body.velocity.magnitude > 0 && !isColliding)
        {
            body.velocity = Vector2.zero;
        }

        inputs.Clear();
    }

    //specifically for diagonal movement; makes the movement more smooth
    void AssignInput(List<string> inputs)
    {
        if (inputs.Count == 2)
        {
            MovePlayer(inputs, 1.5f);
        }

        else
        {
            MovePlayer(inputs, 1);
        }
    }

    void MovePlayer(List<string> inputs, float divider)
    {
        for (int x = 0; x < inputs.Count; x++)
        {
            if (inputs[x] == "w")
            {
                trans.position += transform.up * Time.deltaTime * moveSpeed / divider;
            }
            else if (inputs[x] == "s")
            {
                trans.position += -transform.up * Time.deltaTime * moveSpeed / divider;
            }
            else if (inputs[x] == "d")
            {
                trans.position += transform.right * Time.deltaTime * moveSpeed / divider;
            }
            else if (inputs[x] == "a")
            {
                trans.position += -transform.right * Time.deltaTime * moveSpeed / divider;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    public bool GetIsMovingUp()
    {
        return movingUp;
    }

    public bool GetIsMovingDown()
    {
        return movingDown;
    }
}
