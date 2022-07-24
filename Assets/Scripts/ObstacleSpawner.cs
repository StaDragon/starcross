using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject[] obstacles;
    public bool spawnContinuous;
    public float spawnFrequency = 5f;


    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnContinuous)
        {
            timer += Time.deltaTime;


            if (timer > spawnFrequency)
            {
                timer = 0f;

                int randInt = (int)Random.Range(0,obstacles.Length - 0.1f);

                GameObject obstacle = obstacles[randInt];

                obstacle.transform.position = new Vector3(0,0,transform.position.z);

                Instantiate(obstacle);
            }
        }
    }
}
