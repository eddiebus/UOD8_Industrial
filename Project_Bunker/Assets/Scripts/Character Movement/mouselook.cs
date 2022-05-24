using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    //If current script is ative
    public bool Active = true;
    //Direction player is looking
    private Vector3 _LookDirection;
    public Vector3 LookDirection
    {
        get { return _LookDirection; }
    }

    //Mouse look sensitivity
    public float mouseSensitivity = 1.0f;
    public Transform PlayerBody; //Transform of parent object
    private float xRotation = 0f;

    public void UpdateLookDirection()
    {
        _LookDirection = (PlayerBody.rotation * Quaternion.Euler(xRotation, 0f, 0f)) * Vector3.forward;
    }

    public void SetActive(bool newBool)
    {
        Active = newBool;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Quaternion startRotation = PlayerBody.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            PlayerBody.Rotate(Vector3.up * mouseX);
        }
        UpdateLookDirection();
        DebugDraw();

    }

    private void DebugDraw()
    {
        if (Debug.isDebugBuild)
        {
            Debug.DrawRay(transform.position, _LookDirection, Color.black);
        }
    }
}
 

