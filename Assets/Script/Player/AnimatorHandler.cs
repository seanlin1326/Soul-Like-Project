using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulLike
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator animator;
        public InputHandler inputHandler;
        private PlayerLocomotion playerLocomotion;
        int vertical;
        int horizontal;
        public bool canRotate;


        public void Initialize()
        {
            animator = GetComponent<Animator>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            inputHandler = GetComponentInParent<InputHandler>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }
        public void UpdateAnimatorValues(float _verticalMovement,float _horizontalMove)
        {
            #region -- vertical --
            float _v = 0;
            if(_verticalMovement>0 && _verticalMovement < 0.55f)
            {
                _v = 0.5f;
            }
            else if(_verticalMovement > 0.55f)
            {
                _v = 1;
            }
            else if(_verticalMovement < 0 && _verticalMovement> -0.55f)
            {
                _v = -0.5f;
            }
            else if (_verticalMovement < -0.55f)
            {
                _v = -1;
            }
            else
            {
                _v = 0;
            }
            #endregion
            #region -- horizontal --
            float _h = 0;
            if(_horizontalMove >0 && _horizontalMove < 0.55f)
            {
                _h = 0.5f;
            }
            else if(_horizontalMove > 0.55f)
            {
                _h = 1;
            }
            else if(_horizontalMove <0 && _horizontalMove > -0.55f)
            {
                _h = -0.5f;
            }
            else if(_horizontalMove < -0.55f)
            {
                _h = -1;
            }
            else
            {
                _h = 0;
            }
            #endregion
            animator.SetFloat(vertical, _v, 0.1f, Time.deltaTime);
            animator.SetFloat(horizontal, _h, 0.1f, Time.deltaTime);
        }
        public void PlayTargetAnimation(string _targetAnim,bool _isInteracting)
        {
            animator.applyRootMotion = _isInteracting;
            animator.SetBool("IsInteracting", _isInteracting);
            animator.CrossFade(_targetAnim, 0.2f);
        }
        public void CanRotate()
        {
            canRotate = true;
        }
        public void StopRotate()
        {
            canRotate = false;
        }
        private void OnAnimatorMove()
        {
            if (inputHandler.isInteracting == false)
                return;
            float _delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 _deltaPosition = animator.deltaPosition;

            _deltaPosition.y = 0;
            Vector3 _velocity = _deltaPosition /_delta;
            playerLocomotion.rigidbody.velocity = _velocity;
           

        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
          
        }
    }
}
