using TopDown.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Movement
{
    public class PlayerRotation : Rotator
    {
        [Header("Torso & Legs")]
        [SerializeField] private Transform torso;
        [SerializeField] private Transform legs;

        [Header("Mover Refernce")]
        [SerializeField] private PlayerMover playerMover;

        private InventoryManager _inventoryManager;

        private void Awake()
        {
            _inventoryManager = FindAnyObjectByType<InventoryManager>();
        }

        private void OnLook(InputValue value)
        {
            if (_inventoryManager.menuActivated)
            {
                return;
            }
            Vector2 mousePosition =Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
            LookAt(torso, mousePosition);
        }

        private void Update()
        {
            if (_inventoryManager.menuActivated)
            {
                return;
            }
            Vector3 legsLookPoint = transform.position + new Vector3(playerMover.CurrentInput.x, playerMover.CurrentInput.y);
            LookAt(legs, legsLookPoint);
        }
    }
}

