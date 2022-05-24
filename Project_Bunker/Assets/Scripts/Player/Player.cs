using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public mouselook lookComp;
    public PlayerMovementScript playerMovementComp;
    public ObjectDisplay objDisplayComp;


    public bool InObjDisplay; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InObjDisplay)
        {
            objDisplayComp.SetActive(true);
        }

    }
}
