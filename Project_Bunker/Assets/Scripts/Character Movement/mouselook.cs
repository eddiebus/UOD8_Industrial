using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    public float mouseSensitivity = 1.0f;
    public Transform PlayerBody;
    public bool Active = true;
    private float xRotation = 0f;


    public void SetActive(bool newBool)
    {
        Active = newBool;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Quaternion startRotation = PlayerBody.rotation;
        Debug.Log($"The Start rotation is {startRotation}");
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

    }
}
 

