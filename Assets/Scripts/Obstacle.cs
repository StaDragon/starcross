using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float sideSpeed = 5;
    public float frontSpeed = 40;

    public float thresholdSide = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horDisplacement = sideSpeed * Time.deltaTime;

        float frontDisplacement = transform.position.z - frontSpeed * Time.deltaTime;

        if ((transform.position.x > -thresholdSide && transform.position.x < thresholdSide) &&
            (transform.position.y > -thresholdSide && transform.position.y < thresholdSide))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, frontDisplacement);
        }
        else
        {
            if (transform.position.x > thresholdSide)
            {
                transform.position = new Vector3(transform.position.x - horDisplacement, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -thresholdSide)
            {
                transform.position = new Vector3(transform.position.x + horDisplacement, transform.position.y, transform.position.z);
            }


            if (transform.position.y > thresholdSide)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - horDisplacement, transform.position.z);
            }
            else if (transform.position.y < -thresholdSide)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + horDisplacement, transform.position.z);
            }
        }



    }
}
