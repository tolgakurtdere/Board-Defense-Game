using System.Collections;
using UnityEngine;

namespace BoardDefense.DefenseItem
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class DefenseItemBase : BlockItemBase, IDefender
    {
        public abstract int Damage { get; }
        public abstract int Range { get; }
        public abstract int Interval { get; }
        public abstract Direction Direction { get; }

        private Collider2D _collider;

        protected override void Awake()
        {
            base.Awake();

            _collider = GetComponent<Collider2D>();
        }

        public virtual void StartAttack()
        {
            _collider.enabled = false;

            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            var targets = Board.Instance.FindTargetBlocks(this);

            while (true)
            {
                var attackCount = 0;

                foreach (var target in targets)
                {
                    if (target.GetAttacked(Damage)) attackCount++;
                }

                if (attackCount == 0) yield return null;
                else yield return new WaitForSeconds(Interval);
            }
        }


        #region Development - DrawGizmos

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;

            Gizmos.color = Color.red;
            var targets = Board.Instance.FindTargetBlocks(this);
            foreach (var targetBlock in targets)
            {
                Gizmos.DrawWireSphere(targetBlock.transform.position, 0.2f);
            }
        }
#endif

        #endregion
    }
}