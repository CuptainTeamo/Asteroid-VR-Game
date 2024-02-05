using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodMovement : MonoBehaviour
{
    [Header("Control the speed of the Asteroid")]
    public float maxSpeed;
    public float minSpeed;

    [Header("Control the rotational speed")]
    public float rotationalSpeedMin;
    public float rotationalSpeedMax;

    private float rotationSpeed;
    private float xAngle, yAngle, zAngle;

    public Vector3 movementDirection;

    private float asteroidSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // get random speed
        asteroidSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);

        // random rotation
        xAngle = UnityEngine.Random.Range(0, 360);
        yAngle = UnityEngine.Random.Range(0, 360);
        zAngle = UnityEngine.Random.Range(0, 360);

        transform.Rotate(xAngle, yAngle, zAngle);

        rotationSpeed = UnityEngine.Random.Range(rotationalSpeedMin, rotationalSpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementDirection * Time.deltaTime * asteroidSpeed, Space.World);
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
