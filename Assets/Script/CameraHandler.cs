using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulLike
{
    public class CameraHandler : MonoBehaviour
    {
        public static CameraHandler instance;
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        [SerializeField]private LayerMask ignoreLayers;
        private Vector3 cameraFollowVelocity = Vector3.zero;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float targetPosition;
        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35;
        public float maximumPivot = 35;

        public float cameraSphereRadius=0.2f;
        public float cameraCollisionOffset = 0.2f;
        public float minimumCollisionOffset = 0.2f;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
            
        }
        public void FollowTarget(float _delta)
        {
            
            Vector3 _targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position,ref cameraFollowVelocity,_delta/followSpeed);
            myTransform.position = _targetPosition;
            HandleCameraCollision(_delta);
        }
        public void HandleCameraRotation(float _delta,float _mouseXInput,float _mouseYInput)
        {
            lookAngle += (_mouseXInput * lookSpeed) / _delta;
            pivotAngle -= (_mouseYInput * pivotSpeed) / _delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 _rotation = Vector3.zero;
            _rotation.y = lookAngle;
            Quaternion _targetRotation = Quaternion.Euler(_rotation);
            //myTransform.rotation = _targetRotation;
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, _targetRotation, _delta );
            _rotation = Vector3.zero;
            _rotation.x = pivotAngle;

            _targetRotation = Quaternion.Euler(_rotation);
            
            cameraPivotTransform.localRotation = _targetRotation;
          
        }
        private void Update()
        {
          
        }
        private void HandleCameraCollision(float _delta)
        {
            targetPosition = defaultPosition;
            RaycastHit _hit;
            Vector3 _direction = cameraTransform.position - cameraPivotTransform.position;
            _direction.Normalize();
            
            if(Physics.SphereCast(
                cameraPivotTransform.position,cameraSphereRadius,_direction,out _hit,Mathf.Abs(targetPosition)
                , ignoreLayers))
            {
                float _dis = Vector3.Distance(cameraPivotTransform.position, _hit.point);
                targetPosition = -(_dis - cameraCollisionOffset);
               
            }
           
            if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition = -minimumCollisionOffset;
            }
            //Debug.Log(Mathf.Abs(targetPosition));

            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, _delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
        }
        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
          
           
           
        }
    }
}
