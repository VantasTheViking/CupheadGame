using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardGauge : MonoBehaviour
{
    [SerializeField] Image Mask;
    [SerializeField] GameObject player;
    [SerializeField] int cardNumber;
    PlayerHealth playerStats;

    float maxGauge = 50;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = (float)playerStats.GetCardGauge(cardNumber) / (float)maxGauge;

        Mask.fillAmount = fillAmount;
    }
}
