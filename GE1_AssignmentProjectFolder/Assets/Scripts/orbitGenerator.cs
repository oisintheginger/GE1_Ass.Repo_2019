using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
