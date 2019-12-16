using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectScript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float maxDistance, correctionSpeed;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);
        float correction = (dist - maxDistance) * Time.deltaTime;
        if(dist > maxDistance)
        {
            Vector3 lookVector = this.transform.position - Player.transform.position;
            Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, Quaternion.LookRotation(lookVector), correctionSpeed * Time.deltaTime *correction);
            
        }
        
    }
}
