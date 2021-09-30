using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulLike
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;
        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float sprintSpeed=7f;
        [SerializeField]
        float rotationSpeed = 10;
        [SerializeField] private bool isSprinting;
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();
        }
        private void Update()
        {
            float _delta = Time.deltaTime;
            isSprinting = inputHandler.b_Input;
            inputHandler.TickInput(_delta);
            HandleMovement(_delta);
            HandleRollingAndSprinting(_delta);
        }
        #region  -- Movement --
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float _delta)
        {
            Vector3 _targetDir = Vector3.zero;
            float _moveOverride = inputHandler.moveAmount;
            _targetDir = cameraObject.forward * inputHandler.vertical;
            _targetDir += cameraObject.right * inputHandler.horizontal;

            _targetDir.Normalize();
            _targetDir.y = 0;

            if (_targetDir == Vector3.zero)
                _targetDir = myTransform.forward;
            float _rs = rotationSpeed;
            Quaternion _tr = Quaternion.LookRotation(_targetDir);
            Quaternion _targetRotation = Quaternion.Slerp(myTransform.rotation,_tr,_rs * _delta);
            myTransform.rotation = _targetRotation;

            
        }
        private void HandleMovement(float _delta)
        {
            if (inputHandler.rollFlag)
                return;
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float _speed = movementSpeed;
            if (inputHandler.sprintFlag)
            {
                _speed = sprintSpeed;
                isSprinting = true;
            }
            moveDirection *= _speed;

            Vector3 _projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = _projectedVelocity;
            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0,isSprinting);

            if (animatorHandler.canRotate)
            {
                HandleRotation(_delta);
            }
        }

        public void HandleRollingAndSprinting(float _delta)
        {
            if (animatorHandler.animator.GetBool("IsInteracting"))
                return;
            if (inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;
               
               
                if (inputHandler.moveAmount > 0)
                {
                    animatorHandler.PlayTargetAnimation("Rolling", true);
                    moveDirection.y = 0;
                    Quaternion _rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = _rollRotation;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("StepBack", true);
                }
            }
        }
        #endregion
       
    }
}
