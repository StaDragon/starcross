using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject debugMiddle;

    private Player[] players;
    private Vector3 playerMiddle;

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMiddle = (players[0].transform.position + players[1].transform.position) / 2;
        debugMiddle.transform.position = playerMiddle;
    }
}
