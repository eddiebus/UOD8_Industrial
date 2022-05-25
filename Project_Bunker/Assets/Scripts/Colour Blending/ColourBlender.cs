using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourBlender : MonoBehaviour
{
    public MeshRenderer mesh;
    public Color TargetColour;
    public Color Colour;
    public float FadeSpeed = 1;
    // Start is called before the first frame update


    public void SetColour(Color newColour)
    {
        TargetColour = newColour;
    }

    void Start()
    {
        Colour = new Color(1, 1, 1, 1);
        mesh = GetComponent<MeshRenderer>();
        if (!mesh)
        {
            mesh = GetComponentInChildren<MeshRenderer>();
        }
        if (!mesh)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float[] channels = { Colour.r, Colour.g, Colour.b, Colour.a };
        float[] targetChannels = { TargetColour.r, TargetColour.g, TargetColour.b, TargetColour.a };

        for (int i = 0; i < channels.Length; i++)
        {
            if (channels[i] < targetChannels[i])
            {
                channels[i] += FadeSpeed * Time.deltaTime;
                if (channels[i] > targetChannels[i]) { channels[i] = targetChannels[i]; }
            }
            else if (channels[i] > targetChannels[i])
            {
                channels[i] -= FadeSpeed * Time.deltaTime;
                if (channels[i] < targetChannels[i]) { channels[i] = targetChannels[i]; }
            }
        }


        Colour = new Color(
            channels[0],
            channels[1],
            channels[2],
            channels[3]
            );

        mesh.material.color = Colour;
        
    }
}
