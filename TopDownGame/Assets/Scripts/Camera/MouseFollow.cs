using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 positionMoveTo = camera.ScreenToWorldPoint(Input.mousePosition);
        positionMoveTo.z = 0;
        target.transform.position = Vector3.MoveTowards(target.transform.position, positionMoveTo, moveSpeed * Time.deltaTime);





    }
}
