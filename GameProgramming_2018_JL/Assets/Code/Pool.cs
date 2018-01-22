using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame
{
    public class Pool<T>
        where T : Component
    {
        // The initial size of the pool.
        private int _poolSize;

        // The prefab from which all objects in the pool are instantiated.
        private T _objectPrefab;

        // When the pool runs out of objects, should the pool grow or just return null.
        private bool _shouldGrow;

        // The list containing all of the objects in this pool. 
        private List<T> _pool;

        private Action<T> _initMethod;

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

        public Pool(int poolSize, bool shouldGrow, T prefab, Action<T> initMethod)
            : this(poolSize, shouldGrow, prefab)
        {
            _initMethod = initMethod;
            foreach (var item in _pool)
            {
                _initMethod(item);
            }
        }

        private T AddObject(bool isActive = false)
        {
            T component = UnityEngine.Object.Instantiate(_objectPrefab);

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
