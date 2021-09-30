using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulLike
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool rollFlag;
        public bool sprintFlag;
        private float rollInputTimer;
       
        PlayerController inputActions;
       

        Vector2 movementInput;
        Vector2 cameraInput;
        private void Awake()
        {
           
        }
        private void Start()
        {
          
        }
        
        private void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerController();
                inputActions.PlayerMovement.Movement.performed += _j => movementInput = _j.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += _i => cameraInput = _i.ReadValue<Vector2>();
            }
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void TickInput(float _delta) 
        {
            MoveInput(_delta);
            HandleRollInput(_delta);
        }
        private void MoveInput(float _delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) +Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
        private void HandleRollInput(float _delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            if (b_Input)
            {
                rollInputTimer += _delta;
                sprintFlag = true;
            }
            else
            {
                if(rollInputTimer>0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }
        }
    }
}