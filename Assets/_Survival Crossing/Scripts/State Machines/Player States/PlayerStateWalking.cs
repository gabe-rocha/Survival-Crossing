using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalking : IState {
    Player player;
    private float horizontal;
    private float vertical;
    private Vector3 dirHor;
    private Vector3 dirVer;
    private Vector3 direction;

    public PlayerStateWalking(Player player) {
        this.player = player;
    }

    public void OnEnter() {
        player.animPlayer.SetBool("isMoving", true);

        Debug.Log("Player State: Walking");
    }

    public void OnExit() {
        player.animPlayer.SetBool("isMoving", false);
    }

    public IState Tick() {
        Move();
        if(horizontal >= -player.walkDeadzone && horizontal <= player.walkDeadzone &&
            vertical >= -player.walkDeadzone && vertical <= player.walkDeadzone) {
            return player.statePlayerIdle;
        }

        if(Input.GetButtonDown("Fire1")) {
            return player.statePlayerAttacking;
        }

        return this;

    }
    private void Move() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        dirHor = new Vector3(horizontal, 0f, -horizontal);
        dirVer = new Vector3(vertical, 0f, vertical);
        direction = dirHor + dirVer;

        //Rotate
        player.playerModel.LookAt(player.playerModel.position + direction, Vector3.up);

        Vector3.Normalize(direction);
        direction = new Vector3(direction.x * player.walkSpeed * Time.deltaTime, 0f, direction.z * player.walkSpeed * Time.deltaTime);
        player.characterController.Move(direction);
    }
}