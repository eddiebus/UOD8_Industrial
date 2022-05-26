using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 3D Object display that overlays other cameras.
 * For this obect to work create a new camera object in its own layer. Other cameras should not render this layer.
 */
public class ObjectDisplay : MonoBehaviour
{
    public bool Active = false;
    public string LayerName;

    private GameObject DisplayObject;
    [TextArea(3,5)]
    public string ObjectDescription;

    [Range(1,20)]
    public float ObjectDistance;
    private Vector3 rotationVector = new Vector3(0,0,0);
    private Vector3 DefaultObjectScale = new Vector3(0,0,0);
    private float objectScale = 1;
    [Range(1,2.5f)]
    public float maxObjectScale = 10;
    [Range(0.2f,5)]
    public float rotationSpeed = 1;

    public SpriteToScreen ToolTip;
    public TextToScreen screenText;
    public UnityEngine.Font TextFont;
    public Vector2 TextPosition;
    public Vector2 TextSize;

    public Mesh TargetMesh;
    public Material TargetMaterial;


    //Set script to be active or not
    //E.g script is not interactive if false
    public void SetActive(bool newBool)
    {
        Active = newBool;
    }

    public void SetObject(GameObject newObject)
    {
        MeshFilter meshComp = newObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderComp = newObject.GetComponent<MeshRenderer>();
        
        if (meshComp && meshRenderComp)
        {
            TargetMesh = meshComp.sharedMesh;
            TargetMaterial = meshRenderComp.sharedMaterial;
            DefaultObjectScale = newObject.transform.localScale;
        }
    }

    public void SetObjectDesc(string newDescription)
    {
        ObjectDescription = newDescription;
    }

    //Set the display Object with new mesh and material
    public void SetObject(Mesh newMesh, Material newMaterial)
    {
        TargetMesh = newMesh;
        TargetMaterial = newMaterial;
    }

    public void SetObjectDistance(float newDistance)
    {
        ObjectDistance = newDistance;
    }

    private void SetDescText()
    {
        screenText.SetFont(TextFont);
        screenText.TextString = ObjectDescription;


        screenText.SetSize(new Vector2(
            Screen.width *  TextSize.x,
            Screen.height * TextSize.y));
        screenText.SetPosition(new Vector2(
            Screen.width * TextPosition.x,
            Screen.height * TextPosition.y));

        screenText.TextAlign = TextAnchor.UpperLeft;

        if (Active)
        {
            screenText.SetColour(new Color(1, 1, 1, 1));
        }
        else
        {
            screenText.SetColour(new Color(0, 0, 0, 0));
        }
    }

    //Set camera for self
    private void SetSelfCamera()
    {
        Camera cam = this.GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.Depth;
        cam.cullingMask = 1 << LayerMask.NameToLayer(LayerName);
        cam.fieldOfView = 90;
        cam.rect = new Rect(0, 0, 1, 1);
    }

    //Create the object to be rendered
    private void CreateDisplayObject()
    {
        DisplayObject = new GameObject("Display_Object");
        DisplayObject.transform.SetParent(this.transform);

        DisplayObject.AddComponent<MeshRenderer>();
        DisplayObject.AddComponent<MeshFilter>();
    }

    //Put display object in place
    private void SetDisplayObjectPos()
    {
        if (Active == true)
        {
            DisplayObject.transform.localPosition = new Vector3(0, 0, ObjectDistance);
            DisplayObject.transform.rotation = Quaternion.Euler(rotationVector);
        }
        else 
        {
            DisplayObject.transform.localPosition = new Vector3(0, 0, -100);
            DisplayObject.transform.rotation = Quaternion.Euler(rotationVector);
        }
    }

    //Update the display object setup
    private void UpdateDisplayObjectConfig()
    {
        DisplayObject.layer = LayerMask.NameToLayer(LayerName);
        MeshRenderer renderer = DisplayObject.GetComponent<MeshRenderer>();
        MeshFilter meshFilter = DisplayObject.GetComponent<MeshFilter>();

        renderer.material = TargetMaterial;
        meshFilter.mesh = TargetMesh;
    }

    private void RotAndScaleInput()
    {
        //Rotation
        Cursor.lockState = CursorLockMode.Locked;
        float x = rotationVector.x;
        float y = rotationVector.y;
        float z = rotationVector.z;

        if (Active == true)
        {
            x -= Input.GetAxis("Mouse Y") * rotationSpeed;
            y -= Input.GetAxis("Mouse X") * rotationSpeed;
        }

        rotationVector = new Vector3(x, y, z);

        if (Active == true)
        {
            objectScale += Input.GetAxis("Mouse ScrollWheel");
        }
        if (objectScale < 0.5f)
        {
            objectScale = 0.5f;
        }
        else if (objectScale > maxObjectScale)
        {
            objectScale = maxObjectScale;
        }

        DisplayObject.transform.localScale = new Vector3(
            DefaultObjectScale.x * objectScale, 
            DefaultObjectScale.y * objectScale, 
            DefaultObjectScale.z * objectScale);
    }

    //Debugging Function
    private void DebugUpdate()
    {
        if (!UnityEngine.Debug.isDebugBuild)
        {
            return;
        }
        else
        {
            UnityEngine.Debug.DrawLine(
                this.transform.position,
                DisplayObject.transform.position,
                Color.red
                );
        }

        
    }
    // Start is called before the first frame update
    void Start()
    {
        ToolTip = GetComponent<SpriteToScreen>();
        screenText = GetComponent<TextToScreen>();
        CreateDisplayObject();
    }

    // Update is called once per frame
    void Update()
    {


        TextSize = new Vector2(0.35f, 0.95f);
        TextPosition = new Vector2(0.8f, 0.5f);

        ToolTip.xPos = 0.2f;
        ToolTip.yPos = 0.9f;
        ToolTip.scale = 2.5f;

        if (Active == true)
        {
            RotAndScaleInput();
        }
        //Update objects and properties
        SetDisplayObjectPos();
        SetSelfCamera();
        UpdateDisplayObjectConfig();


        if (ToolTip)
        {
            if (Active == true)
            {
                ToolTip.SetColour(new Color(1, 1, 1, 1));
            }
            else
            {
                ToolTip.SetColour(new Color(1, 1, 1, 0));
            }
        }

        if (screenText)
        {
            SetDescText();
        }

        DebugUpdate();
    }

}
