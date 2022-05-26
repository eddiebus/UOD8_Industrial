using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextToScreen : MonoBehaviour
{
    private bool InitOK = false;
    public Font TextFont;
    public int TextSize = 10;
    [TextArea(1,5)]
    public string TextString;
    public Color TextColour;
    public TextAnchor TextAlign = TextAnchor.MiddleCenter;
    public Text TextComp;
    public RectTransform RectComp;

    public string UIObjectTag;
    private GameObject UIObject;
    private GameObject BGImageObj;
    private UnityEngine.UI.Image BGImageComp;
    private RectTransform BGImageRect;

    public void SetPosition(Vector2 newPos)
    {
        BGImageObj.transform.position = new Vector3(
            newPos.x,
            newPos.y,
            1);
    }

    public void SetSize(Vector2 newSize)
    {
        if (!InitOK)
        {
            return;
        }
        RectComp.sizeDelta = newSize;
    }

    public void SetColour(Color newColour)
    {
        TextColour = newColour;
    }

    public void SetFont(Font newFont)
    {
        TextFont = newFont;
    }

    private bool InitUI()
    {
        UIObject = new GameObject("TextLabel");

        //Find Deafult IO Layer
        GameObject parentObj = GameObject.FindGameObjectWithTag(UIObjectTag);
        if (parentObj)
        {
            TextComp = UIObject.AddComponent<UnityEngine.UI.Text>();
            RectComp = UIObject.GetComponent<RectTransform>();

            BGImageObj = new GameObject("BG_Image");
            BGImageObj.transform.SetParent(parentObj.transform);
            BGImageComp = BGImageObj.AddComponent<UnityEngine.UI.Image>();
            BGImageComp.color = new Color(0, 0, 0, 0.2f);
            BGImageRect = BGImageObj.GetComponent<RectTransform>();


            UIObject.transform.SetParent(BGImageObj.transform);

        }
        else
        {
            Debug.Log($"Error Object {this.gameObject.name}. Couldn't find Canvas Object with Tag 'UI'");
            return false;
        }

        return true;
    }

    private void SetTextParam()
    {
        TextComp.font = TextFont;
        TextComp.text = TextString;
        TextComp.color = TextColour;
        if (TextSize > 0)
        {
            TextComp.resizeTextForBestFit = false;
            TextComp.fontSize = TextSize;
        }
        else
        {
            if (TextSize < 0) { TextSize = 0; }
            TextComp.resizeTextForBestFit = true;
        }
        TextComp.alignment = TextAlign;
    }

    private void SetBGParam()
    {
        BGImageComp.color = new Color(0, 0, 0, 0.5f * TextComp.color.a);
        BGImageRect.sizeDelta = RectComp.sizeDelta;
    }

    private void UpdateObjName()
    {
        string objName = "TXTLabel: " + TextString;
        int extraLength = 10;
        if (objName.Length > "TXTLabel".Length + extraLength)
        {
            objName = objName.Substring(0, "TXTLabel".Length + extraLength - 3);
            objName += "...";
        }
        //objName = objName.Substring(0, 10);
        UIObject.name = objName; 
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
            SetTextParam();
            UpdateObjName();
            SetBGParam();
        }
    }
}
