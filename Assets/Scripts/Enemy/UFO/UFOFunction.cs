using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFunction : MonoBehaviour
{

    Transform _trans;
    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_trans.position.x < -14)
        {
            Destroy(gameObject, 0);
        }
    }
}
