using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerCombat combat;

    public bool isKnockedBack;


    //Pega o movimento do personagem para mandar para a classe de anime.
    public Vector2 _moveInput
    {
        get { return this.moveInput; }
        set { this.moveInput = value;}
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        combat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isKnockedBack == false)
        {
            rb.linearVelocity = moveInput * movementSpeed;
        }

        if (combat != null && combat.comboStep > 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
    
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.linearVelocity = direction * force; 
        StartCoroutine(KnockbackCounter(stunTime));
    }

    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
