using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{

    [Tooltip("Health of object (int)")]
    [SerializeField] private int _health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rocket"))
        {
            Destroy(collision.gameObject);
            takeDamage(15);
        }
    }

    public void takeDamage(int damage)
    {
        
          _health -= damage;
        
    }
}
