using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningShaderScript : MonoBehaviour
{
    Renderer lightnightRenderer;
    SpriteRenderer spriteRenderer;
    float offSetY
    {
        get
        {
            return lightnightRenderer.material.GetFloat("_OffSetY");
        }
        set
        {
            lightnightRenderer.material.SetFloat("_OffSetY", value);
        }
    }
    float tilingY
    {
        get
        {
            return lightnightRenderer.material.GetFloat("_TilingY");
        }
        set
        {
            lightnightRenderer.material.SetFloat("_TilingY", value);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        lightnightRenderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //lightnightRenderer = GetComponent<Renderer>();
        LightningAnim();
        InvokeRepeating(nameof(LightningFlip), 0, 0.1f);
    }
    void FixedUpdate()
    {
        
    }
    void LightningFlip()
    {
        if (spriteRenderer.flipX == true)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }    
    void LightningAnim()
    {
        //Color tmp = spriteRenderer.color;
        //tmp.a = Random.Range(0, 2);
        //spriteRenderer.color = tmp;


        var time = Random.Range(0f, 0.2f);
        offSetY = Random.Range(0f, 1.5f);
        tilingY = Random.Range(0f, 1.5f);
        transform.LeanScaleX(Random.Range(0.1f,1f), time);
        Invoke(nameof(LightningAnim), time);

        //if (tilingY == 1.5f)
        //    tilingY = 1;
        //else
        //    tilingY = 1.5f;

        //DOVirtual.Float(1, 1.5f, 0.5f, v => tilingY = v).SetEase(Ease.InQuart);
        //DOVirtual.Float(0, 1.5f, time, v => offSetY = v);
        //DOVirtual.Float(1.5f, 0, time, v => offSetY = v);

        //if (offSetY < 1)
        //    offSetY = 1.5f;
        //else
        //    offSetY = 1;


        //Invoke(nameof(LightningFlip), time);
    }
}
