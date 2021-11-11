using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform trans;
    Rigidbody2D body;

    [SerializeField] float Speed;


    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            trans.position += -transform.right * Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            trans.position += transform.right * Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            trans.position += transform.up * Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            trans.position += -transform.up * Time.deltaTime * Speed;
        }
    }
}
