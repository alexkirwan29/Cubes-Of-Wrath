using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    public float StrafeSpeed = 10f;
    public float TurnSpeed = 45;
    public enum moveMode {Turn, Strafe, SnapTurn};
    public moveMode mode = moveMode.Strafe;
    public Transform shipGraphics;
    float turnInput;
    float lastInput;
    int dir;
    void Start()
    {
        //GameCamera.instance.SetTarget(this);
    }

    // Update is called once per frame
    void Update()
    {
        turnInput = Input.GetAxisRaw("Horizontal");

        if (mode == moveMode.Strafe)
        {
            
            Vector3 moveVector = new Vector3(turnInput * StrafeSpeed, 0, Speed);
            transform.position += moveVector * Time.deltaTime;

            shipGraphics.localRotation = Quaternion.Euler(0,0,-moveVector.x);
        }
        else if (mode == moveMode.Turn)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            transform.Rotate(Vector3.up, TurnSpeed * Time.deltaTime * turnInput);
        }
        else if(mode == moveMode.SnapTurn)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            if (turnInput != lastInput)
            {
                lastInput = turnInput;
                dir += (int)turnInput;
                dir = (int)Mathf.Repeat(dir, 4);
                transform.rotation = Quaternion.Euler(0,dir * 90,0);
            }
            //transform.Rotate(Vector3.up, TurnSpeed * Time.deltaTime * turnInput);
        }
    }
    public float horizontalVelocity { get { return turnInput; } }
}
