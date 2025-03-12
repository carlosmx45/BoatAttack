using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesFakeMovement : MonoBehaviour
{
    public float rotationSpeed;
    public float maxRotationAngle;

    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private Vector3 rotationAxis = new Vector3(1, 0, 1);
    private float timeToChange;
    private bool isReturning = false;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = originalRotation;
    }

    void Update()
    {
        if (transform.rotation == targetRotation)
        {
            if (isReturning)
            {
                float angle = Random.Range(-maxRotationAngle, maxRotationAngle);
                targetRotation = originalRotation * Quaternion.Euler(rotationAxis * angle);

                timeToChange = Time.time + Random.Range(1f, 3f);

                isReturning = false;
            }
            else
            {
                targetRotation = originalRotation;
                isReturning = true;
            }
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
