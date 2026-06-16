using UnityEngine;
using TopDown.Shooting;

namespace TopDown.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float SprintSpeed;
        [SerializeField] private GunController controller;

        private bool isSprint = false;
        private Rigidbody2D body2D;
        protected Vector3 currentInput;
        public Vector3 CurrentInput => currentInput;

        private void Awake()
        {
            body2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            if(!isSprint || controller.isAiming)
            {
                body2D.linearVelocity = movementSpeed * currentInput * Time.fixedDeltaTime;
            }
            else
            {
                body2D.linearVelocity = SprintSpeed * currentInput * Time.fixedDeltaTime;
            }
            
        }

        private void OnSprint()
        {
            isSprint = true;
        }

        private void OnSprintRelease()
        {
            isSprint = false;
        }

    }
}

