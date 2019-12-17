using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// orbit generator is an object that has a meth that creates an elliptical path for an object to follow
//it uses a period like a pendulum formula
//the method takes in the z and x positions of an object, and the period variable and calculates the next position
//I found this method being used for 2D orbits here: https://www.youtube.com/watch?v=mQKGRoV_jBc
//Its a 3 part tutorial series that goes over the theory behind the orbiting motion
//I altered it slightly to create 'angled' orbits, by feeding in an 'angle' value to alter the gameobjects height based on their Z position
[System.Serializable]
public class orbitGenerator
{
    public float xAxis;
    public float yAxis;

    public orbitGenerator(float xAxis, float yAxis)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    public Vector3 calculation(float Time, float orbitAngle, float xIn, float zIn)
    {
        float angle = Mathf.Deg2Rad * 360f * Time;
        float x = Mathf.Sin(angle) * xIn;
        float z = Mathf.Cos(angle) * zIn;
        float y = Mathf.Tan(orbitAngle * (Mathf.PI / 180)) * z;
        return new Vector3(x,y,z);
    }
}
