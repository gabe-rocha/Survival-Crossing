using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttacking1h : IState {
    Player player;
    private float horizontal;
    private float vertical;
    private Vector3 dirHor;
    private Vector3 dirVer;
    private Vector3 direction;
    private float attackStartTime = float.NegativeInfinity;
    private float animationLength, attacksPerSecond;

    public PlayerStateAttacking1h(Player player) {
        this.player = player;

        foreach (var animationClip in player.animPlayer.runtimeAnimatorController.animationClips) {
            switch (animationClip.name) {
                case "KayKit Animated Character_Attack(1h)":
                    animationLength = animationClip.length;
                    break;
                default:
                    break;
            }
        }
    }

    public void OnEnter() {
        Debug.Log("Player State: Attacking1h");
        Attack();
    }

    public void OnExit() { }

    public IState Tick() {

        //Animation completed?
        if(Time.time >= attackStartTime + animationLength) {
            if(Input.GetAxis("Horizontal") < -player.walkDeadzone || Input.GetAxis("Horizontal") > player.walkDeadzone ||
                Input.GetAxis("Vertical") < -player.walkDeadzone || Input.GetAxis("Vertical") > player.walkDeadzone) {
                //joystick moved
                return player.statePlayerWalking;
            } else {
                return player.statePlayerIdle;
            }
        }

        //Player can attack again in the middle of this attack?
        if(Time.time >= attackStartTime + 1f / attacksPerSecond) {
            //Can attack again
            if(CheckWantToAttackAgain()) {
                Attack();
                return this;
            }
        }

        Rotate();

        return this;
    }

    private bool CheckWantToAttackAgain() {
        return Input.GetButtonDown("Fire1");
    }
    private void Attack() {
        attackStartTime = Time.time;
        attacksPerSecond = player.attacksPerSecond;
        player.animPlayer.SetTrigger("Attack1h");
        Debug.Log($"Attack Start Time: {attackStartTime}");
    }

    private void Rotate() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(horizontal < -player.walkDeadzone || horizontal > player.walkDeadzone ||
            vertical < -player.walkDeadzone || vertical > player.walkDeadzone) {
            dirHor = new Vector3(horizontal, 0f, -horizontal);
            dirVer = new Vector3(vertical, 0f, vertical);
            direction = dirHor + dirVer;
            player.playerModel.LookAt(player.playerModel.position + direction, Vector3.up);
        }
    }
}