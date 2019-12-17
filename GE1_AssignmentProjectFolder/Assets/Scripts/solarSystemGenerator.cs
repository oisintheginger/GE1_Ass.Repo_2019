using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script is used to generate multiple 'orbits'
/// each new orbit has a planetScript.cs attached
/// At runtime, a prefab orbit is used to generate a specific amount of new orbits
/// Each orbit has a center, and a 'planet object' as a child object
/// The orbit values for the angle of orbit, the speed, and the detection, the looking speed, and other variables are set when they are created
/// </summary>
public class solarSystemGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject OrbiterPrefab, player;

    [SerializeField]
    int planetsToSpawn;
    [SerializeField]
    float MinDistance,MaxDistance, maxAngle,detectRadius, lookSpeed;
    
    void Start()
    {
        for(int i = 0; i<planetsToSpawn; i++)
        {
            GameObject newOrbit = OrbiterPrefab;
            newOrbit.transform.position = this.transform.position;
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
