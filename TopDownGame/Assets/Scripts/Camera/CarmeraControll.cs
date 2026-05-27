using UnityEngine;


namespace TopDown.CameraControl
{
    public class CarmeraControll : MonoBehaviour
    {
        [SerializeField] private Transform playertransform;
        [SerializeField] private float displacementMultiplir = 0.15f;
        private float zPosition = -10;

        private void Update()
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cameraDisplacement = (mousePosition - playertransform.position) * displacementMultiplir;

            Vector3 finalCameraPosition = playertransform.position + cameraDisplacement;
            finalCameraPosition.z = zPosition;
            transform.position = finalCameraPosition;
        }
    }
}

