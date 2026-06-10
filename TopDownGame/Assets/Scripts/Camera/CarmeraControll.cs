using UnityEngine;


namespace TopDown.CameraControl
{
    public class CarmeraControll : MonoBehaviour
    {
        [SerializeField] private Transform playertransform;
        [SerializeField] private float displacementMultiplir = 0.15f;
        private float zPosition = -10;

        public float cameraOffset = -10.0f;
        public float cameraHeight = 1f;
        public float cameraSpeed = 1f;

        private bool isAiming = false;

        private void Update()
        {
            if(isAiming) 
            {
                camAim();
            }
            else
            {
                camPlayer();
            }

        }

        private void camPlayer()
        {
            Vector3 targetPos = new Vector3(playertransform.position.x, playertransform.position.y + cameraHeight, cameraOffset);
            transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
        }

        private void camAim()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cameraDisplacement = (mousePosition - playertransform.position) * displacementMultiplir;

            Vector3 finalCameraPosition = playertransform.position + cameraDisplacement;
            finalCameraPosition.z = zPosition;
            transform.position = finalCameraPosition;
        }

        private void OnAim() // 마우스 오른쪽 버튼이 눌렸을 때
        {
            isAiming = true;
        }

        private void OnAimRelease() // 마우스 오른쪽 버튼이 떼졌을 때
        {
            isAiming = false;
        }

    }
}

