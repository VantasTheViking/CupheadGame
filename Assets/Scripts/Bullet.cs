using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullet : MonoBehaviour
{

    [Tooltip("Layer to collide with (int)")]
    [SerializeField] private int _layer;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == _layer)
        {
            Destroy(gameObject, 0.1f);
            player.gameObject.GetComponent<PlayerHealth>().ChargeCardGauge();
            if (collision.gameObject.tag.Equals("Purple Enemy") || collision.gameObject.tag.Equals("Green Enemy"))
            {
                Destroy(collision.gameObject, 0);
                player.gameObject.GetComponent<PlayerHealth>().ChargeCardGauge();
            }
               
        }
        
    }
}
