using TopDown.Movement;
using UnityEngine;


[RequireComponent (typeof(Animator))]
public class LegsAnime : MonoBehaviour
{
    [SerializeField] private PlayerMover playerMover;
    private Animator legsAnimator;

    private void Awake()
    {
        legsAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        legsAnimator.SetBool("moving", playerMover.CurrentInput != Vector3.zero);
    }



}
