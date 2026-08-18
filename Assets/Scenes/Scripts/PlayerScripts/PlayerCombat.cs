using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    private Animator anim;
    private int comboStep = 0;
    private float lastClickTime = 0f;
    private float maxComboDelay = 0.8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Clique detectado");
            ProcessAttackCombo();
        }

        if (comboStep > 0 && Time.time - lastClickTime > maxComboDelay)
        {
            ResetCombo();
        }
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
    }

    public void ResetCombo()
    {
        comboStep = 0;
    }

}
