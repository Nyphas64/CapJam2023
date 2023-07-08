using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBlock : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    public float transitionDuration = .3f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        //transform.child.isSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            audioSource.PlayOneShot(clickSound);
            ChangeBlock();
        }
    }
    
   
    void ChangeBlock()
    {
        int i = 0;
        int changei = -666;
        Transform child;
        NoG_MoveBlock childScript;

        for (i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i);
            childScript = child.GetComponent<NoG_MoveBlock>();
            FlashChild(child);   //definitely not the most efficient way of doing this, but Im definitely lazy :)


            if (childScript.isSelected)
            {
                childScript.isSelected = false;
                changei = i+1;
            }   
        }

        Debug.Log("Setting"+changei+"to the next selected object");
        if (changei == transform.childCount)
        {
            changei = 0;
        }
        if (changei == -666) //scene did not start with a child selected
        {
            changei = 0;
        }

        child = transform.GetChild(changei);
        childScript = child.GetComponent<NoG_MoveBlock>();
        childScript.isSelected = true;
        FillChild(child);

    }

    void FillChild(Transform child)
    {
        SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();

        Color newColor = childRenderer.color;
        newColor.a = 1f;
        childRenderer.color = newColor;
    }

    void FlashChild(Transform child) //not my best function name 
    {
        SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();
        // Change the alpha value of the SpriteRenderer color
        
        Color newColor = childRenderer.color;
        newColor.a = 0.5f;
        childRenderer.color = newColor;
        
        //StartCoroutine(scaleAlpha(childRenderer));

    }

    private System.Collections.IEnumerator scaleAlpha(SpriteRenderer renderer) //this shit dont work and idfk why 
    {
        float elapsedTime = 0f;
        Color startColor = renderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            Color newColor = Color.Lerp(startColor, targetColor, t);
            renderer.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        renderer.color = targetColor;
    }
}
