using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class PlayerUnit : Unit
    {
        [SerializeField]
        private string horizontalAxis = "Horizontal";
        [SerializeField]
        [Tooltip("The name of the vertical axis")]
        private string verticalAxis = "Vertical";

        // The amount of points that the player has.
        [HideInInspector]
        public int _points;

        // The amount of points needed to win the game.
        public int _pointsNeededToWin;

        // The amount of lives that the player has left.
        public int _livesLeft = 2;

        // A reference to the score text.
        [SerializeField]
        private Text _pointsText;

        // A reference to the text indicating the amount of lives.
        [SerializeField]
        private Text _livesText;

        protected override void Update()
        {
            var input = ReadInput();
            Mover.Turn(input.x);
            Mover.Move(input.z);

            // TODO: Refactor me! Extract method.
            bool shoot = Input.GetButton("Fire1");
            if (shoot)
            {
                Weapon.Shoot();
            }

            // If the player gets enough points 
            // to win, they win the game.
            if (_points == _pointsNeededToWin)
            {
                // Game Over! Player Wins!
                Debug.Log("You win!");
            }

            //if (Health.CurrentHealth == 0)
            //{
            //    Debug.Log("Do we get here?");
            //    _livesLeft--;
            //}

            // If the player loses their three lives, the game is lost.
            if (_livesLeft == -1)
            {
                // Game Over! Player Loses!
                Debug.Log("You lose!");
            }

            // Showing the amount of points the player has in the UI.
            _pointsText.text = "Points: " + _points.ToString();

            // Showing the amount of lives the player has in the UI.
            _livesText.text = "Lives: " + _livesLeft.ToString();

            Debug.Log(_points);
        }

        private Vector3 ReadInput()
        {
            float movement = Input.GetAxis(verticalAxis);
            float turn = Input.GetAxis(horizontalAxis);
            return new Vector3(turn, 0, movement);
        }
    }
}
