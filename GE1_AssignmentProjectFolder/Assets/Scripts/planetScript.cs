using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetScript : MonoBehaviour
{
    public orbitGenerator orbitPath; //reference to the orbit generator class
    public Transform planetObject; //the object to move around the center
    public GameObject player; //the player to track the motion of

    //Variables for shooting at the player
    [SerializeField]
    float waitToShoot = 0.5f;
    public Transform  shootPoint;
    public GameObject projectile;
    public float bulletSpeed;
    [SerializeField] float waitShoot = 1f;
    float timer = 0.0f;

    //Planet orbiting and targeting
    [Range(0f, 1f)]
    public float orbitComplete = 0f;
    public float orbitTime = 3f;
    float regularComplete;
    public float orbitSpeed;
    public float orbitAngle, xIn, zIn;
    public float lookSpeed, detectionRadius;


    [SerializeField] float maxRadius, baseRadius, scaler, lerpSpeed;

    //Class for creating different ammuntion types
    [System.Serializable]
    public class AmmunitionPool
    {
        public string tag;
        public GameObject bullet;
        public int size;
    }

    Dictionary<string, Queue<GameObject>> ammunitionDictionary;
    public List<AmmunitionPool> ammunitionPoolsList;

    //this is the same script I used to solve lab 2
    //It uses object pools to create a limited amount of ammunition to use
    //The tutorial is for this is found at: https://www.youtube.com/watch?v=tdSmKaJvCoA 
    //The tutorial is an introduction to the concept of Object Pooling

    void Shooting2(string tag)
    {
        if (shootPoint != null)
        {
            GameObject bullet2Spawn = ammunitionDictionary[tag].Dequeue();
            bullet2Spawn.SetActive(true);
            bullet2Spawn.transform.position = shootPoint.position;
            bullet2Spawn.transform.rotation = shootPoint.rotation;
            if (bullet2Spawn.GetComponent<Rigidbody>() == false)
            {
                bullet2Spawn.AddComponent<Rigidbody>();
            }
            bullet2Spawn.GetComponent<Rigidbody>().velocity = bullet2Spawn.transform.forward * bulletSpeed;
            bullet2Spawn.GetComponent<Rigidbody>().useGravity = false;
            ammunitionDictionary[tag].Enqueue(bullet2Spawn);
            
        }
    }
    
    void Start()
    {
        setPlanetPosition();

        //creating and filling the different pools of ammo based on the serializable class
        ammunitionDictionary = new Dictionary<string, Queue<GameObject>>();
        if (projectile != null)
        {
            foreach (AmmunitionPool ammo in ammunitionPoolsList)
            {
                Queue<GameObject> bulletPool = new Queue<GameObject>();
                for (int i = 0; i < ammo.size; i++)
                {
                    GameObject newBullet = Instantiate(ammo.bullet);
                    newBullet.SetActive(false);
                    bulletPool.Enqueue(newBullet);
                    StartCoroutine(bulletcall(bulletPool, newBullet));
                }
                ammunitionDictionary.Add(ammo.tag, bulletPool);

            }
        }
    }

    //moving the planet to a new vector3, which is calculated by the orbitGenerator.cs class 
    void setPlanetPosition()
    {
        Vector3 orbitPosition = orbitPath.calculation(orbitComplete, orbitAngle, xIn, zIn);
        planetObject.localPosition = orbitPosition;
    }
    
    void FixedUpdate()
    {
        //the values used to target the player when they get too close to the planet object
        float playerDist = Vector3.Distance(planetObject.transform.position, player.transform.position);
        Vector3 tV = -planetObject.transform.position + player.transform.position;
        
        //when the orbit complete gets too small it gets reset
        if (Mathf.Abs(orbitComplete) < 0.0000001f)
        {
            orbitComplete *= 0.001f;
        }

        //the player distance to the planet object is used to alter its behaviour
        //When it is outside of the range, it orbits as per usual
        //Otherwise the planet freezes and targets the player and begins to shoot

        if (playerDist > detectionRadius) // carry on as usual
        {
            orbitSpeed = 1f / orbitTime;
            orbitComplete += Time.deltaTime * orbitSpeed;
            orbitComplete %= 1f;
        }
        else if(playerDist < detectionRadius) // stop, target and shoot at the player
        {
            planetObject.transform.rotation = Quaternion.Lerp(planetObject.transform.rotation, Quaternion.LookRotation(tV), lookSpeed * Time.deltaTime);
         
            timer += Time.deltaTime;
            if (timer >= 1f / waitShoot)
            {
                Shooting2("Plasma Ball"); // the "Plasma Ball" is the type of ammunition that is being used, in future other types of ammunition could be added
                timer -= waitShoot;
            }
        }

        beatScaler(); //scaling the radius of the planet depending on the first frequency band generated in AudioVisualizationScript.cs
        setPlanetPosition(); // setting the planet position
    }

    //scaling the radius to the first frequency band in the freqBand array in AudioVisualisationScript.cs
    void beatScaler()
    {
        planetObject.transform.localScale = Vector3.Lerp(planetObject.transform.localScale,
                                                                        new Vector3((AudioVisualistationScript.freqBand[0] * maxRadius * scaler) + baseRadius, (AudioVisualistationScript.freqBand[0] * maxRadius*scaler) + baseRadius, (AudioVisualistationScript.freqBand[0] * maxRadius * scaler) + baseRadius),
                                                                        lerpSpeed*Time.deltaTime);
    }

    //Re-queues the used bullet after 5 seconds, this allows it to be re-used
    IEnumerator bulletcall(Queue<GameObject> bulletPool, GameObject newBullet)
    {
        yield return new WaitForSecondsRealtime(5.0f);
        bulletPool.Enqueue(newBullet);
    }
}
