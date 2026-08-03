using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int currentHealth = 3;
    public int maxHealth = 3;

    public TMP_Text healthText;

    private void Start()
    {
        healthText.text = "Hp: " + currentHealth + " / " + maxHealth;
    }
    
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthText.text = "Hp: " + currentHealth + " / " + maxHealth;
        if(currentHealth <=0)
        {
            gameObject.SetActive(false);
        }
    }
}
