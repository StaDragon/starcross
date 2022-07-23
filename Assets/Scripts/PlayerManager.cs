using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject debugMiddle;

    private bool playersCombined = false;
    private bool playersAttracting = false;
    private Player[] players;
    private Rigidbody[] rBodies;
    private Vector3 playerMiddle;
    private FixedJoint joint;

    private int buttonDualHold = 0;
    private bool dualButtonSuccess = false;

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<Player>();
        rBodies = new Rigidbody[players.Length];
        for(int i = 0; i < rBodies.Length; i++)
        {
            rBodies[i] = players[i].GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerMiddle = (players[0].transform.position + players[1].transform.position) / 2;
        debugMiddle.transform.position = playerMiddle;

        Debug.Log(dualButtonSuccess);

        if(buttonDualHold == 2 && !dualButtonSuccess)
        {
            Debug.Log("DualButton Action");
            dualButtonSuccess = true;

            if (!playersCombined)
            {
                Debug.Log("Merge");
                playersAttracting = true;
            }
            else
            {
                if(joint != null)
                {
                   
                    Vector3 p1top2 = (players[1].transform.position - players[0].transform.position).normalized;
                    Destroy(joint);
                    rBodies[0].velocity = 5 * p1top2;
                    rBodies[1].velocity = -5 * p1top2;
                    playersCombined = false;
                }
            }
        }

        if(buttonDualHold < 2)
        {
            dualButtonSuccess = false;
        }

        if (playersAttracting)
        {
            players[0].Attract(players[1].transform);
            players[1].Attract(players[0].transform);
        }
    }

    public void buttonPressed()
    {
        buttonDualHold++;
    }

    public void buttonReleased()
    {
        buttonDualHold--;
    }

    public void Merge(GameObject p)
    {
        //dualButtonSuccess = false;

        playersAttracting = false;
        playersCombined = true;
        Debug.Log("Players should merge: " + p.name);

        if(players[0].GetComponent<FixedJoint>() == null)
        {
            joint = players[0].gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = rBodies[1];
        }
    }
}

