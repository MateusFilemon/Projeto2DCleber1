using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 velocity;
    private float damage;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //fisica é fixedupdate
    private void FixedUpdate()
    {
        //rb.velocity = velocity;
        rb.velocity = transform.right * 8f;
    }

    
    public void Initialize(Vector2 _direction, float _damage)
    {
        velocity = _direction * moveSpeed;
        damage = _damage;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {

        }
    }

    /* protected virtual void Flip()
    {
        //localScale escala xyz do objeto
        Vector3 _localScale = transform.localScale;
        Vector2 _velocity = rb.velocity;

        if ((_localScale.x > 0f && _velocity.x < -0.1f) || (_localScale.x < 0f && _velocity.x > -0.1f))
        {
            _localScale.x *= -1f;
            //= atribui valor
            //_localScale.x = _localScale.x * -1f;
            transform.localScale = _localScale;
            //atribui devolta a propriedade do componente
        }

    } 
    Caso tenha sprite, por esse metodo no update
    */ 

}
