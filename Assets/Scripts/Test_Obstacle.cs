using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Obstacle : MonoBehaviour
{
    public float resetZDistance = -30;
    public float resetZPos = 50;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per physics frame
    void FixedUpdate()
    {

        float displacement = transform.position.z - speed * Time.deltaTime;


        if(displacement < resetZDistance)
        {
            displacement = resetZPos;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, displacement);

        
    }
}
