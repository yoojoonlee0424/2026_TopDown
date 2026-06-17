using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    private AgentAnimations agentAnimations;
    private AgentMover agentMover;

    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private Transform attackPoint;
    //private WeaponParent weaponParent;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Update()
    {
        //pointerInput = GetPointerInput();
        //movementInput = movement.action.ReadValue<Vector2>().normalized;

        agentMover.MovementInput = MovementInput;
        //weaponParent.PointerPosition = pointerInput;
        AnimateCharacter();
    }

    public void PerformAttack()
    {
        //weaponParent.Attack();
        GameObject melee = Instantiate(meleePrefab, attackPoint.position, attackPoint.rotation, null);
        
    }

    private void Awake()
    {
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        //weaponParent = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    private void AnimateCharacter()
    {
        agentAnimations.RotateToPointer();
        //agentAnimations.PlayAnimation(MovementInput);
        
    }



}