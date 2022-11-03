using System.Collections.Generic;
using System.Linq;
using BoardDefense.DefenseItem;
using BoardDefense.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoardDefense
{
    public class Block : MonoBehaviour, IDropHandler
    {
        [ShowInInspector, ReadOnly] public int RowIndex { get; private set; } = -1;
        [ShowInInspector, ReadOnly] public int ColumnIndex { get; private set; } = -1;
        [ShowInInspector, ReadOnly] private bool _isDroppable;
        [ShowInInspector, ReadOnly] private List<IItem> _items = new();

        public void OnDrop(PointerEventData eventData)
        {
            if (!_isDroppable) return;

            var obj = eventData.pointerDrag;
            if (obj)
            {
                var defender = obj.GetComponent<IDefender>();
                if (defender != null)
                {
                    AddItem(defender);
                    defender.StartAttack();
                }
            }
        }

        public void Initialize(int rowIndex, int columnIndex, bool droppable)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            _isDroppable = droppable;
        }

        public void AddItem(IItem item)
        {
            if (_items.Contains(item))
            {
                Debug.LogError("Item is already in this block!");
                return;
            }

            item.SetPosition(transform.position);
            _items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            if (!_items.Contains(item))
            {
                Debug.LogError("Item is not in this block!");
                return;
            }

            _items.Remove(item);
        }

        public bool IsItemHere(IItem item)
        {
            return _items.Contains(item);
        }

        public bool GetAttacked(int damage)
        {
            var isAnyAttackHappened = false;

            foreach (var damageable in _items.ToList().OfType<IDamageable>())
            {
                damageable.TakeDamage(damage);
                isAnyAttackHappened = true;
            }

            return isAnyAttackHappened;
        }
    }
}