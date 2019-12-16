using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetScript : MonoBehaviour
{
    public orbitGenerator orbitPath;
    public Transform planetObject;
    public GameObject player;

    [Range(0f, 1f)]
    public float orbitComplete = 0f;
    public float orbitTime = 3f;
    float regularComplete;
    public float orbitSpeed;
    public float orbitAngle, xIn, zIn;
    public float lookSpeed, detectionRadius;
    

    // Start is called before the first frame update
    void Start()
    {
        setPlanetPosition();
        regularComplete = orbitTime;
    }

    void setPlanetPosition()
    {
        Vector3 orbitPosition = orbitPath.calculation(orbitComplete, orbitAngle, xIn, zIn);
        planetObject.localPosition = orbitPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float playerDist = Vector3.Distance(planetObject.transform.position, player.transform.position);
        Vector3 tV = -planetObject.transform.position + player.transform.position;
        
        if (Mathf.Abs(orbitComplete) < 0.0000001f)
        {
            orbitComplete *= 0.001f;
        }
        if (playerDist > detectionRadius)
        {
            
            orbitSpeed = 1f / orbitTime;
            orbitComplete += Time.deltaTime * orbitSpeed;
            orbitComplete %= 1f;
        }
        else if(playerDist < detectionRadius)
        {
            planetObject.transform.rotation = Quaternion.Lerp(planetObject.transform.rotation, Quaternion.LookRotation(tV), lookSpeed * Time.deltaTime);
        }
        
        setPlanetPosition();
    }
}
