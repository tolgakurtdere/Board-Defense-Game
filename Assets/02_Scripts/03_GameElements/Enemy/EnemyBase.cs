using System;
using System.Collections;
using UnityEngine;

namespace BoardDefense.Enemy
{
    public abstract class EnemyBase : BlockItemBase, IMover, IDamageable
    {
        public static event Action OnEnemyDied;
        [SerializeField, Min(1)] protected int initialHealth = 1;
        public virtual int Health { get; protected set; }
        public abstract float Speed { get; }
        private Coroutine _moveRoutine;

        protected virtual void Start()
        {
            Health = initialHealth;
        }

        public override void Activate()
        {
            base.Activate();

            _moveRoutine = StartCoroutine(Move());
        }

        public IEnumerator Move()
        {
            while (true)
            {
                Board.Instance.GoNextBlock(this);

                yield return new WaitForSeconds(1 / Speed);
            }
        }

        public virtual void TakeDamage(int damageAmount)
        {
            Health -= damageAmount;

            if (Health <= 0)
            {
                Die();
            }

            print(name + " GET DAMAGE: HP: " + Health);
        }

        public virtual void Die()
        {
            print("DEAD! : " + name);
            StopCoroutine(_moveRoutine);
            Deactivate();
            Board.Instance.RemoveItem(this);

            OnEnemyDied?.Invoke();
        }
    }
}