using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 3D Object display that overlays other cameras.
 * For this obect to work create a new camera object in its own layer. Other cameras should not render this layer.
 */
public class ObjectDisplay : MonoBehaviour
{
    public bool Active = true;
    public string LayerName;
    private GameObject DisplayObject;
    [Range(1,20)]
    public float ObjectDistance;
    private Vector3 rotationVector = new Vector3(0,0,0);
    private float objectScale = 1;
    [Range(2,10)]
    public float maxObjectScale = 10;
    [Range(0.2f,5)]
    public float rotationSpeed = 1;

    public Mesh TargetMesh;
    public Material TargetMaterial;

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
        DisplayObject.transform.localPosition = new Vector3(0, 0, ObjectDistance);
        DisplayObject.transform.rotation = Quaternion.Euler(rotationVector);
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

        if (!Active)
        {
            return;
        }
        //Rotation
        Cursor.lockState = CursorLockMode.Locked;
        float x = rotationVector.x;
        float y = rotationVector.y;
        float z = rotationVector.z;


        float rotSpeedScale = 2; //Whole numbers are quick. Lower scale a tad
        x += Input.GetAxis("Mouse Y") * rotationSpeed;
        y -= Input.GetAxis("Mouse X") * rotationSpeed;

        rotationVector = new Vector3(x, y, z);

        objectScale += Input.GetAxis("Mouse ScrollWheel");
        if (objectScale < 0.5f)
        {
            objectScale = 0.5f;
        }
        else if (objectScale > maxObjectScale)
        {
            objectScale = maxObjectScale;
        }

        DisplayObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
    }
    private void DebugCheck()
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
        CreateDisplayObject();
    }

    // Update is called once per frame
    void Update()
    {
        RotAndScaleInput();
        //Update objects and properties
        SetDisplayObjectPos();
        SetSelfCamera();
        UpdateDisplayObjectConfig();
        DebugCheck();
    }

}
