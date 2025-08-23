using UnityEngine;
using TMPro;

public class ParallaxSpeedController : MonoBehaviour
{
    public TMP_Text scoreText;

    [Tooltip("¿Cada cuántos metros debe aumentar la velocidad?")]
    public float distanceThreshold = 10f;

    [Tooltip("¿Cuánto aumenta de velocidad cada vez?")]
    public float accelerationStep = 0.05f;
   
    private float nextIncrease;// meta para el siguiente aumento

    [HideInInspector]
    public float distance;

    // velocidades originales
    private ParallaxLayerMover[] parallaxLayers;
    private float[] baseSpeeds;

    // factor multiplicador global
    [HideInInspector]
    public float GlobalSpeedRate = 1f;          

    void Start()
    {
        nextIncrease = distanceThreshold; // se mantiene
        distance = 0f;                     // aseguramos arranque limpio

        // Buscar todos los ParallaxLayerMover en escena
        parallaxLayers = FindObjectsOfType<ParallaxLayerMover>();
        baseSpeeds = new float[parallaxLayers.Length];

        // Guardar la velocidad inicial de cada capa
        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            baseSpeeds[i] = parallaxLayers[i].moveSpeed;
        }
    }

    void Update()
    {
        // Aumentar distancia con una base fija (puedes enlazarlo al ScoreManager)
        distance += Time.deltaTime * GlobalSpeedRate;

        // Verificar si pasamos el umbral de distancia
        if (distance >= nextIncrease)
        {
            GlobalSpeedRate += accelerationStep;     // Aumentar factor global
            nextIncrease += distanceThreshold;    // Ajustar próximo objetivo
            Debug.Log($"Nueva aceleración global: x{GlobalSpeedRate:F2}");

            // Aplicar nueva velocidad proporcional a todos los fondos
            for (int i = 0; i < parallaxLayers.Length; i++)
            {
                if (parallaxLayers[i] != null)
                {
                    parallaxLayers[i].moveSpeed = baseSpeeds[i] * GlobalSpeedRate;
                }
            }
        }

        // Mostrar con 2 decimales
        scoreText.text = $"Distance: {distance:F2} m";
    }
}
