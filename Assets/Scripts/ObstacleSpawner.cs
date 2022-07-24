using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject[] obstacles;
    public bool spawnContinuous;
    public float spawnFrequency = 5f;
    public float distFromCentre = 10f;


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

                int randInt0 = Random.Range(0,obstacles.Length - 1);

                GameObject obstacle = obstacles[randInt0];

                int randInt1 = Random.Range(0, 3);

                switch (randInt1)
                {
                    case 0:
                        obstacle.transform.position = new Vector3(distFromCentre, 0, transform.position.z);
                        break;
                    case 1:
                        obstacle.transform.position = new Vector3(0, distFromCentre, transform.position.z);
                        break;
                    case 2:
                        obstacle.transform.position = new Vector3(-distFromCentre, 0, transform.position.z);
                        break;
                    default:
                        obstacle.transform.position = new Vector3(0, -distFromCentre, transform.position.z);
                        break;
                }

                int randInt2 = Random.Range(0, 3);
                switch (randInt2)
                {
                    case 0:
                        obstacle.transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 1:
                        obstacle.transform.localScale = new Vector3(-1, -1, 1);
                        break;
                    case 2:
                        obstacle.transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    default:
                        obstacle.transform.localScale = new Vector3(1, -1, 1);
                        break;
                }

                Instantiate(obstacle);
            }
        }
    }
}
