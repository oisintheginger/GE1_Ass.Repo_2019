using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMotion : MonoBehaviour
{
    float speed;
    
    public float period, rate, height;
    Vector3 pos;
    // Start is called before the first frame update
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