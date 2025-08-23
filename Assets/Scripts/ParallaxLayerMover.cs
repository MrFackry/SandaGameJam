using UnityEngine;
using UnityEngine.Tilemaps;

public class ParallaxLayerMover : MonoBehaviour
{
    
    public float moveSpeed;

    private float tileWidth;
    private Vector3 startPosition;

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
    }

    void Update()
    {
        
        float newXPosition = Mathf.Repeat(Time.time * -moveSpeed, tileWidth);
        
        // Aplicamos la nueva posición.
        transform.position = startPosition + Vector3.right * newXPosition;
    }
}