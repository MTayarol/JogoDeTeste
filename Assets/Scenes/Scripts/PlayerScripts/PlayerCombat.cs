using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    private Animator anim;
    private float lastClickTime = 0f;
    public float cooldown = 2;
    private float timer;
    [SerializeField] private float maxComboDelay = 0.8f;
    //Variáveis de ataque:
    //Funções Unity:
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public StatsUI statsUi;

    //Bool:
    //Int:
    public int comboStep = 0;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    
    public void Update()
    {
        if (comboStep > 0 && Time.time - lastClickTime > maxComboDelay)
        {
            ResetCombo();
        }
        if(timer > 0)
            timer -= Time.deltaTime;

    }

    

    public void Attack()
    {     
        if (comboStep == 0 && timer > 0)
        return;
    
        ProcessAttackCombo();   
        
    }

    void ProcessAttackCombo()
    {
        //if (timer <= 0)
        //{
            if (comboStep == 0)
            {
                comboStep = 1;
                lastClickTime = Time.time;
                anim.SetTrigger("Ataque1");
                anim.SetBool("isAttacking", true);
            }
            
            else if(comboStep == 1 && Time.time - lastClickTime <= maxComboDelay)
            {
                comboStep = 2;
                lastClickTime = Time.time;
                anim.SetTrigger("Ataque2");
                anim.SetBool("isAttacking", true);
            }
            else
            {
                ResetCombo();
            }
        //}
        
    }

    public void DealDamage()
    {
        StatsManager.Instance.damage += 1;
        statsUi.UpdateDamage();
        
        Collider2D [] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if(enemy.isTrigger)
                continue;
            
            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-StatsManager.Instance.damage);
                enemies[0].GetComponent<EnemyKnockback>().Knockback(transform, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
            }
        }
        
    }

    public void ResetCombo()
    {
        comboStep = 0;
        anim.SetBool("isAttacking", false);
        timer = cooldown;
    }

    public void FinishingAttack()
    {
        if(comboStep == 1 && Time.time - lastClickTime >= maxComboDelay)
        {
            anim.SetBool("isAttacking", false);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }

}
