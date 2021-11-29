using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFunction : MonoBehaviour
{
    float initialY;
    Transform _trans;
    // Start is called before the first frame update
    void Start()
    {
        initialY = GameObject.Find("Main Camera").GetComponent<UFOSpawn>()._spawnPoint.transform.position.y;
        _trans = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_trans.position.x < -14)
        {
            Destroy(gameObject, 0);
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialY + Mathf.Sin(Time.realtimeSinceStartup * 6)/4, gameObject.transform.position.z);
    }
}