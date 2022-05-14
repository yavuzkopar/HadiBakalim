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
    public bool isOnGround;
    [HideInInspector] public Rigidbody _rigidbody;
    [HideInInspector] public GameObject normalCamera;

    [Header("Stats")] 
    [SerializeField] private float movementSpeed = 5;

   [SerializeField] private float fullSpeed = 5f;

    [SerializeField] private float rotationSpeed = 10;

    [Space(10)]
	[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	public float JumpTimeout = 0.50f;
	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	public float FallTimeout = 0.15f;

    [Space(10)]
	[Tooltip("The height the player can jump")]
	public float JumpHeight = 1.2f;
	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	public float Gravity = -15.0f;

	private float _jumpTimeoutDelta;
	private float _fallTimeoutDelta;
    public float _verticalVelocity;
    PlayerActions _playerActions;

    bool canDo;

        
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
     //   cameraObject = Camera.main.transform;
    // _playerActions = GetComponent<PlayerActions>();
        
    }
      public void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new PlayerActions();
        }
        _playerActions.Enable();
    }

    
       
    
    private void Update()
    {
        GroundCheck();
     //   JumpAndGravity();
        

        if(RingMenuController.Singleton.ringMenuParent.activeSelf) 
        {
          //  moveDirection = Vector3.zero;
          //  movementSpeed = 0f;
          inputHandler.Sifirla();
            
            
        }
        if (TPoint.singleton.canTransform)
        {
            inputHandler.TickInput(Time.deltaTime);
        }
        
        
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        
      //  moveDirection.y = 0;
      
      moveDirection.Normalize();
        movementSpeed = PlayerInfo.singleton.movementSpeed;
        moveDirection *= movementSpeed;
        moveDirection.y = _verticalVelocity;
        
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
        
        if(!_playerActions.PlayerMovement.SagClick.inProgress )
            LookAtT(Time.deltaTime);
        else
            HandleRotation(Time.deltaTime);
        
        
        
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
      public  Vector3 origin;
    [SerializeField] float radius,gravity,sayac;
    [SerializeField] LayerMask layerMask;
    RaycastHit hit1;
    void GroundCheck()
    {
         canDo = Physics.CheckSphere(transform.position + origin,radius,layerMask);
         bool deneyim = Physics.SphereCast(transform.position+origin,radius/2, Vector3.down,out RaycastHit hitt,origin.y,layerMask);
          bool deneyimmm = Physics.Raycast(transform.position+origin, Vector3.down,out hit1,origin.y+0.1f,layerMask);
       //  Debug.DrawRay(transform.position + origin,Vector3.down,Color.magenta);
       //  Debug.DrawLine(transform.position+origin,hitt.point,Color.magenta);
         if (!deneyimmm)
         {
              _verticalVelocity += Gravity * Time.deltaTime;
              isOnGround = false;
         }
         else
         {
             _verticalVelocity = 0f;
           //  transform.position = hitt.point;
           transform.position = hit1.point;
           isOnGround = true;
             if (_playerActions.PlayerMovement.Jump.triggered && _jumpTimeoutDelta <= 0.0f)
			{
                
				// the square root of H * -2 * G = how much velocity needed to reach desired height
				_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }
         }
    }
 
    
    void OnDrawGizmosSelected()
    {
        if(Physics.Raycast(transform.position+origin, Vector3.down,out hit1,origin.y))
            Gizmos.color = Color.blue;
        else
            Gizmos.color = Color.black;

        Gizmos.DrawSphere(hit1.point,0.1f);
        Gizmos.DrawRay(hit1.point,hit1.normal);
    }
    void JumpAndGravity()
    {
       
        if (canDo)
		{
			// reset the fall timeout timer
			_fallTimeoutDelta = FallTimeout;
			// update animator if using character
			
			// stop our velocity dropping infinitely when grounded
			if (_verticalVelocity < 0.0f)
			{
			//	_verticalVelocity = -2f;
                _verticalVelocity = 0f;
			}
			// Jump
			if (_playerActions.PlayerMovement.Jump.triggered && _jumpTimeoutDelta <= 0.0f)
			{
                Debug.Log("jumped");
				// the square root of H * -2 * G = how much velocity needed to reach desired height
				_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				// update animator if using character
			//	if (_hasAnimator)
			//	{
			//		_animator.SetBool(_animIDJump, true);
			//	}
			}
			// jump timeout
			if (_jumpTimeoutDelta >= 0.0f)
			{
				_jumpTimeoutDelta -= Time.deltaTime;
			}
		}
		else
		{
			// reset the jump timeout timer
			_jumpTimeoutDelta = JumpTimeout;
			// fall timeout
			if (_fallTimeoutDelta >= 0.0f)
			{
				_fallTimeoutDelta -= Time.deltaTime;
			}
		//	else
		//	{
		//		// update animator if using character
		//		if (_hasAnimator)
		//		{
		//			_animator.SetBool(_animIDFreeFall, true);
		//		}
		//	}
			// if we are not grounded, do not jump
		// 	_input.jump = false;
        _verticalVelocity += Gravity * Time.deltaTime;
		}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
		//	if (_verticalVelocity < _terminalVelocity)
		//	{
            
				
		//	}
    }


    #endregion

   
}
