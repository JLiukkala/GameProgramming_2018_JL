using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class CollectableSpawner : MonoBehaviour
    {
        // An array of collectables.
        [SerializeField]
        private GameObject[] _collectables;

        // An index that stores a random value between zero and 
        // the length of the _collectables game object array.
        int _index;

        // A float that keeps track of time that has passed.
        private float _time = 0;

        // A float that determines how long it takes for a collectable to spawn.
        public float _timeBeforeSpawn;

        void Update ()
        {
            // If the time passed is less than the time before a collectable 
            // spawns, time is incremented by Time.deltaTime.
            if (_time < _timeBeforeSpawn)
            {
                _time += Time.deltaTime;
            }

            // If the time passed is more than or equal to the time before a collectable 
            // spawns, a collectable is spawned into the scene and time is reset to zero.
            if (_time >= _timeBeforeSpawn)
            {
                Spawn();
                _time = 0;
            }
        }

        // Spawns the collectables.
        public GameObject Spawn()
        {
            // This determines the possible positions that a collectable can spawn in.
            Vector3 spawnPosition = new Vector3(Random.Range(-34f, 34f), 0.25f, Random.Range(38f, -38f));

            // Chooses a random collectable out of the array of collectables.
            _index = Random.Range(0, _collectables.Length);

            // Instantiates a random collectable in a random spawn position.
            GameObject spawnedCollectible = Instantiate(_collectables[_index], spawnPosition, transform.rotation);

            // Returns the collectable game object.
            return spawnedCollectible;
        }
    }
}
