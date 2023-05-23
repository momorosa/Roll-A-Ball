using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float horizontalSpeed;

    float yaw;
    
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            Debug.Log("Camera requires a reference to the player.");
        yaw = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            yaw += horizontalSpeed * Mouse.current.delta.x.ReadValue() * Time.deltaTime;
            transform.position = player.position + offset;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yaw, 0);
            offset = Quaternion.AngleAxis(horizontalSpeed * Mouse.current.delta.x.ReadValue() * Time.deltaTime, Vector3.up) * offset;
        }
        transform.position = player.position + offset;
        transform.LookAt(player);
    }
}
