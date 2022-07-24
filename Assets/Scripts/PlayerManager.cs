using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject debugMiddle;
    public bool playersCombined = false;
    public bool invunerable;

    private bool playersAttracting = false;
    private Player[] players;
    private Rigidbody[] rBodies;
    private MeshRenderer[] pRenderers;
    private MeshRenderer mergedRenderer;
    private Vector3 playerMiddle;
    private FixedJoint joint;

    private int buttonDualHold = 0;
    private bool dualButtonSuccess = false;

    private float invunTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<Player>();
        rBodies = new Rigidbody[players.Length];
        pRenderers = new MeshRenderer[players.Length];
        for (int i = 0; i < rBodies.Length; i++)
        {
            rBodies[i] = players[i].GetComponent<Rigidbody>();
            pRenderers[i] = players[i].GetComponent<MeshRenderer>();
        }

        mergedRenderer = debugMiddle.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invunerable)
        {
            invunTimer += Time.deltaTime;
            if(invunTimer > 1.5f)
            {
                invunerable = false;
                invunTimer = 0f;
            }
        }

        playerMiddle = (players[0].transform.position + players[1].transform.position) / 2;
        debugMiddle.transform.position = playerMiddle;
        debugMiddle.transform.LookAt(players[0].transform.position, transform.up);

        //Debug.Log(dualButtonSuccess);

        if(buttonDualHold == 2 && !dualButtonSuccess)
        {
            //Debug.Log("DualButton Action");
            dualButtonSuccess = true;

            if (!playersCombined)
            {
                //Debug.Log("Merge");
                playersAttracting = true;
            }
            else
            {
                Split();
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

        mergedRenderer.enabled = true;

        foreach(MeshRenderer renderer in pRenderers)
        {
            renderer.enabled = false;
        }

        playersAttracting = false;
        playersCombined = true;
        //Debug.Log("Players should merge: " + p.name);

        players[0].SetMerged(true);
        players[1].SetMerged(true);

        if(players[0].GetComponent<FixedJoint>() == null)
        {
            joint = players[0].gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = rBodies[1];
        }
    }

    public void Split()
    {
        if (joint != null)
        {
            mergedRenderer.enabled = false;

            foreach (MeshRenderer renderer in pRenderers)
            {
                renderer.enabled = true;
            }

            Vector3 p1top2 = (players[1].transform.position - players[0].transform.position).normalized;
            Destroy(joint);
            rBodies[0].velocity = 5 * p1top2;
            rBodies[1].velocity = -5 * p1top2;
            playersCombined = false;
            players[0].SetMerged(false);
            players[1].SetMerged(false);
        }
    }

    public Player[] getPlayers()
    {
        return players;
    }
}

