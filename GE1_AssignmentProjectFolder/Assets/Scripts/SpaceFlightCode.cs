using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFlightCode : MonoBehaviour
{
    [SerializeField] float speed, boost, braking,pitch, roll;
    Rigidbody _shipRB;
    [SerializeField] Camera mC;
    Vector3 look;

    // Start is called before the first frame update
    void Start()
    {
        _shipRB = this.GetComponent<Rigidbody>();
    }

    //The player moves through using Xbox One Controller Input
    //The player always moves in its forward direction 
    //The player is able to alter the angle of their ship in order to change direction
    //The player can boost and brake
    //The Camera fov changes when boosting or braking to enhance feeling of 'speed'
    void FixedUpdate()
    {
        Camera.main.fieldOfView =Mathf.Lerp(60f, 
                                            60f + (30f * Mathf.Abs(Input.GetAxis("RightTrigger"))) - (15f * Mathf.Abs(Input.GetAxis("LeftTrigger"))),
                                            Time.deltaTime*50f);
        
        
        _shipRB.AddForce((transform.forward * (speed+(boost*Input.GetAxis("RightTrigger")))) * (1 - Input.GetAxis("LeftTrigger")));
        float angleCorrector = Mathf.Abs(1.5f- Input.GetAxis("RightTrigger"));
        transform.Rotate(Vector3.right, pitch * Input.GetAxis("Vertical") * angleCorrector);
        transform.Rotate(Vector3.forward, roll * -Input.GetAxis("Horizontal") * angleCorrector);
    }


}
