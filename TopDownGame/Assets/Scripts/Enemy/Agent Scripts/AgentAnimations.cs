using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimations : MonoBehaviour
{
    [SerializeField] private Transform torso;
    [SerializeField] private Transform Player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player").transform;

    }

    public void RotateToPointer()
    {
        Vector2 playerPos = Player.transform.position;

        LookAt(torso, playerPos);
        //Debug.LogError(playerPos);

    }

    private void LookAt(Transform rotatedTransform, Vector3 target)
    {
        float lookAngle = AngleBetweenTwoPoints(transform.position, target) + 90;

        rotatedTransform.eulerAngles = new Vector3(0, 0, lookAngle);


    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void PlayAnimation(Vector2 movementInput)
    {
        animator.SetBool("Running", movementInput.magnitude > 0);

    }
}