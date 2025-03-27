using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float killTimeDelay = 15f;
    [SerializeField] private float moveSpeed = 3f;

    void Update()
    {
        MoveObject();
        RotateOnZAxis();
        DestroyObject(killTimeDelay);
    }

    private void DestroyObject(float timeDelay)
    {
        Destroy(gameObject, timeDelay);
    }

    private void RotateOnZAxis()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 1);
    }

    private void MoveObject()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
