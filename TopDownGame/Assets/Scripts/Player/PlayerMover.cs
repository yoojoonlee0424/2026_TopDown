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

        [Header("스테미나 시스템")]
        [SerializeField] private float runStaminaCost = 5;
        [SerializeField] private float minStamina = 100;

        private bool isSprint = false;
        private Rigidbody2D body2D;
        protected Vector3 currentInput;
        public Vector3 CurrentInput => currentInput;

        private Stamina stamina;

        private void Awake()
        {
            body2D = GetComponent<Rigidbody2D>();
            stamina = GetComponent<Stamina>();
        }


        private void FixedUpdate()
        {
            SprintRun();
            
        }

        private void SprintRun()
        {
            if (!isSprint || controller.isAiming)
            {
                body2D.linearVelocity = movementSpeed * currentInput * Time.fixedDeltaTime;
            }
            else if(stamina.currentStamina >= minStamina)
            {
                body2D.linearVelocity = SprintSpeed * currentInput * Time.fixedDeltaTime;
                stamina.StaminaCost(runStaminaCost);
            }
        }

        #region
        private void OnSprint()
        {
            isSprint = true;
        }

        private void OnSprintRelease()
        {
            isSprint = false;
        }
        #endregion

    }
}

