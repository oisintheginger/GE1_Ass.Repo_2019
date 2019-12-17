using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnWaver : MonoBehaviour
{
    [SerializeField] GameObject bodySegment; // the segment prefab
    [SerializeField] float  spacing, waveHeight, speed; // variables for the positioning, the maximum wave height and the speed of the sine wave motion
    [SerializeField] int segments; //the number of segments 
    [SerializeField] GameObject[] segmentsArray;
    WaveMotion pieceScript;

    //spawnWaver.cs is used to create the ship tail
    //it spawns a 'tail' made of prefab 'segments' 
    //each segment has a WaveMotion.cs script attached
    //As mentioned in WaveMotion.cs, each segments starts at a different point in its motion path
    //When being instantiated, the values for each segment variables used to create the motion is set
    //This includes its 'period' which allows for the wave motion
    //Each of the segments is added to an array of game objects
    
    void Start()
    {
        segmentsArray = new GameObject[segments];
        
        for(int i = 0; i< segments; i++)
        {
            GameObject newPiece = bodySegment;
            GameObject piece = Instantiate(newPiece, this.transform);
            piece.gameObject.name = "Segment" + (i+1);
            pieceScript = piece.GetComponent<WaveMotion>();
            pieceScript.period = (0.25f * i) / segments;
            piece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-(spacing * i));
            pieceScript.rate = speed;
            pieceScript.height = waveHeight;
            if (i<1)
            {
               piece.transform.localScale = new Vector3((1f / 1)+(2f/segments), piece.transform.localScale.y, piece.transform.localScale.z);
            }
            else
            {
               piece.transform.localScale = new Vector3(1f / i, piece.transform.localScale.y, piece.transform.localScale.z);
            }

            segmentsArray[i] = piece;
        }
        
    }
}
