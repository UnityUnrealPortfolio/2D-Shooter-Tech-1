using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movment Properties")]

    [SerializeField] InputScheme inputScheme;
    [SerializeField] float playerSpeed;

    [Tooltip("Rate of ship slow down after last input if no further input applied")]
    [SerializeField] float slowDownRate = .9f;

    [Tooltip("If player is using joystick, this will control their look speed.")]
    [SerializeField] float lookSpeed = 5;
    [Tooltip("augments the look sensitivity of joystick inputs")]
    [SerializeField] float lookSensitivity = 20f;

    [Tooltip("If player is using keyboard mouse this will control their turn speed")]
    [SerializeField]float rotateSpeed = 90;

    float currentSpeed = 0f;
    Vector3 lastMovement = Vector3.zero;
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    private void Update()
    {
        if(inputScheme == InputScheme.KeyboardMouse)
        {

        RotatePlayerWithMouse();
        }
        if(inputScheme == InputScheme.Gamepad)
        {

          RotatePlayerWithJoystick();
        }
        MovePlayer();
    }

    private void RotatePlayerWithJoystick()
    {
        float turnSpeed = -lookSpeed * lookSensitivity;
        transform.Rotate(0f, 0f, lookInput.x * Time.deltaTime * turnSpeed);
    }

    private void RotatePlayerWithMouse()
    {
        //where is mouse relative to player    
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //determine angel to turn between ship and mouse pos
        float deltaX = transform.position.x - mousePosInWorld.x;
        float deltaY = transform.position.y - mousePosInWorld.y;

        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;

        //apply that angle to the transforms rotation in the Z axis(top down)
        Quaternion rot = Quaternion.Euler(0, 0, angle + rotateSpeed);
        transform.rotation = rot;
    }

    private void MovePlayer()
    {
       
        if(moveInput.sqrMagnitude > 0)
        {
            currentSpeed = playerSpeed;
            transform.Translate(moveInput * Time.deltaTime * playerSpeed, Space.World);
            lastMovement = moveInput;
        }
        else
        {
            //move in direction we were going
            transform.Translate(lastMovement * Time.deltaTime*currentSpeed, Space.World);

            //slow down with time
            currentSpeed *= slowDownRate;
        }
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
            moveInput = context.ReadValue<Vector2>();
    }

    public void GetLookInput(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
public enum InputScheme
{
    KeyboardMouse,
    Gamepad
}
