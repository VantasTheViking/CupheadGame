using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BossHealth : MonoBehaviour
{
    [SerializeField] public int _health;

    [SerializeField] private int _layer;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == _layer)
        {
            Destroy(collision.gameObject, 0);

            takeDamage();
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
