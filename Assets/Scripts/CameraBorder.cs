using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorder : MonoBehaviour
{
    [SerializeField] Transform _playerPos;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject background;
    [SerializeField] GameObject nightBackground;
    
    public Vector3 bL;
    public Vector3 tR;
    Rect camRect;

    // Start is called before the first frame update
    void Start()
    {
        

        bL = _camera.ScreenToWorldPoint(Vector3.zero);

        tR = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight));

        camRect = new Rect(bL.x, bL.y, tR.x - bL.x, tR.y - bL.y);

        background.transform.localScale = new Vector3(camRect.size.x, camRect.size.y, 1);
        background.transform.position = new Vector3(0, 0, 0);

        nightBackground.transform.localScale = new Vector3(camRect.size.x, camRect.size.y, 1);
        nightBackground.transform.position = new Vector3(0, 0, 0);



        GameObject.Find("TopSpawn").transform.position = new Vector3(tR.x, tR.y - 2, 0);
        GameObject.Find("BottomSpawn").transform.position = new Vector3(tR.x, bL.y + 2, 0);

        GameObject.Find("MeteorSpawn1").transform.position = new Vector3(bL.x + 5, tR.y + 1, 0);
        GameObject.Find("MeteorSpawn2").transform.position = new Vector3(tR.x - 4, tR.y + 1, 0);
        
    }


    // Update is called once per frame
    void Update()
    {
        _playerPos.transform.position = new Vector3(Mathf.Clamp(_playerPos.transform.position.x, camRect.xMin + 0.5f, camRect.xMax - 0.5f), Mathf.Clamp(_playerPos.transform.position.y, camRect.yMin + 0.25f, camRect.yMax - 0.25f), _playerPos.transform.position.z);
    }
}


