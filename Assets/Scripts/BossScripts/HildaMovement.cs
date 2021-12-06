using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HildaMovement : MonoBehaviour
{
    [SerializeField] GameObject hilda;
    Phase1Attacks phase1Attacks;
    Phase2Attacks phase2Attacks;
    GlobalController global;

    float initialY;
    float initialX;
    float movementTimer;

    public bool isShooting;
    bool phase3;

    // Start is called before the first frame update
    void Start()
    {
        initialX = hilda.transform.position.x;
        initialY = hilda.transform.position.y;

        phase1Attacks = GetComponent<Phase1Attacks>();
        phase2Attacks = GetComponent<Phase2Attacks>();
        global = GetComponent<GlobalController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShooting && !phase3)
        {
            hilda.transform.position = new Vector3(hilda.transform.position.x, initialY + Mathf.Sin(movementTimer) * 4, gameObject.transform.position.z);
            hilda.transform.position = new Vector3(initialX + Mathf.Sin(movementTimer * 2) * 2, hilda.transform.position.y, gameObject.transform.position.z);
            movementTimer += Time.deltaTime;
        }

        else if (!isShooting && phase3)
        {
            hilda.transform.position = new Vector3(gameObject.transform.position.x, initialY + Mathf.Sin(movementTimer * 3), gameObject.transform.position.z);
            movementTimer += Time.deltaTime;
        }

        if (phase1Attacks.GetIsTornado())
        {
            isShooting = true;
        }

        else if (phase2Attacks.GetIsSpiral() && global.GetPhase() == 2)
        {
            isShooting = true;
        }
        else 
        {
            isShooting = false;
        }

        if (global.GetPhase() == 3)
        {
            phase3 = true;
        }
    }


}
