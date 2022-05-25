using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;

    public Queue<Cube> cubesQueue = new Queue<Cube>();
    [SerializeField] private int cubesQueueOpacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int MaxCubeNumber;

    private int maxPower = 12;

    private Vector3 defaultSpawnPosition;

    private void Awake()
    {
        Instance = this;

        defaultSpawnPosition = transform.position;
        MaxCubeNumber = (int)Mathf.Pow(2, maxPower);

        InitializeCubeQueue();
    }

    private void InitializeCubeQueue()
    {
        for (int i = 0; i < cubesQueueOpacity; i++)
            AddCubeToQueue();
    }

    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform)
            .GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        cubesQueue.Enqueue(cube);
    }

    public Cube Spawn(int cubeNumber, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueOpacity++;
                AddCubeToQueue();
            }
            else
            {
                return null;
            }
        }

        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(cubeNumber);
        cube.SetColor(GetColor(cubeNumber));
        cube.gameObject.SetActive(true);

        return cube;
    }

    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }

    public void DestroyCube(Cube cube)
    {
        cube.CubeRigidbody.velocity = Vector3.zero;
        cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }

    private int GenerateRandomNumber()
    {
        return (int)MathF.Pow(2, UnityEngine.Random.Range(1, 6));
    }

    private Color GetColor(int number)
    {
        return cubeColors[(int)(MathF.Log(number) / MathF.Log(2) - 1)];
    }
}
