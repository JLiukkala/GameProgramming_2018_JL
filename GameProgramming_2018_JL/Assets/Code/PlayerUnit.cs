﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class PlayerUnit : Unit
    {
        [SerializeField]
        private string horizontalAxis = "Horizontal";
        [SerializeField]
        private string verticalAxis = "Vertical";

        protected override void Update()
        {
            var input = ReadInput();
            Mover.Turn(input.x);
            Mover.Move(input.z);
        }

        private Vector3 ReadInput()
        {
            float movement = Input.GetAxis(verticalAxis);
            float turn = Input.GetAxis(horizontalAxis);
            return new Vector3(turn, 0, movement);
        }
    }
}
