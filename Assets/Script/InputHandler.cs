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

        PlayerController inputActions;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerController();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
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
        }
        private void MoveInput(float _delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) +Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
    }
}