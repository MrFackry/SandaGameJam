using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveLayer : MonoBehaviour
{
    [SerializeField] private Vector2 speedMove;
    private Vector2 offset;
    private Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        
    }

    // Update is called once per frame
    private void Update()
    {
        offset = speedMove * Time.time;
        material.mainTextureOffset = offset;
    }
}
