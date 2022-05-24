using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteToScreen : MonoBehaviour
{
    private bool InitOK = false;
    private GameObject UIObject;
    public string UIObjectTag;
    public Vector3 ScreenSize;
    public Sprite TargetSprite;
    public Color spriteColour;
    private Image UIImage;
    private RectTransform TransformRect;

    [Range (0,1)]
    public float xPos = 0;
    [Range(0, 1)]
    public float yPos = 0;
    [Range(0.2f,10)]
    public float scale = 1;

    public bool InitUI()
    {
        UIObject = new GameObject("SpriteLabel");
        //Find Default UI Layer
        GameObject parentObj = GameObject.FindGameObjectWithTag(UIObjectTag);
        if (parentObj)
        {
            UIObject.transform.SetParent(parentObj.transform);
        }
        else
        {
            Debug.Log($"Error Object {this.gameObject.name}. Couldn't find Canvas Object with Tag 'UI'");
            return false;
        }

        TransformRect = GetComponent<RectTransform>();

        UIImage = UIObject.AddComponent<Image>();
        UIImage.maskable = true;
        UIImage.preserveAspect = true;
        UIImage.type = Image.Type.Simple;

        return true;
    }

    public void SetColour(Color newColour)
    {
        spriteColour = newColour;
    }

    public void SetSprite()
    {
        if (TargetSprite)
        {
            UIImage.sprite = TargetSprite;
        }
        UIImage.color = spriteColour;
    }

    private void GetSize()
    {
        Vector3 bottomLeftCorner = Camera.main.ScreenToWorldPoint(
            new Vector3(0, 0, 0)
            );

        Vector3 topRightCorner = Camera.main.ScreenToWorldPoint(
            new Vector3(
                Camera.main.pixelWidth,
                Camera.main.pixelHeight,
                0)
            );

        ScreenSize = new Vector3(Screen.width, Screen.height, 0);
    }

    public void SetPosition()
    {
        float x = 0;
        float y = 0;

        x += ScreenSize.x * xPos;
        y += ScreenSize.y * yPos;
        UIObject.transform.position = new Vector3(x, y,0);
        UIObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitOK = InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (InitOK)
        {
            GetSize();
            SetPosition();
            SetSprite();
        }
    }
}
