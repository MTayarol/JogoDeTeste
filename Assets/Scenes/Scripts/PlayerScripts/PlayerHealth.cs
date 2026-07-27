using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int currentHealth = 3;
    public int maxHealth = 3;
    
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth <=0)
        {
            gameObject.SetActive(false);
        }
    }
}
