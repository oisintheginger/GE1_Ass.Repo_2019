using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpaceFlightCode : MonoBehaviour
{
    [SerializeField] GameObject audioSource;
    [SerializeField] float speed, boost, braking,pitch, roll;
    Rigidbody _shipRB;
    [SerializeField] Camera mC;
    Vector3 look;
    [SerializeField] List<AudioClip> musicSelection;
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            musicSelect();
        }
        audioSource.GetComponent<AudioSource>().volume += Input.GetAxis("Mouse X") * Time.deltaTime; //controlling the volume with the right joystick
        Debug.Log(Input.GetAxis("Mouse X"));
        Camera.main.fieldOfView =Mathf.Lerp(60f, 
                                            60f + (30f * Mathf.Abs(Input.GetAxis("RightTrigger"))) - (15f * Mathf.Abs(Input.GetAxis("LeftTrigger"))),
                                            Time.deltaTime*50f);
        
        
        _shipRB.AddForce((transform.forward * (speed+(boost*Input.GetAxis("RightTrigger")))) * (1 - Input.GetAxis("LeftTrigger")));
        float angleCorrector = Mathf.Abs(1.5f- Input.GetAxis("RightTrigger"));
        transform.Rotate(Vector3.right, pitch * Input.GetAxis("Vertical") * angleCorrector);
        transform.Rotate(Vector3.forward, roll * -Input.GetAxis("Horizontal") * angleCorrector);
    }

    void musicSelect()
    {
        System.Random rnd = new System.Random();
        int SelectInt = rnd.Next(0, musicSelection.Count+1);
        audioSource.GetComponent<AudioSource>().clip = musicSelection[SelectInt];
        audioSource.GetComponent<AudioSource>().Play();
    }
}
