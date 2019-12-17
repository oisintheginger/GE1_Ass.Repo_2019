using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires the gameobject to have an audiosource
//Being a public class means that any of the other scripts can access the audio data
[RequireComponent (typeof (AudioSource))]
public class AudioVisualistationScript : MonoBehaviour
{
    AudioSource _AS;
    public static float[] samplesArray = new float[256];
    public static float[] freqBand = new float[8];
    void Start()
    {
        _AS = GetComponent<AudioSource>();
    }
    void Update()
    {
        GetAudioSpectrum();
        GetFrequencyBands();
    }

    //Getting the audio spectrum data and feeding it into the samplesArray. These values can then be accessed by VisualizerGenerator.cs and planetScript.cs
    //to alter them in response to the audio playing
    void GetAudioSpectrum()
    {
        _AS.GetSpectrumData(samplesArray, 0, FFTWindow.Blackman);
    }


    //Frequency Bands Found on the Game Engines 1 Github, didn't see the point in reinventing the wheel
    //I just grabbed the relevant code for getting frequency bands and changed the variable names to work with my assignment
    void GetFrequencyBands()
    {
        for (int i = 0; i < freqBand.Length; i++)
        {
            int start = (int)Mathf.Pow(2, i) - 1;
            int width = (int)Mathf.Pow(2, i);
            int end = start + width;
            float average = 0;
            for (int j = start; j < end; j++)
            {
                average += samplesArray[j] * (j + 1);
            }
            average /= (float)width;
            freqBand[i] = average;
        }

    }

}
