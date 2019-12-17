using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMotion : MonoBehaviour
{
    //Using the information I learned about orbiting motion in orbitGenerator.cs
    //I was able to figure out how to create a sine wave motion for my ship 'tail'
    //Each segment of the tail has this script attached, however when they are generated on awake, their individual period is altered 
    //the starting period of each segment is 0.25( i.e. a quarter of its period) * (index of segment/ amount of segments)
    //this means that each segment begins at a different point but with the same speed
    //this creates the wave like motion
    float speed;
    public float period, rate, height;
    Vector3 pos;
    void Start()
    {
        pos = this.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(period) >= 1f)
        {
            period *= 0;
        }
        speed = 1f / rate;
        period += Time.deltaTime * speed;
        pos.y = waveMotion(period);
        
        this.transform.localPosition = pos;
    }

    float waveMotion(float period)
    {
        float angle = Mathf.Deg2Rad * 360f * period;
        float yPos = Mathf.Sin(angle)*height;
        return yPos;
    }
}