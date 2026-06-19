using UnityEngine;


namespace TopDown.CameraControl
{
    public class CarmeraControll : MonoBehaviour
    {
        [SerializeField] private Transform playertransform;
        [SerializeField] private Camera cam;
        [SerializeField] private GameObject defCam;
        [SerializeField] private GameObject aimCam;
        [SerializeField] private float displacementMultiplir = 0.15f;
        [SerializeField] private float moveSpeed = -10;
        private float zPosition = -10;

        

        //public float defCamzoom = 4.5f;
        //public float aimCamzoom = 2f;

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
            aimCam.SetActive(false);
            defCam.SetActive(true);

            aimCam.transform.position = Vector3.MoveTowards(aimCam.transform.position, defCam.transform.position, moveSpeed * Time.deltaTime);
        }

        private void camAim()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cameraDisplacement = (mousePosition - playertransform.position) * displacementMultiplir;

            Vector3 finalCameraPosition = playertransform.position + cameraDisplacement;
            finalCameraPosition.z = zPosition;
            aimCam.transform.position = finalCameraPosition;

            aimCam.SetActive(true);
            defCam.SetActive(false);

            //cam.orthographicSize = aimCamzoom;
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

