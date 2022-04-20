using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
   [SerializeField] private Transform cameraObject;
    private InputHandler inputHandler;
    public Vector3 moveDirection;
    public Transform tPoint;

    [HideInInspector] public Rigidbody _rigidbody;
    [HideInInspector] public GameObject normalCamera;

    [Header("Stats")] 
    [SerializeField] private float movementSpeed = 5;

    private float fullSpeed = 5f;

    [SerializeField] private float rotationSpeed = 10;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
     //   cameraObject = Camera.main.transform;
        
    }
    
    private void Update()
    {
        
        if (TPoint.singleton.canTransform)
        {
            inputHandler.TickInput(Time.deltaTime);
        }
        
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movementSpeed;
        
        
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector); 
        _rigidbody.velocity = projectedVelocity;
        
        
        Vector3 localVelocity = transform.InverseTransformDirection(moveDirection.normalized);
        if (localVelocity.z <0.5f)
        {
            movementSpeed = fullSpeed / 2;
        }
        else
        {
            movementSpeed = fullSpeed;
        }
     //   HandleRotation(Time.deltaTime);
        LookAtT(Time.deltaTime);
    }

    #region Movement
    
    private Vector3 targetPosition;
    private Vector3 normalVector;

    void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;
        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;
        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }
        Quaternion tr = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * delta);
    }

    public Vector3 targetdirect;
    void LookAtT(float delta)
    {
        if (TPoint.singleton.canTransform)
        {
            targetdirect = tPoint.position - transform.position;
        
            targetdirect.y = 0;
            Quaternion tr = Quaternion.LookRotation(targetdirect);
            transform.rotation = Quaternion.Slerp(transform.rotation,tr,rotationSpeed * delta);
        }
        
    }


    #endregion
    

}
