using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private InputHandler inputHandler;
    private Animator _animator;
    private PlayerLocomotion _playerLocomotion;
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        _animator = GetComponent<Animator>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
      //  _animator.SetFloat("Horizontal",inputHandler.horizontal * _playerLocomotion.targetdirect.z);
      //  _animator.SetFloat("Vertical",inputHandler.vertical* _playerLocomotion.targetdirect.x);
      Vector3 localVelocity = transform.InverseTransformDirection(_playerLocomotion.moveDirection.normalized);
      float fspeedZ = localVelocity.z;
      float fspeedX = localVelocity.x;
            
      _animator.SetFloat("Vertical", fspeedZ);
      _animator.SetFloat("Horizontal", fspeedX);
      if(_playerLocomotion.isOnGround)
        inputHandler.Jump(_animator);
    }
}
