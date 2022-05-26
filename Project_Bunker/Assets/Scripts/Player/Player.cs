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

    public bool InObjDisplay; 

    private bool Init()
    {
        lookComp = GetComponentInChildren<mouselook>();
        playerMovementComp = GetComponentInChildren<PlayerMovementScript>();
        MainCamObj = GameObject.FindGameObjectWithTag("MainCamera");
        objectDisplayComp = GetComponentInChildren<ObjectDisplay>();

        if (lookComp && playerMovementComp && MainCamObj)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckState()
    {
        if (InObjDisplay == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                InObjDisplay = false;
                playerMovementComp.SetActive(true);
                lookComp.SetActive(true);
                objectDisplayComp.SetActive(false);
                objectDisplayComp.SetObjectDesc("");
            }
        }
    }

    private void CheckForInteractiveObject()
    {
        Ray hitRay = new Ray(MainCamObj.transform.position, lookComp.LookDirection);
        RaycastHit[] rayResults = Physics.RaycastAll(hitRay, InterationDistance,InteractiveLayerFilter);
        if (rayResults.Length > 0)
        {

            GameObject closestObject = null;
            float closestDistance = 0;
            for (int i = 0; i < rayResults.Length; i++)
            {
                GameObject currentGameObject = rayResults[i].transform.gameObject;
                Vector3 hitPosition = rayResults[i].point;
                Vector3 distanceVector = hitPosition - MainCamObj.transform.position;
                float distance = distanceVector.magnitude;

                if (i == 0)
                {
                    closestObject = currentGameObject;
                    closestDistance = distance;
                }
                else
                {
                    if (distance < closestDistance)
                    {
                        closestObject = currentGameObject;
                        closestDistance = distance;
                    }
                }
            }

            InteractiveObject InteractiveComp = closestObject.GetComponent<InteractiveObject>();
            if (InteractiveComp)
            {
                InteractiveComp.Glow();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    InObjDisplay = true;
                    lookComp.SetActive(false);
                    playerMovementComp.SetActive(false);
                    objectDisplayComp.SetActive(true);
                    objectDisplayComp.SetObject(InteractiveComp.GalleryObjPrefab);
                    objectDisplayComp.SetObjectDesc(InteractiveComp.GalleryObjDesc);
                }
            }
            else
            {
                Debug.Log($"Error: Couldn't find interactive script component in object {closestObject.name}");
            }
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
        CheckState();
    }

    private void DebugDraw()
    {
        if (Debug.isDebugBuild)
        {
            Debug.DrawRay(MainCamObj.transform.position, lookComp.LookDirection * InterationDistance * 100, Color.yellow);
        }
    }
}
