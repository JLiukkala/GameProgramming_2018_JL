using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Pool<T>
        where T : Component
    {
        private int _poolSize;
        private T _objectPrefab;
        private bool _shouldGrow;
        private List<T> _pool;

        public Pool(int poolSize, bool shouldGrow, T prefab)
        {
            _poolSize = poolSize;
            _shouldGrow = shouldGrow;
            _objectPrefab = prefab;

            _pool = new List<T>(_poolSize);

            for (int i = 0; i < _poolSize; i++)
            {
                AddObject();
            }
        }

        private T AddObject(bool isActive = false)
        {
            T component = Object.Instantiate(_objectPrefab);

            if (isActive)
            {
                Activate(component);
            }
            else
            {
                Deactivate(component);
            }

            _pool.Add(component);

            return component;
        }

        protected virtual void Activate(T component)
        {
            component.gameObject.SetActive(true);
        }

        protected virtual void Deactivate(T component)
        {
            component.gameObject.SetActive(false);
        }

        public T GetPooledObject()
        {
            T result = null;

            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].gameObject.activeSelf == false)
                {
                    result = _pool[i];
                    break;
                }
            }

            if (result == null && _shouldGrow)
            {
                result = AddObject();
            }

            if (result != null)
            {
                Activate(result);
            }

            return result;
        }

        public bool ReturnObject(T component)
        {
            bool result = false;

            foreach (var pooledObject in _pool)
            {
                if (pooledObject == component)
                {
                    Deactivate(component);
                    result = true;
                    break;
                }
            }

            if (!result)
            {
                Debug.LogError("Tried to return an object which doesn't belong to this pool!");
            }

            return result;
        }
    }
}
