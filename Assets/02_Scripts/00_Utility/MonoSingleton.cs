using UnityEngine;

namespace TK.Utility
{
    /// <summary> 
    /// To access the heir by a static field "Instance".
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        [SerializeField] private bool dontDestroyOnLoad;
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance) CreateNewInstance();
                return _instance;
            }
        }

        private static void CreateNewInstance()
        {
            _instance = FindObjectOfType<T>(true);

            if (!_instance)
            {
                Debug.LogError($"The singleton object could not found! : {typeof(T)}");
            }
        }

        protected virtual void Awake()
        {
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }
    }
}