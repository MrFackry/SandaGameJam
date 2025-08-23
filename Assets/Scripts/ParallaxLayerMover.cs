using UnityEngine;
using UnityEngine.Tilemaps;

// Versión Final: Soluciona el error "out of view frustum" y el salto visual.
// Debe estar en el objeto contenedor (ej: "CapaFondo_5").
public class ParallaxLayerMover : MonoBehaviour
{
    [Tooltip("La velocidad de movimiento. ¡DEBE ser mayor que 0 en el Inspector!")]
    public float moveSpeed;

    private float tileWidth;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        Tilemap tilemap = GetComponentInChildren<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("¡ERROR en '" + name + "'! No se encontró un Tilemap en sus hijos. El script se desactivará.");
            enabled = false;
            return;
        }

        tilemap.CompressBounds();
        tileWidth = tilemap.localBounds.size.x * tilemap.transform.localScale.x;

        if (tileWidth <= 0)
        {
             Debug.LogError("¡ERROR en '" + name + "'! El ancho del Tilemap es cero. El script se desactivará.");
             enabled = false;
        }
    }

    void Update()
    {
        // Esta línea es la solución. Mantiene el objeto dentro de un rango definido.
        // Nunca se irá a coordenadas lejanas, por lo tanto, el error desaparecerá.
        float newXPosition = Mathf.Repeat(Time.time * -moveSpeed, tileWidth);
        
        // Aplicamos la nueva posición.
        transform.position = startPosition + Vector3.right * newXPosition;
    }
}