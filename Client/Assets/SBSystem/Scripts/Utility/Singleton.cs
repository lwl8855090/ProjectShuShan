using UnityEngine;
using System;
using System.Collections;

namespace SB
{
    public class Singleton<T>
    {
        private static T _instance = default(T);
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)Activator.CreateInstance(typeof(T));
                }
                return _instance;
            }
        }

        public void DestroyInstance()
        {
            _instance = default(T);
        }


        public virtual void Initilize() { }
        public virtual void Update() { }
        public virtual void Reset() { }
    }
}
