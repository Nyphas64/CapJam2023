using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnCoins : MonoBehaviour
{
    Coin[] coins;

    private void Awake()
    {
        coins = GetComponentsInChildren<Coin>();
    }

    public void StartResetCoins()
    {
        StartCoroutine(ResetCoins());
    }

    public IEnumerator ResetCoins()
    {
        yield return new WaitForSeconds(3f);
        foreach(Coin coin in coins)
        {
            coin.enabled = true;
            coin.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
