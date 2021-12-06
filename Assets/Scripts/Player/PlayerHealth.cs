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

    

    private int card1;

    private int card2;

    private int card3;

    private int card4;

    private int card5;


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
        if (!gameObject.GetComponent<PlayerControl>().invincible)
        {
            _health -= damage;
        }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _layer)
        {
            takeDamage(1);
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<PolygonCollider2D>().enabled = false;

        }
    }

    public void ChargeCardGauge()
    {
        if (card1 < 50)
        {
            card1 += 1;
        } else if (card2 < 50){
            card2 += 1;
        }
        else if (card3 < 50)
        {
            card3 += 1;
        }
        else if (card4 < 50)
        {
            card4 += 1;
        }
        else if (card5 < 50)
        {
            card5 += 1;
        }

    }

    public bool GetCardsFull()
    {
        if (card1 >= 50 && card2 >= 50 && card3 >= 50 && card4 >= 50 && card5 >= 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetCards()
    {
        card1 = 0;
        card2 = 0;
        card3 = 0;
        card4 = 0;
        card5 = 0;
    }

    public void resetCardGauge(int card)
    {
        switch (card)
        {
            case 1:
                card1 = 0;
                break;
            case 2:
                card2 = 0;
                break;
            case 3:
                card3 = 0;
                break;
            case 4:
                card4 = 0;
                break;
            case 5:
                card5 = 0;
                break;
            default:
                break;
        }
    }

    public void setCardGauge(int card, int value)
    {
        switch (card)
        {
            case 1:
                card1 += value;
                break;
            case 2:
                card2 += value;
                break;
            case 3:
                card3 += value;
                break;
            case 4:
                card4 += value;
                break;
            case 5:
                card5 += value;
                break;
            default:
                break;
        }
    }

    public int GetCardGauge(int card)
    {
        switch (card)
        {
            case 1:
                return card1;
            case 2:
                return card2;
            case 3:
                return card3;
            case 4:
                return card4;
            case 5:
                return card5;
            default:
                return 0;
        }
    }
}
