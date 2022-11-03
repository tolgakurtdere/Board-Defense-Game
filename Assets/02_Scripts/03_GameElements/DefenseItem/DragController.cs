using TK.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoardDefense.DefenseItem
{
    [RequireComponent(typeof(Collider2D))]
    public class DragController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private Collider2D _collider;

        private void OnEnable()
        {
            LevelManager.OnLevelStopped += OnLevelStopped;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelStopped -= OnLevelStopped;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnLevelStopped(bool isSuccess)
        {
            _collider.enabled = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _collider.enabled = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _collider.enabled = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }
}