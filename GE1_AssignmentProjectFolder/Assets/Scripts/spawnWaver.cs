using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnWaver : MonoBehaviour
{
    [SerializeField] GameObject bodySegment, head;
    [SerializeField] float  spacing, waveHeight, speed, delay;
    [SerializeField] int segments, num;
    [SerializeField] GameObject[] segmentsArray;
    bool activateSpeedIncrease;
    WaveMotion pieceScript;
    // Start is called before the first frame update
    void Start()
    {
        segmentsArray = new GameObject[segments];
        
        for(int i = 0; i< segments; i++)
        {
            GameObject newPiece = bodySegment;
            GameObject piece = Instantiate(newPiece, this.transform);
            //GameObject piece = Instantiate(newPiece, new Vector3(this.transform.position.x, this.transform.position.y, -spacing * i), Quaternion.identity, this.transform.parent);
            piece.gameObject.name = "Segment" + (i+1);
            //piece.SetActive(false);
            pieceScript = piece.GetComponent<WaveMotion>();
            pieceScript.period = (0.25f * i) / segments;
            piece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-(spacing * i));
            pieceScript.rate = speed;
            pieceScript.height = waveHeight;
            //
            if (i<1)
            {
               piece.transform.localScale = new Vector3((1f / 1)+(2f/segments), piece.transform.localScale.y, piece.transform.localScale.z);
            }
            else
            {
               piece.transform.localScale = new Vector3(1f / i, piece.transform.localScale.y, piece.transform.localScale.z);
            }

            segmentsArray[i] = piece;
            
            //piece.SetActive(true);
        }
        
    }
    private void FixedUpdate()
    {
        foreach (GameObject piece in segmentsArray)
        {
          //  piece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -spacing * System.Array.IndexOf(segmentsArray, piece));
        }
    }
}
