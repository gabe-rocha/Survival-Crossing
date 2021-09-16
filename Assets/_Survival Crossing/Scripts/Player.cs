using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] internal float walkSpeed = 1f;
    [SerializeField] internal float walkDeadzone = 0.2f;
    [SerializeField] internal CharacterController characterController;
    [SerializeField] internal Animator animPlayer;
    [SerializeField] internal Transform playerModel;
    [SerializeField] internal float attacksPerSecond = 2f;

    //
    internal IState statePlayerIdle, statePlayerWalking, statePlayerAttacking;

    private StateMachine playerStateMachine;

    // Start is called before the first frame update
    void Start() {
        statePlayerIdle = new PlayerStateIdle(this);
        statePlayerWalking = new PlayerStateWalking(this);
        statePlayerAttacking = new PlayerStateAttacking1h(this);

        playerStateMachine = new StateMachine();
        playerStateMachine.SetState(statePlayerIdle);
    }

    // Update is called once per frame
    void Update() {
        playerStateMachine.Tick();
    }
}