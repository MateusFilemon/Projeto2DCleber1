using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character2D
{

    Player photonPlayer;

    protected override void Update()
    {
        base.Update();

        PlayerInputs();
    }
    void PlayerInputs()
    {

        x = Input.GetAxisRaw("Horizontal");
        if (onGround() && Input.GetButtonDown("Jump"))
            canJump = true;

        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= attackTime)
        {
            Attack();
        }

    }

    protected override void Attack()
    {
        base.Attack();

        animator.SetTrigger("Attack1");
    }





}
