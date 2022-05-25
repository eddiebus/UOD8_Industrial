using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * Script for handling interactive objects and what they do.
 */
public class InteractiveObject : MonoBehaviour
{
    public GameObject GalleryObjPrefab;
    [TextArea(3,5)]
    public string GalleryObjDesc; 
    public float GlowTime = 0;
    [Range(1,10)]
    public float GlowFadeSpeed = 1;
    public Color GlowColour;
    public ColourBlender ColourBlend;
    private void Init()
    {
        ColourBlend = this.gameObject.AddComponent<ColourBlender>();
    }

    public void Glow()
    {
        GlowTime += 1; 
        if (GlowTime > 1)
        {
            GlowTime = 1;
        }
    }

    public void SetGlowColour(Color newColour)
    {
        GlowColour = newColour;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        GlowTime -= Time.deltaTime * GlowFadeSpeed;
        if (GlowTime < 0)
        {
            GlowTime = 0;
        }

        if (GlowTime > 0)
        {
            ColourBlend.SetColour(GlowColour);
        }
        else
        {
            ColourBlend.SetColour(new Color(1, 1, 1, 1));
        }
    }
}
