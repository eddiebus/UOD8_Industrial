using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Management class for a scripts to be controlled by the player
 * The object should have all these scripts included
 */
public class Player : MonoBehaviour
{
    private GameObject MainCamObj;
    public LayerMask InteractiveLayerFilter;
    public mouselook lookComp;
    public PlayerMovementScript playerMovementComp;
    public ObjectDisplay objectDisplayComp;
    [Range(0.1f,10f)]
    public float InterationDistance = 1;

    public string GalleryObjTag;
    public bool InObjDisplay; 

    private bool Init()
    {
        lookComp = GetComponentInChildren<mouselook>();
        playerMovementComp = GetComponentInChildren<PlayerMovementScript>();
        MainCamObj = GameObject.FindGameObjectWithTag("MainCamera");

        if (lookComp && playerMovementComp && MainCamObj)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckForInteractiveObject()
    {
        Ray hitRay = new Ray(MainCamObj.transform.position, lookComp.LookDirection);
        RaycastHit[] rayResults = Physics.RaycastAll(hitRay, InterationDistance,InteractiveLayerFilter);
        if (rayResults.Length > 0)
        {
            Debug.Log("Hit!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Init() == false)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DebugDraw();
        CheckForInteractiveObject();
    }

    private void DebugDraw()
    {
        Debug.DrawRay(MainCamObj.transform.position, lookComp.LookDirection * InterationDistance, Color.yellow);
    }
}
