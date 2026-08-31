using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TMP_Text healthText;

    private void Start()
    {
        healthText.text = "Hp: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
    }
    
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;
        healthText.text = "Hp: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
        if(StatsManager.Instance.currentHealth <=0)
        {
            gameObject.SetActive(false);
        }
    }
}
