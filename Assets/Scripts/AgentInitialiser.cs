using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInitialiser : MonoBehaviour
{
    [SerializeField] private GameObject DeerMalePrefab;
    [SerializeField] private GameObject DeerFemalePrefab;
    [SerializeField] private GameObject WolfMalePrefab;
    [SerializeField] private GameObject WolfFemalePrefab;
    [SerializeField] private int InitialDeerSpawnAmount;
    [SerializeField] private int InitialWolfSpawnAmount;
    [SerializeField] private int DeerCounter = 0;
    [SerializeField] private int WolfCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitialSpawner();
    }

    // Spawns one deer in a random location
    void SpawnRandomDeer(float yOffset)
    {
        float x = Random.Range(0, 300);
        float z = Random.Range(0, -300);

        // perform a raycast at the desired location (at an offset above the heighest ground) directly downwards
        // to ensure we are only placing on suitable ground.
        Ray ray = new Ray(new Vector3(x, yOffset, z), Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.transform == null)
            return;

        if (hit.transform.tag == "Ground")
        {
            int random = Random.Range(1, 3);
            GameObject temp;
            if (random == 1)
                temp = Instantiate(DeerMalePrefab, hit.point, Quaternion.identity);
            else
                temp = Instantiate(DeerFemalePrefab, hit.point, Quaternion.identity);
            temp.transform.parent = transform;
            DeerCounter++;

        }
    }

    // Spawns one wolf in a random location
    void SpawnRandomWolf(float yOffset)
    {
        float x = Random.Range(0, 300);
        float z = Random.Range(0, -300);

        // perform a raycast at the desired location (at an offset above the heighest ground) directly downwards
        // to ensure we are only placing on suitable ground.
        Ray ray = new Ray(new Vector3(x, yOffset, z), Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.transform == null)
            return;

        if (hit.transform.tag == "Ground")
        {
            int random = Random.Range(1, 3);
            GameObject temp;
            if (random == 1)
                temp = Instantiate(WolfMalePrefab, hit.point, Quaternion.identity);
            else
                temp = Instantiate(WolfFemalePrefab, hit.point, Quaternion.identity);
            temp.transform.parent = transform;
            WolfCounter++;         
        }
    }

    // Creates an initial population
    void InitialSpawner()
    {
        Vector3 currentPos = new Vector3(0, 0, 0);
        float yOffset = 30;

        while (DeerCounter < InitialDeerSpawnAmount)
        {
            SpawnRandomDeer(yOffset);
        }
        while (WolfCounter < InitialWolfSpawnAmount)
        {
            SpawnRandomWolf(yOffset);
        }
    }
}
