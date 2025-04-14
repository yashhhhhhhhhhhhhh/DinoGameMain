using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    private Vector3 originalPosition;
    private float shakeTime = 0f;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (shakeTime > 0)
        {
            transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeTime = duration;
    }
}
            