using UnityEngine;
using UnityEngine.Tilemaps;

public class ParallaxLayerMover2 : MonoBehaviour
{
    [Tooltip("Velocidad hacia la izquierda (m/s)")]
    public float moveSpeed = 7f;
    
    Transform player;  
    // Se usa para calcular el borde derecho real del conjunto de hijos.
    private float maxLocalRight = 0f;

    void Awake()
    {
        //Buscamos el player
        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void OnEnable()
    {
        // Recalcular cada vez que el padre se active (útil cuando el spawner lo reactiva)
        RecalculateRightmostLocalX();
    }

    void Start()
    {
        // Cálculo inicial (por si ya está activo desde el inicio)
        RecalculateRightmostLocalX();
    }

    void Update()
    {
        // Mover el objeto padre (y por ende todos sus hijos)
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (player == null) return;

        // Convertir el punto derecho local almacenado a coordenada mundo
        Vector3 worldRightPoint = transform.TransformPoint(new Vector3(maxLocalRight, 0f, 0f));
        float rightEdgeX = worldRightPoint.x;

        // Si el borde derecho del conjunto está a la izquierda del player -> desactivar padre
        if (rightEdgeX < player.position.x)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Recalcula el punto más a la derecha entre todos los renderers hijos  
    /// </summary>
    public void RecalculateRightmostLocalX()
    {
        // Buscar TilemapRenderer entre hijos (incluyendo inactivos)
        var tilemapRenderers = GetComponentsInChildren<TilemapRenderer>(true);

        // Fallback a SpriteRenderer si no hay TilemapRenderer
        if (tilemapRenderers.Length == 0)
        {
            var spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
            if (spriteRenderers.Length == 0)
            {
                Debug.LogWarning($"[{name}] No se encontró TilemapRenderer ni SpriteRenderer entre los hijos.");
                maxLocalRight = 0f;
                return;
            }

            float maxLocal = float.NegativeInfinity;
            foreach (var r in spriteRenderers)
            {
                Vector3 worldRight = r.bounds.max;
                float localX = transform.InverseTransformPoint(worldRight).x;
                if (localX > maxLocal) maxLocal = localX;
            }
            maxLocalRight = (maxLocal == float.NegativeInfinity) ? 0f : maxLocal;
            return;
        }

        float maxLocalRightCandidate = float.NegativeInfinity;
        foreach (var r in tilemapRenderers)
        {
            Vector3 worldRight = r.bounds.max;                       // borde derecho en world space
            float localX = transform.InverseTransformPoint(worldRight).x; // convertir a local del padre
            if (localX > maxLocalRightCandidate) maxLocalRightCandidate = localX;
        }

        maxLocalRight = (maxLocalRightCandidate == float.NegativeInfinity) ? 0f : maxLocalRightCandidate;
#if UNITY_EDITOR
        Debug.Log($"[{name}] maxLocalRight calculado = {maxLocalRight:F3}");
#endif
    }
}
