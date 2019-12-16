using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerGenerator : MonoBehaviour
{
    [SerializeField] GameObject sectionPrefab;
    GameObject[] visualiserSections = new GameObject[256];
    [SerializeField] float maxHeight, radius, baseLength, maxLengthScale, lookSpeed;
    [SerializeField] Transform sunPosition;
    [SerializeField] Transform TargetToFollow;
    // Start is called before the first frame update
    void Start()
    {
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
        this.transform.position = sunPosition.position;
        this.transform.rotation = sunPosition.rotation;// * Quaternion.Euler(new Vector3(-90,0,0));
        this.transform.parent = sunPosition.transform;
    }

    // Update is called once per frame
    void Update()
    {
        visualizerFunction();
    }

    void visualizerFunction()
    {
        for (int i = 0; i < 64; i++)
        {
            if (sectionPrefab != null)
            {
                visualiserSections[i].transform.localScale = Vector3.Lerp(visualiserSections[i].transform.localScale,
                                                                        new Vector3(1, 1, (AudioVisualistationScript.samplesArray[i] * maxHeight) + baseLength),
                                                                        0.1f);
                visualiserSections[255 - i].transform.localScale = Vector3.Lerp(visualiserSections[i].transform.localScale,
                                                                            new Vector3(1, 1, (AudioVisualistationScript.samplesArray[i] * maxHeight) + baseLength),
                                                                            0.1f);
                visualiserSections[128 - i].transform.localScale = Vector3.Lerp(visualiserSections[i].transform.localScale,
                                                                            new Vector3(1, 1, (AudioVisualistationScript.samplesArray[i] * maxHeight) + baseLength),
                                                                            0.1f);
                visualiserSections[128 + i].transform.localScale = Vector3.Lerp(visualiserSections[i].transform.localScale,
                                                                            new Vector3(1, 1, (AudioVisualistationScript.samplesArray[i] * maxHeight) + baseLength),
                                                                            0.1f);

                if (visualiserSections[i].transform.localScale.z > maxLengthScale)
                {
                    visualiserSections[255 - i].transform.localScale = new Vector3(1, 1, maxLengthScale + baseLength);
                    visualiserSections[i].transform.localScale = new Vector3(1, 1, maxLengthScale + baseLength);
                    visualiserSections[128 - i].transform.localScale = new Vector3(1, 1, maxLengthScale + baseLength);
                    visualiserSections[128 + i].transform.localScale = new Vector3(1, 1, maxLengthScale + baseLength);
                }


            }

        }
        if (TargetToFollow != null)
        {
            Vector3 tVector = sunPosition.transform.position - TargetToFollow.transform.position;
            sunPosition.transform.rotation = Quaternion.Lerp(sunPosition.transform.rotation, Quaternion.LookRotation(tVector) * Quaternion.Euler(0, 90, 90), lookSpeed*Time.deltaTime);
        }
    }
}
