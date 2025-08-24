using UnityEngine;
using System.Collections.Generic;

public class ParallaxSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxGroup
    {
        public GameObject parent;
        [HideInInspector] public Vector3 initialPosition;
        [HideInInspector] public ParallaxLayerMover2 mover;
    }

    // Lista de objetos con tilemaps "Grupos"
    public ParallaxGroup[] groups;   
    private ParallaxGroup currentGroup;

    void Start()
    {
        // Guardar posiciones iniciales y desactivar todo desde el comienzo
        foreach (var g in groups)
        {
            if (g.parent != null)
            {
                g.initialPosition = g.parent.transform.position;
                g.mover = g.parent.GetComponent<ParallaxLayerMover2>();
                g.parent.SetActive(false);
            }
        }

        // activar el primero
        ActivateRandomGroup();
    }

    void Update()
    {
        if (currentGroup != null && !currentGroup.parent.activeSelf)
        {
            // El grupo terminó su recorrido, luego regresarlo y activar otro
            currentGroup.parent.transform.position = currentGroup.initialPosition;
            ActivateRandomGroup();
        }
    }

    void ActivateRandomGroup()
    {
        if (groups.Length == 0) return;

        // Selecciona uno al azar que no sea el mismo que estaba activo
        List<ParallaxGroup> available = new List<ParallaxGroup>(groups);
        if (currentGroup != null) available.Remove(currentGroup);

        if (available.Count == 0) available.Add(currentGroup);

        currentGroup = available[Random.Range(0, available.Count)];

        // Activar el grupo
        currentGroup.parent.SetActive(true);
    }
}
