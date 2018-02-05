using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public interface IMover
    {

        // Use this for initialization
        void Init(float moveSpeed, float turnSpeed);

        // Update is called once per frame
        void Move(float amount);
        void Turn(float amount);

        void Move(Vector3 direction);
        void Turn(Vector3 target);
    }
}
