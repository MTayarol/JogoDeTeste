using UnityEngine;

//Parei nos 6:10 do vídeo de ataque. AINDA TENHO UM BUG QUE ELE ATACA SÓ UMA VEZ E É
public class EnemyMovement : MonoBehaviour
{
    //Variáveis float

    public float speed;
    public float attackRange = 2; 
    public float attackCooldown = 2;

    private float attackCooldownTimer;
    public float playerDetectRange = 5;
    //Variáveis inteiras

    private int facingDirection = 1;

    //EnemyStates.
    private EnemyState enemyState;

    //RigidBodys
    private Rigidbody2D rb;

    //Transforms.
    private Transform player;
    public Transform detectPoint;

    //Animators.
    private Animator anim;

    //LayerMask
    public LayerMask playerLayer;

    void Start()
    {
        //Inicia os componentes padrão do Unity
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }


        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if(enemyState == EnemyState.Attacking)
        {
            //Ataque
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Attacking);
        }
    }

    void Chase()
    {

        //Caso a posição do player seja maior que a posição do inimigo e ele esteja virando para o lado contrário, FLIP
        if(player.position.x > transform.position.x && facingDirection == -1 ||
            player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1; 
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectPoint.position, playerDetectRange, playerLayer); 

        if (hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }

            else if (Vector2.Distance(transform.position, player.position) > attackRange)   
            {
                ChangeState(EnemyState.Chasing);
            }
        }

        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {   
        //Sai da animação atual
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);
        
        //Atualiza o estado atual
        enemyState = newState;

        //Atualiza a nova animação
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
    }   

    public void OnDrawGizmosSelected()
    {
        if (detectPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectPoint.position, playerDetectRange);
    }

}



public enum EnemyState
{
    Idle,
    Chasing,
    Attacking

}

/*
if (rb.linearVelocity.x > 0.1f)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        else if (rb.linearVelocity.x < -0.1f)
        {
            transform.eulerAngles = new Vector2(0, 180);    
        }
        */