using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NokiaSnakeGame.Core
{
    public class MonoSingletonGeneric<T> : MonoBehaviour where T: MonoSingletonGeneric<T> 
    {
        private static T m_Instance;
        public static T Instance { get { return m_Instance; } }

        protected virtual void Awake()
		{
            if (m_Instance != null)
                Destroy(gameObject);

            m_Instance = (T)this;        
		}
    }
}