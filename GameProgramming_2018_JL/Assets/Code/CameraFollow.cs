using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class CameraFollow : MonoBehaviour, ICameraFollow
    {
        // This variable determines how far back the camera is from the target.
        // The value for distance can be changed from the Unity Editor.
        [SerializeField]
        private float _distance;

        // This variable determines the angle from which the camera is looking at 
        // the target. The value for distance can be changed from the Unity Editor.
        [SerializeField]
        private float _angle;

        // This is the transform for the target object. 
        // It must be set in the Unity Editor.
        [SerializeField]
        private Transform _targetTransform;
        
        // Setting up the necessary methods that must be 
        // inherited from the interface ICameraFollow.
        public void SetDistance(float distance)
        {
            distance = _distance;
        }

        public void SetAngle(float angle)
        {
            angle = _angle;
        }

        public void SetTarget(Transform targetTransform)
        {
            targetTransform = _targetTransform;
        }

        // When implementing a follow camera, it is recommended to use LateUpdate because 
        // it is called after all other Update functions have been called. That means 
        // it is capable of tracking objects that have moved in the other Updates.
        void LateUpdate()
        {
            // Sets the camera's position to a new variable.
            Vector3 cameraPosition = transform.position;
            cameraPosition = _targetTransform.position;

            // Performs the conversion from degrees to radians.
            // I put it in a separate float variable since it
            // didn't seem to work without doing that.
            float angle = _angle * Mathf.Deg2Rad;

            // Finds out the height from target by multiplying the distance
            // of the camera with the cosine of the angle of the camera.
            float heightFromTarget = Mathf.Cos(angle) * _distance;
            float horizontalDistanceFromTarget = Mathf.Sin(angle) * _distance;

            // Creates a variable called direction that is the camera's 
            // transform's forward. It's minus value is then multiplied 
            // with the horizontal distance from the target.
            Vector3 direction = transform.forward;
            direction = direction.normalized;
            direction = -direction * horizontalDistanceFromTarget;

            // Updating the camera's position on each of the three axes
            // with the direction (x and z) and the height (y).
            cameraPosition.x += direction.x;
            cameraPosition.z += direction.z;
            cameraPosition.y += heightFromTarget;

            // Setting the camera's transform's position
            // to be the same as cameraPosition.
            transform.position = cameraPosition;

            // Making the camera look at the target.
            transform.LookAt(_targetTransform);
        }
    }
}
