using UnityEngine;

namespace TopDown.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        private Rigidbody2D body2D;
        protected Vector3 currentInput;
        public Vector3 CurrentInput => currentInput;

        private void Awake()
        {
            body2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            body2D.linearVelocity = movementSpeed * currentInput * Time.fixedDeltaTime;
        }


    }
}

