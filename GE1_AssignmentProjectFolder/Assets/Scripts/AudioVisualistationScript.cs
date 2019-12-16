using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioVisualistationScript : MonoBehaviour
{
    AudioSource _AS;
    public static float[] samplesArray = new float[256];
    public static float[] freqBand = new float[8];
    public static float[] samplesBuffer = new float[256];
    float[] bufferDecrease = new float[256];
    [SerializeField] float higherScale, lowerScale;
    // Start is called before the first frame update
    void Start()
    {
        _AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetAudioSpectrum();
    }

    void GetAudioSpectrum()
    {
        _AS.GetSpectrumData(samplesArray, 0, FFTWindow.Blackman);
    }
    
}
