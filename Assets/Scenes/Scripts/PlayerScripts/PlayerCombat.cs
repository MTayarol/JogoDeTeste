using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    private Animator anim;
    public int comboStep = 0;
    public bool isAttacking = false;
    private float lastClickTime = 0f;
    [SerializeField] private float maxComboDelay = 0.8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    }

    public void Attack()
    {
        ProcessAttackCombo();
    }

    void ProcessAttackCombo()
    {
        if (comboStep == 0)
        {
            comboStep = 1;
            lastClickTime = Time.time;
            anim.SetTrigger("Ataque1");
        }
        else if(comboStep == 1 && Time.time - lastClickTime <= maxComboDelay)
        {
            comboStep = 2;
            lastClickTime = Time.time;
            anim.SetTrigger("Ataque2");
        }
        else
        {
            ResetCombo();
        }
    }

    public void ResetCombo()
    {
        comboStep = 0;
        isAttacking = false;
    }

    public void FinishingAttack()
    {
        anim.SetBool("isAttacking", false);
    }

}
