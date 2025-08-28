using UnityEngine;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    [SerializeField] public Slider healthBar;
    public bool isDead = false;
    public float maxHealth;
    public GameObject LoseScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (healthBar != null)
        {
            maxHealth = healthBar.maxValue;
            healthBar.value = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void TakeDamage(float damage)
    {
        if (healthBar != null)
        {
            healthBar.value -= damage;
            if (healthBar.value <= 0)
            {
                isDead = true;
                LoseScreen.SetActive(true);
                Time.timeScale = 0f;
                Cursor.visible = true;               
            }
        }
    }
}
