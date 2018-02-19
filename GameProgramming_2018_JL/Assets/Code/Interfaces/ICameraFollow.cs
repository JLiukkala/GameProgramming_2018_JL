using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public interface ICameraFollow
    {
        void SetAngle(float angle);
        void SetDistance(float distance);
        void SetTarget(Transform targetTransform);
    }
}
