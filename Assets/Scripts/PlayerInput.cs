using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Singleton
    public static PlayerInput Instance;

    // Horizontal Axis (A and D Keys)
    public float Horizontal { get; private set; }
    //vertical Axis (W and S Keys)
    public float Vertical { get; private set; }
    //Mouse X Position
    public float MouseX { get; private set; }
    //Mouse Y Position
    public float MouseY { get; private set; }
    //Left Mouse Button
    public bool Action { get; private set; }
    //Roll Button
    public bool Roll { get; private set; }

   
    public void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        Action = Input.GetMouseButton(0);
        Roll = Input.GetKeyDown(KeyCode.Space);
    }
}
