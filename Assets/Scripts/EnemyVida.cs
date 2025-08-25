using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    
    [Header("Efectos Opcionales")]
    [SerializeField] private GameObject deathEffect; // Partículas o efectos al morir
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    
    private AudioSource audioSource;
    
    void Start()
    {
        // Inicializar la vida actual al máximo
        currentHealth = maxHealth;
        
        // Obtener componente de audio si existe
        audioSource = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// Aplica daño al enemigo
    /// </summary>
    /// <param name="damageAmount">Cantidad de daño a aplicar</param>
    public void TakeDamage(int damageAmount)
    {
        // Reducir la vida
        currentHealth -= damageAmount;
        
        // Reproducir sonido de daño
        PlaySound(damageSound);
        
        // Mostrar información en consola (opcional, para debug)
        Debug.Log($"{gameObject.name} recibió {damageAmount} de daño. Vida restante: {currentHealth}");
        
        // Verificar si el enemigo debe morir
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    /// <summary>
    /// Maneja la muerte del enemigo
    /// </summary>
    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto!");
        
        // Reproducir sonido de muerte
        PlaySound(deathSound);
        
        // Crear efecto de muerte si está asignado
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
        
        // Destruir el objeto enemigo
        Destroy(gameObject);
    }
    

    
    /// <summary>
    /// Reproduce un sonido si está disponible
    /// </summary>
    /// <param name="clip">Clip de audio a reproducir</param>
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    
    /// <summary>
    /// Obtiene la vida actual del enemigo
    /// </summary>
    /// <returns>Vida actual</returns>
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    
    /// <summary>
    /// Obtiene la vida máxima del enemigo
    /// </summary>
    /// <returns>Vida máxima</returns>
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    
    /// <summary>
    /// Obtiene el porcentaje de vida actual (0-1)
    /// </summary>
    /// <returns>Porcentaje de vida</returns>
    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
}