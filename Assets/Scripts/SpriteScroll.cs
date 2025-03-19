using UnityEngine;

public class SpriteScroll : MonoBehaviour
{
    [SerializeField] private Vector2 moveSpeed;

    private Vector2 offset;
    private Material material;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        offset += moveSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}
