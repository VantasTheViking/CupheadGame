using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHealth : MonoBehaviour
{
    [Tooltip("Health of object (int)")]
    [SerializeField] private int _health;

    [Tooltip("Layer to collide with (int)")]
    [SerializeField] private int _layer;

    // Update is called once per frame
    void Update()
    {
        if(_health <= 0)
        {
            this.GetComponent<Renderer>().enabled = false;
        }
    }

    void takeDamage(int damage)
    {
        _health -= damage;
    }

    public int getHealth()
    {
        return _health;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layer)
        {
            takeDamage(1);
        }
    }

    public int GetHealth()
    {
        return _health;
    }
}
