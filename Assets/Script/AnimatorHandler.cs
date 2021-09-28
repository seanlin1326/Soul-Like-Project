using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulLike
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator animator;
        int vertical;
        int horizontal;
        public bool canRotate;


        public void Initialize()
        {
            animator = GetComponent<Animator>();
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
        public void CanRotate()
        {
            canRotate = true;
        }
        public void StopRotate()
        {
            canRotate = false;
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
