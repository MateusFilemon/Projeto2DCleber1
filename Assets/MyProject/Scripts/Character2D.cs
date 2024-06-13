using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Rigidbody2D))]
public class Character2D : MonoBehaviourPun
{

    protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundDistance;
    [SerializeField] protected LayerMask groundLayer;
    protected bool canJump;
    protected float x, y;


    protected float attackTime;
    [SerializeField] protected GameObject arrowPrefab;
   
    [SerializeField]  protected float damage;
    [SerializeField] protected float AttackSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected Transform attackPos;
    [SerializeField] protected LayerMask enemyLayer;


    [SerializeField] protected float currentLife;
    [SerializeField] protected float maxLife;
    [SerializeField] protected bool dead;


    protected virtual void Awake() 
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

        Flip();
        Animations();
    }

    protected virtual void FixedUpdate()
    {
        Move();

        if(canJump) 
        {
            Jump();
        }
    }

    protected virtual void Move() 
    { 
        y = rb.velocity.y;
        rb.velocity = new Vector2 (x * moveSpeed, y);
    }

    protected virtual void Jump() 
    { 
        canJump = false;
        Vector2 _velocity = rb.velocity;
        //velocidade sempre 0 para quando aplicar força ser sempre o mesmo valor
        _velocity.y = 0f;
        rb.AddForce(Vector2.up * jumpForce);
    }

    protected bool onGround() 
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundLayer);
    }

    protected virtual void Flip() 
    { 
        //localScale escala xyz do objeto
        Vector3 _localScale = transform.localScale;
        Vector2 _velocity = rb.velocity;

        if ( (_localScale.x > 0f && _velocity.x < -0.1f) || (_localScale.x < 0f && _velocity.x > -0.1f))
        {
            _localScale.x *= -1f;
            //= atribui valor
            //_localScale.x = _localScale.x * -1f;
            transform.localScale = _localScale;
            //atribui devolta a propriedade do componente
        }

    }

    protected virtual void Animations()
    {
        //Movement
        if (rb.velocity.x > 0.1f || rb.velocity.x < -0.1f)
        {
            animator.SetFloat("Speed_X", 1f);
        }
        else
            animator.SetFloat("Speed_X", 0f);
        //Jump
        if (rb.velocity.y > 0.1f)
            animator.SetFloat("Speed_Y", 1f);
        else if (rb.velocity.y < -0.1f)
        {
            animator.SetFloat("Speed_Y", -1f);
        }
        else
        {
            animator.SetFloat("Speed_Y", 0f);
        }
        animator.SetBool("OnGround", onGround());

        //Life
        animator.SetBool("Dead", dead);
    }

    protected virtual void Attack()
    {
        attackTime = Time.time + 1f/AttackSpeed;

    }

}
