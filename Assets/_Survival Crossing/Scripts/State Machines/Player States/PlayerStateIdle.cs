using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : IState {
    Player player;
    public PlayerStateIdle(Player player) {
        this.player = player;
    }

    public void OnEnter() {
        Debug.Log("Player State: Idle");
    }

    public void OnExit() { }

    public IState Tick() {

        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        if (hor < -player.walkDeadzone || hor > player.walkDeadzone ||
            ver < -player.walkDeadzone || ver > player.walkDeadzone) {
            //joystick moved
            return player.statePlayerWalking;
        }

        if (Input.GetButtonDown("Fire1")) {
            return player.statePlayerAttacking;
        }

        return this;
    }
}