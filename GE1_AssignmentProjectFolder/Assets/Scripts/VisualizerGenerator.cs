using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerGenerator : MonoBehaviour
{

    //Creatinga circular visualizer based on the sample array information from AudioVisualisationScript.cs
    // There is a samplecube for each of the 256 samples
    // This sample cube is scaled in order to visualize the audio
    // When being generated, each of the sample cubes is give a transform parent. The parent is position at the bottom of the sample cube.
    // The parent object is directly scaled, and due to the positioning of the sample cube relative to it, the sample cube only scales in a singular direction
    // in reaction to the audio sample information
    
    [SerializeField] GameObject sectionPrefab, sunObject;
    GameObject[] visualiserSections = new GameObject[256];

    [SerializeField]    float 
                        maxHeight, 
                        radius,
                        baseLength, 
                        maxLengthScale, 
                        lookSpeed, 
                        maxRadius,scaler, 
                        baseRadius, 
                        lerpSpeed, 
                        MAXSPHERESCALE,
                        TOOBIGLERPSPEED, 
                        visualizerLerp;

    [SerializeField] Transform sunPosition;
    [SerializeField] Transform TargetToFollow;

    void Start()
    {
        
        //creating the sample cubes and positioning them in a circular formation
        for(int i = 0; i<256; i++)
        {
            GameObject parentObject = new GameObject();
            parentObject.transform.position = this.transform.position;
            GameObject cubePiece = (GameObject)Instantiate(sectionPrefab);
            parentObject.transform.parent = this.transform;
            parentObject.name = "CubeParent" + i; 
            cubePiece.transform.position = parentObject.transform.position +    new Vector3(cubePiece.transform.localScale.x/2,
                                                                                            cubePiece.transform.localScale.y/2, 
                                                                                            -cubePiece.transform.localScale.z / 2);
            cubePiece.transform.parent = parentObject.transform;
            this.transform.eulerAngles = new Vector3(0f, i * 1.40625f, 0f);
            parentObject.transform.position = Vector3.forward * radius;
            visualiserSections[i] = parentObject;
            
        }
        //moving to the sun position once the circle is generated
        this.transform.position = sunPosition.position;
        this.transform.rotation = sunPosition.rotation;
        this.transform.parent = sunPosition.transform;
    }

    // Update is called once per frame
    void Update()
    {
        visualizerFunction();
    }


    // Most of the 256 samples go unused during a normal song, so in order to spice up the visualization and to increase the reactive elements
    // only the first 64 sanples are used. This means that every 1/4 of the circular visualizer is actually a copy of the first 64 samples
    void visualizerFunction()
    {
        for (int i = 0; i < 64; i++)
        {
            if (sectionPrefab != null)
            {
                visualiserSections[i].transform.localScale       = Vector3.Lerp(visualiserSections[i].transform.localScale,
                                                                            new Vector3(1, 1, (AudioVisualistationScript.samplesArray[i] * maxHeight) + baseLength),
                                                                            Time.deltaTime * visualizerLerp);
                visualiserSections[255 - i].transform.localScale = Vector3.Lerp(visualiserSections[i].transform.localScale,
                                                                            new Vector3(1, 1, (AudioVisualistationScript.samplesArray[i] * maxHeight) + baseLength),
                                                                            Time.deltaTime * visualizerLerp);
                visualiserSections[128 - i].transform.localScale = Vector3.Lerp(visualiserSections[i].transform.localScale,
                                                                            new Vector3(1, 1, (AudioVisualistationScript.samplesArray[i] * maxHeight) + baseLength),
                                                                            Time.deltaTime * visualizerLerp);
                visualiserSections[128 + i].transform.localScale = Vector3.Lerp(visualiserSections[i].transform.localScale,
                                                                            new Vector3(1, 1, (AudioVisualistationScript.samplesArray[i] * maxHeight) + baseLength),
                                                                            Time.deltaTime*visualizerLerp);

                //making sure not to go through the central 'sun' object
                if (visualiserSections[i].transform.localScale.z > maxLengthScale)
                {
                    visualiserSections[255 - i].transform.localScale = new Vector3(1, 1, maxLengthScale + baseLength);
                    visualiserSections[i].transform.localScale = new Vector3(1, 1, maxLengthScale + baseLength);
                    visualiserSections[128 - i].transform.localScale = new Vector3(1, 1, maxLengthScale + baseLength);
                    visualiserSections[128 + i].transform.localScale = new Vector3(1, 1, maxLengthScale + baseLength);
                }


            }

        }

        // scaling  the sun object using the first freqBand in AudioVisualisationScript.cs
        sunObject.transform.localScale = Vector3.Lerp(sunObject.transform.localScale,
                                                                        new Vector3((AudioVisualistationScript.freqBand[0] * maxRadius * scaler) + baseRadius, (AudioVisualistationScript.freqBand[0] * maxRadius * scaler) + baseRadius, (AudioVisualistationScript.freqBand[0] * maxRadius * scaler) + baseRadius),
                                                                        lerpSpeed*Time.deltaTime);
        if(sunObject.transform.localScale.x> MAXSPHERESCALE)
        {
            sunObject.transform.localScale = Vector3.Lerp(sunObject.transform.localScale,
                                                                        new Vector3(MAXSPHERESCALE, MAXSPHERESCALE, MAXSPHERESCALE),
                                                                        TOOBIGLERPSPEED * Time.deltaTime);
        }
        //targeting the player so that the visualizer is always visible to some extent from any position in the scene
        if (TargetToFollow != null)
        {
            Vector3 tVector = sunPosition.transform.position - TargetToFollow.transform.position;
            sunPosition.transform.rotation = Quaternion.Lerp(sunPosition.transform.rotation, Quaternion.LookRotation(tVector) * Quaternion.Euler(0, 90, 90), lookSpeed*Time.deltaTime);
        }
    }
}
