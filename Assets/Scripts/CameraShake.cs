using System;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 0.5f;

    private Vector3 originalPosition;
    void Start()
    {
        originalPosition = transform.position;
    }

    public void ShakeCamera()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elipsiedTime = 0;
        while (elipsiedTime < shakeDuration)
        {
            transform.position = originalPosition + (Vector3)UnityEngine.Random.insideUnitCircle * shakeMagnitude;
            elipsiedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = originalPosition;
    }
}
