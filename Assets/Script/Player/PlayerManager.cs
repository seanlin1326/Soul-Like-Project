using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulLike
{
    public class PlayerManager : MonoBehaviour
    {
        CameraHandler cameraHandler;
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        Animator animator;
        public bool isInteracting;
        [Header("player Flag")]
        public bool isSprinting;
        // Start is called before the first frame update
        void Start()
        {
            playerLocomotion = GetComponent<PlayerLocomotion>();
            cameraHandler = CameraHandler.instance;
            inputHandler = GetComponent<InputHandler>();
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            float _delta = Time.deltaTime;

            isInteracting = animator.GetBool("IsInteracting");
           
            inputHandler.TickInput(_delta);
            playerLocomotion.HandleMovement(_delta);
            playerLocomotion.HandleRollingAndSprinting(_delta);
        }
        private void FixedUpdate()
        {
            float _delta = Time.fixedDeltaTime;
            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(_delta);
                cameraHandler.HandleCameraRotation(_delta,inputHandler.mouseX,inputHandler.mouseY);
            }
        }
        private void LateUpdate()
        {
            isSprinting = inputHandler.b_Input;
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
        }
    }
}
