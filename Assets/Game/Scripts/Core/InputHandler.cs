using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    private CameraHandler _cameraHandler;

    private PlayerActions inputActions;
    private Vector2 movementInput;
    private Vector2 cameraInput;
    private TPoint tp;

    private void Start()
    {
        _cameraHandler = CameraHandler.singleton;
        tp = TPoint.singleton;
        
    }

    private void FixedUpdate()
    {
        Debug.Log("bbb");
        float delta = Time.fixedDeltaTime;
        if (_cameraHandler != null)
        {
            Debug.Log("aaa");
            _cameraHandler.FollowTarget(delta);
            if (inputActions.PlayerMovement.SagClick.inProgress && TPoint.singleton.canTransform)
            {
                _cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
               
               Debug.Log("hhhh");
            }
        }
        else
        {
            Debug.Log("ccc");
        }
        
        
        
    }

    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerActions();
            inputActions.PlayerMovement.Movement.performed +=
                inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
    }

    void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01((Mathf.Abs(horizontal) + Mathf.Abs(vertical)));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }
}
