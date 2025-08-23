using UnityEngine;
using UnityEngine.Tilemaps;

public class ParallaxLayerMover : MonoBehaviour
{
    public float moveSpeed;

    private float tileWidth;
    private Vector3 startPosition;
    private float offsetX; // acumulador del movimiento

    void Start()
    {
        startPosition = transform.position;

        Tilemap tilemap = GetComponentInChildren<Tilemap>();
        if (tilemap == null)
        {
            enabled = false;
            return;
        }

        tilemap.CompressBounds();
        tileWidth = tilemap.localBounds.size.x * tilemap.transform.localScale.x;

        Debug.Log("El ancho calculado para '" + name + "' es: " + tileWidth);

        if (tileWidth <= 0)
        {
            enabled = false;
        }

        offsetX = 0f; // arranca limpio
    }

    void Update()
    {
        // Acumular desplazamiento según velocidad y tiempo
        offsetX += -moveSpeed * Time.deltaTime;

        // Aplicar ciclo con Mathf.Repeat
        float newXPosition = Mathf.Repeat(offsetX, tileWidth);

        // Mover capa
        transform.position = startPosition + Vector3.right * newXPosition;
    }
}
