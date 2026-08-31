using UnityEngine;
using System.Collections;

public class EnemyKnockback : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private EnemyMovement enemy_Movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<EnemyMovement>();
    }

    public void Knockback(Transform playerTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemy_Movement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTimer(knockbackTime, stunTime));
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.linearVelocity = direction * knockbackForce;

    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
    
        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_Movement.ChangeState(EnemyState.Idle);
    }
    
}
