using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowthMultiplier = 1.2f; //Adiciona 20% a cada novo level
    public Slider expSlider;
    public TMP_Text currentLevelText;  


    public void Start()
    {
        UpdateUI();        
    }

    public void Update()
    {
        
    }

    public void OnEnable()
    {
        EnemyHealth.OnMonsterDefeated += GainExperience;
    }

    public void OnDisable()
    {
        EnemyHealth.OnMonsterDefeated -= GainExperience;
    }



    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevel)
        {
            level++;
        }
        UpdateUI();
    }


    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = ("Level: " + level);
    }

}
