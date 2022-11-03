using TK.Manager;
using UnityEngine;

namespace BoardDefense
{
    public abstract class BlockItemBase : MonoBehaviour, IItem
    {
        private void OnEnable()
        {
            LevelManager.OnLevelStopped += OnLevelStopped;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelStopped -= OnLevelStopped;
        }

        protected virtual void Awake()
        {
        }

        private void OnLevelStopped(bool obj)
        {
            StopAllCoroutines();
            enabled = false;
        }

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public virtual void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}