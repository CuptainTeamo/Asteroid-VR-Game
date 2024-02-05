using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Size of the spawner area")]
    public Vector3 spawnerSize;

    [Header("Rate of spawn")]
    public float spawnRate = 1f;

    [Header("Model to spawn")]
    [SerializeField] GameObject asteriodModel;

    private float spawnTimer = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnerSize);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnRate)
        {
            SpawnAsteroid();
            spawnTimer = 0f;
        }
    }

    private void SpawnAsteroid()
    {
        // position
        Vector3 spawnPoint = transform.position + new Vector3(UnityEngine.Random.Range(-spawnerSize.x / 2, spawnerSize.x / 2), UnityEngine.Random.Range(-spawnerSize.y / 2, spawnerSize.y / 2), UnityEngine.Random.Range(-spawnerSize.z / 2, spawnerSize.z / 2));

        // instiate the asteroid
        GameObject asteriod = Instantiate(asteriodModel, spawnPoint, transform.rotation);

        asteriod.transform.SetParent(this.transform);
    }
}
