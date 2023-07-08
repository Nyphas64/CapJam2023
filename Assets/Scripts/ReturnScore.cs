using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnScore : MonoBehaviour
{
    Image[] slots;

    Sprite orgImage;

    private void Awake()
    {
        slots = GetComponentsInChildren<Image>();
        orgImage = slots[1].sprite;
    }

    public void StartResetScore()
    {
        StartCoroutine(ResetScore());
    }

    public IEnumerator ResetScore()
    {
        yield return new WaitForSeconds(3);
        foreach(Image slot in slots) 
        {
            slot.sprite = orgImage;
        }
    }
}
