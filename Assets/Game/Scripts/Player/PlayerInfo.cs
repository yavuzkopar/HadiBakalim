using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo singleton;
    public Transform rightHandTransform;
    public float movementSpeed;
    public bool isOnGround;
    public Rigidbody rb;
    public float jumpForce;

    [Header("Check Sphere")]
    [SerializeField]
    Vector3 origin;
    [SerializeField] float radius,gravity,sayac;
    [SerializeField] LayerMask layerMask;
    
    private void Awake()
    {
        singleton = this;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
       isOnGround = Physics.CheckSphere(transform.position + origin,radius,layerMask);

       
    }

    
    void OnDrawGizmos()
    {
        if(isOnGround) Gizmos.color = Color.gray;
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.position + origin,radius);
    }
}
