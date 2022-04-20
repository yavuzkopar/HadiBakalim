using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    
    Vector3 cameraPosition;
    public float lookSpeed = 0.1f;

    public float followSpeed = 0.1f;

    public float pivotSpeed = 0.03f;

    private float defaultPos;

    private float lookAngle;

    private float pivotAngle;

   [SerializeField] private float minPivot= -35;
    
   [SerializeField] private float maxPivot = 35;

    public static CameraHandler singleton;
    // Start is called before the first frame update
    private void Awake()
    {
        singleton = this;
        defaultPos = cameraTransform.localPosition.z;
    }

    public void FollowTarget(float delta)
    {
        Vector3 targetPos = Vector3.Lerp(transform.position, targetTransform.position, delta / followSpeed);
        transform.position = targetPos;
    }

    public void HandleCameraRotation(float delta,float mouseXinput,float mouseYinput)
    {
        lookAngle += (mouseXinput * lookSpeed) / delta;
        pivotAngle -= (mouseYinput * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minPivot, maxPivot);
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;
       // rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.rotation = targetRotation;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
