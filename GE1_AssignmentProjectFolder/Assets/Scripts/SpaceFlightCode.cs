using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFlightCode : MonoBehaviour
{
    [SerializeField] float speed,pitch, roll;
    Rigidbody _shipRB;
    
    Vector3 look;

    // Start is called before the first frame update
    void Start()
    {
        _shipRB = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (Input.GetKey(KeyCode.Space))
        {
            _shipRB.AddForce(transform.forward * speed);
        }
        */
        _shipRB.AddForce(transform.forward * speed*Input.GetAxis("RightTrigger"));
        //use lerp for rotation;
        float angleCorrector = _shipRB.velocity.magnitude*0.0125f;
        transform.Rotate(Vector3.right, pitch * Input.GetAxis("Vertical") * angleCorrector);
        transform.Rotate(Vector3.forward, roll * -Input.GetAxis("Horizontal") * angleCorrector);
    }


}
