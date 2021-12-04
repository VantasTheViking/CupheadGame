using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BossHealth : MonoBehaviour
{
    [Tooltip("Health value of gameobject (int)")]
    [SerializeField] public int _health;

    [Tooltip("The layer to collide with (int)")]
    [SerializeField] private int _layer;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == _layer)
        {
            Destroy(collision.gameObject, 0);

            takeDamage();

            //Debug.Log(_health);
        }
    }

    public void takeDamage()
    {
        _health -= 1;
    }

    public int getHealth()
    {
        return _health;
    }
}