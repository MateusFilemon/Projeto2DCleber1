using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : Character2D
{

    Player photonPlayer;

    protected override void Update()
    {
        
        base.Update();
        if (!photonView.IsMine) return;
        PlayerInputs();
    }

    protected override void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        base.FixedUpdate();
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

        photonView.RPC(nameof(AttackPun), RpcTarget.All);

       
    }

    [PunRPC]
    protected virtual void AttackPun()
    {

        animator.SetTrigger("Attack1");
      
    }

    public void SpawnArrow()
    {
        GameObject _arrow = Instantiate(arrowPrefab, attackPos.position, Quaternion.identity);
        Vector2 _direction = new Vector2(transform.localScale.x, 0);
        _arrow.GetComponent<Arrow>().Initialize(_direction, damage);
    }

}
