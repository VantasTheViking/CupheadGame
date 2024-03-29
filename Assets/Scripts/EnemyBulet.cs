using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBulet : MonoBehaviour
{

    [Tooltip("Layer to collide with (int)")]
    [SerializeField] private int _layer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layer)
        {
            Destroy(gameObject);

            if (collision.gameObject.tag.Equals("Purple Enemy") || collision.gameObject.tag.Equals("Green Enemy"))
            {
                Destroy(collision.gameObject, 0);
            }

        }

    }
}
