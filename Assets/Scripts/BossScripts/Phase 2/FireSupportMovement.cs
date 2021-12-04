using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSupportMovement : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] float movementSpeed;
    [SerializeField] bool goingDown;
    


    private void Update()
    {

        if (transform.position.y < camera.GetComponent<CameraBorder>().bL.y)
        {
            goingDown = false;
        } 
        else if (transform.position.y > camera.GetComponent<CameraBorder>().tR.y)
        {
            goingDown = true;
        }


        if (goingDown)
        {
            transform.position -= transform.up * Time.deltaTime * movementSpeed;
            
        }
        else
        {
            transform.position += transform.up * Time.deltaTime * movementSpeed;
        }
    }
}
