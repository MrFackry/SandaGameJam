using System.Collections.Generic;
using UnityEngine;

public class DynamicParallaxGenerator : MonoBehaviour
{
    [Header("Configuración de Segmentos")]
    [Tooltip("Arrastra aquí todos los Prefabs de las variaciones del fondo.")]
    public List<GameObject> segmentPrefabs;

    [Tooltip("La velocidad a la que se moverá esta capa del fondo.")]
    public float moveSpeed;

    [Header("¡IMPORTANTE! Configuración Manual")]
    [Tooltip("Define aquí el ancho EXACTO de tus segmentos. Usa la prueba manual para encontrar este valor.")]
    public float manualSegmentWidth; // <-- NUEVO CAMPO

    private List<GameObject> activeSegments = new List<GameObject>();
    private float screenLeftEdgeX;

    void Start()
    {
        // --- Validaciones de Seguridad ---
        if (segmentPrefabs == null || segmentPrefabs.Count == 0)
        {
            Debug.LogError("La lista 'Segment Prefabs' está vacía.");
            enabled = false; return;
        }
        if (manualSegmentWidth <= 0)
        {
            Debug.LogError("El 'Manual Segment Width' debe ser un valor mayor que 0. Mide tu prefab para encontrar el valor correcto.");
            enabled = false; return;
        }
        if (Camera.main == null)
        {
             Debug.LogError("No se encontró una cámara con el Tag 'MainCamera'.");
             enabled = false; return;
        }
        
        screenLeftEdgeX = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        SpawnInitialSegments();
    }

    void Update()
    {
        if (activeSegments.Count == 0) return;

        foreach (GameObject segment in activeSegments)
        {
            segment.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        GameObject leftmostSegment = activeSegments[0];
        // Comprobamos usando el ancho manual
        if ((leftmostSegment.transform.position.x + manualSegmentWidth / 2) < screenLeftEdgeX)
        {
            activeSegments.RemoveAt(0);
            Destroy(leftmostSegment);

            GameObject lastSegment = activeSegments[activeSegments.Count - 1];
            SpawnNewSegment(lastSegment);
        }
    }

    void SpawnInitialSegments()
    {
        GameObject firstSegment = SpawnNewSegment(null);
        SpawnNewSegment(firstSegment);
    }

    GameObject SpawnNewSegment(GameObject previousSegment)
    {
        int randomIndex = Random.Range(0, segmentPrefabs.Count);
        GameObject randomPrefab = segmentPrefabs[randomIndex];
        GameObject newSegment = Instantiate(randomPrefab);
        newSegment.transform.SetParent(this.transform);

        if (previousSegment == null)
        {
            newSegment.transform.position = this.transform.position;
        }
        else
        {
            // Usamos el ancho manual para un posicionamiento perfecto
            float previousSegmentRightEdge = previousSegment.transform.position.x + manualSegmentWidth / 2;
            float newSpawnX = previousSegmentRightEdge + manualSegmentWidth / 2;
            newSegment.transform.position = new Vector3(newSpawnX, this.transform.position.y, this.transform.position.z);
        }

        activeSegments.Add(newSegment);
        return newSegment;
    }
}