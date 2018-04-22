using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Collectable : MonoBehaviour
    {

        // The amount of points that the collectable gives the player.
        [SerializeField]
        private int _pointsGiven;

        // Reference to the player unit.
        private GameObject _player;

        // Reference to the PlayerUnit script and its functionalities.
        private PlayerUnit _playerUnitScript;

        // Setting the references. 
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerUnitScript = _player.GetComponent<PlayerUnit>();
        }

        // If the player steps into the collider of the collectable, 
        // they get the amount of points that the collectable gives.
        // After that, the collectable game object is destroyed.
        public void OnTriggerEnter(Collider other)
        {
            _playerUnitScript._points += _pointsGiven;
            //Debug.Log("Got " + _pointsGiven + " points!");

            Destroy(gameObject);
        }
    }
}
