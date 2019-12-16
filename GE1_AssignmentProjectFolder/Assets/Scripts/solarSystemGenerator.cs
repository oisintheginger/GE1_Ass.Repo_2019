using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solarSystemGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject OrbiterPrefab, player;

    [SerializeField]
    int planetsToSpawn;
    [SerializeField]
    float MinDistance,MaxDistance, maxAngle,detectRadius, lookSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<planetsToSpawn; i++)
        {
            GameObject newOrbit = OrbiterPrefab;
            newOrbit.transform.rotation *= Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
            planetScript pS = newOrbit.GetComponent<planetScript>();
            pS.player = player;
            pS.detectionRadius = detectRadius;
            pS.lookSpeed = lookSpeed;
            pS.xIn = Random.Range(MinDistance, MaxDistance);
            pS.zIn = Random.Range(MinDistance, MaxDistance);
            pS.orbitAngle = Random.Range(0f, maxAngle);
            pS.orbitTime = Random.Range(5f, 10f);
            Instantiate(newOrbit);

        }
    }
}
