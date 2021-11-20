using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorder : MonoBehaviour
{
    [SerializeField] Transform _playerPos;
    [SerializeField] Camera _camera;

    Vector3 bL;
    Vector3 tR;
    Rect camRect;

    // Start is called before the first frame update
    void Start()
    {

        bL = _camera.ScreenToWorldPoint(Vector3.zero);

        tR = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight));

        camRect = new Rect(bL.x, bL.y, tR.x - bL.x, tR.y - bL.y);
    }


    // Update is called once per frame
    void Update()
    {
        _playerPos.transform.position = new Vector3(Mathf.Clamp(transform.position.x, camRect.xMin + 0.5f, camRect.xMax - 0.5f), Mathf.Clamp(transform.position.y, camRect.yMin + 0.25f, camRect.yMax - 0.25f), transform.position.z);
    }
}


