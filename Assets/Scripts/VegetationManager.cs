using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationManager : MonoBehaviour
{

    [SerializeField] private GameObject GrassPrefab;
    [SerializeField] private int InitialSpawnAmount;
    [SerializeField] private int grassCounter = 0;
    [SerializeField] private float SpawnTimer;
    [SerializeField] private bool Spawning;

    private bool StoppedSpawning;

    // Start is called before the first frame update
    void Start()
    {
        InitialSpawner();
        StartCoroutine(SpawnVegetation());
    }

    // Update is called once per frame
    void Update()
    {
        if (StoppedSpawning && Spawning)
            StartCoroutine(SpawnVegetation());
    }

    // Spawns one piece of grass in a random location
    void SpawnRandomGrass(float yOffset)
    {
        float x = Random.Range(0, 300);
        float z = Random.Range(0, -300);

        // perform a raycast at the desired location (at an offset above the heighest ground) directly downwards
        // to ensure we are only placing grass on suitable ground.
        Ray ray = new Ray(new Vector3(x, yOffset, z), Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.transform == null)
            return;

        if (hit.transform.tag == "Ground")
        {
            GameObject temp = Instantiate(GrassPrefab, hit.point, Quaternion.identity);
            temp.transform.parent = transform;
            grassCounter++;
        }
    }

    // Creates an initial population of grass
    void InitialSpawner()
    {
        Vector3 currentPos = new Vector3(0, 0, 0);
        float yOffset = 30;

        while (grassCounter < InitialSpawnAmount)
        {
            SpawnRandomGrass(yOffset);
        }
    }

    // Spawns grass over time
    IEnumerator SpawnVegetation()
    {
        Vector3 currentPos = new Vector3(0, 0, 0);
        float yOffset = 30;

        StoppedSpawning = false;

        while (Spawning)
        {
            SpawnRandomGrass(yOffset);
            yield return new WaitForSeconds(SpawnTimer);
        }
        StoppedSpawning = true;
    }
}
