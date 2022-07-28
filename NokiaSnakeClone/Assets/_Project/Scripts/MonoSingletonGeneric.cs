using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NokiaSnakeGame.Core
{
    public class MonoSingletonGeneric<T> : MonoBehaviour where T: MonoSingletonGeneric<T> 
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
		{
            if (instance != null)
                Destroy(gameObject);

            instance = (T)this;        
		}
    }
}