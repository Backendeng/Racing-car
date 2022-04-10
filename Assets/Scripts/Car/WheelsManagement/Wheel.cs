﻿using System;
using UnityEngine;
using Utilities;

namespace Car.WheelsManagement
{
    public class Wheel : MonoBehaviour
    {
        public float WheelRPM => collider.rpm;

        [SerializeField] private WheelCollider collider;
        
        //temporary place for const values - will be moved to scriptable object
        private const float RotationSpeedMultiplier = 0.5f;
        private const float MAXWheelsRotationAngle = 30f;
        private float _initialAngle;
        private Transform _transform;
        
        

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _initialAngle = collider.steerAngle;
        }

        public void ApplyMotorTorque(float force)
        {
            collider.motorTorque = force;
        }
        
        public void ApplyBrakeTorque(float force)
        {
            collider.brakeTorque = force;
        }
        public void RotateWheel(float direction)
        {
            if (Mathf.Approximately(direction, 0) && !Mathf.Approximately(collider.steerAngle, _initialAngle))
            {
                //if there is no input in x axis - slowly center the wheels
                HandleAutoCenteringWheels();
            }
            else if (Mathf.Abs(collider.steerAngle) <= MAXWheelsRotationAngle || (collider.steerAngle * -direction) >= 0)
            {
                collider.steerAngle += direction * RotationSpeedMultiplier;
            }
           
        }
        
        public void UpdateWheel()
        {
            Vector3 pos;
            Quaternion rot;
            
            collider.GetWorldPose(out pos, out rot);

            _transform.rotation = rot;
            _transform.position = pos;
        }

        private void HandleAutoCenteringWheels()
        {
            collider.steerAngle += -Mathf.Sign(collider.steerAngle) * RotationSpeedMultiplier/2;

            if (Mathf.Abs(collider.steerAngle - _initialAngle) <= (RotationSpeedMultiplier +1))
            {
                collider.steerAngle = _initialAngle;
            }
        }
    }
}