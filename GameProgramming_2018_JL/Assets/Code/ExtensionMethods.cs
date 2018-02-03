using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame
{
    public static class ExtensionMethods
    {
        public static TComponent GetOrAddComponent< TComponent >( this GameObject gameObject)
            where TComponent : Component
        {
            TComponent component = gameObject.GetComponent<TComponent>();
            if (component == null)
            {
                component = gameObject.AddComponent<TComponent>();
            }
            return component;
        }

        public static Component GetOrAddComponent(this GameObject gameObject, Type type)
        {
            Component component = gameObject.GetComponent(type);
            if (component == null)
            {
                component = gameObject.AddComponent(type);
            }
            return component;
        }

        public static TComponent GetComponentInInactiveParents<TComponent>
            (this GameObject gameObject)
            where TComponent : Component
        {
            return gameObject.GetComponentInInactiveParentsRecursive<TComponent>();
            //return gameObject.GetComponentInInactiveParentsIterative<TComponent>();
        }

        public static bool AddUnique<T>(this IList<T> list, T item)
        {
            if (list.Contains(item))
            {
                return false;
            }

            list.Add(item);
            return true;
        }

        private static TComponent GetComponentInInactiveParentsIterative<TComponent>
            (this GameObject gameObject)
            where TComponent : Component
        {
            TComponent result;
            Transform transform = gameObject.transform;
            do
            {
                result = transform.GetComponent<TComponent>();
                transform = transform.parent;
            } while (result == null && transform != null);

            return result;
        }

        private static TComponent GetComponentInInactiveParentsRecursive<TComponent>
            (this GameObject gameObject)
            where TComponent : Component
        {
            TComponent result = gameObject.GetComponent<TComponent>();
            if (result == null)
            {
                Transform parentTransform = gameObject.transform.parent;
                if (parentTransform != null)
                {
                    result = parentTransform.gameObject.
                        GetComponentInInactiveParentsRecursive<TComponent>();
                }
            }

            return result;
        }

        public static TComponent GetComponentInHierarchy<TComponent>
            (this GameObject gameObject, bool includeInactive = false)
            where TComponent : Component
        {
            return includeInactive
                ? gameObject.GetComponentInInactiveHierarchy<TComponent>()
                : gameObject.GetComponentInActiveHierarchy<TComponent>();
        }

        private static TComponent GetComponentInActiveHierarchy<TComponent>
            (this GameObject gameObject)
            where TComponent : Component
        {
            TComponent result =
                gameObject.GetComponentInChildren<TComponent>(includeInactive: false);
            if (result == null)
            {
                result = gameObject.GetComponentInParent<TComponent>();
            }

            return result;
        }

        private static TComponent GetComponentInInactiveHierarchy<TComponent>
            (this GameObject gameObject)
            where TComponent : Component
        {
            TComponent result =
                gameObject.GetComponentInChildren<TComponent>(includeInactive: true);
            if (result == null)
            {
                result = gameObject.GetComponentInInactiveParents<TComponent>();
            }

            return result;
        }
    }
}