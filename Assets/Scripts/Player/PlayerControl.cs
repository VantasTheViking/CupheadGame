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

    bool isColliding;
    float timeToNextShot = 0;

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

    bool CanShoot()
    {
        if (timeToNextShot < Time.realtimeSinceStartup)
        {
            timeToNextShot = Time.realtimeSinceStartup + shootDelay;
            return true;
        }

        else
        {
            return false;
        }
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
