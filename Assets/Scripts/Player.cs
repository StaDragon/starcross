using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1, 2)]
    public int playerNum = 1;

    private Rigidbody rBody;
    private PlayerManager pManager;
    private bool attracting = false;
    private bool merged = false;
    private float movementPenalty = 1.0f;
    private Transform otherPlayer = null; 

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        pManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Button_P" + playerNum.ToString()))
        {
            pManager.buttonPressed();
        }
        else if(Input.GetButtonUp("Button_P" + playerNum.ToString()))
        {
            pManager.buttonReleased();
        }
        
        Vector2 inputVector = new Vector2(Input.GetAxis("Hor_P" + playerNum.ToString()), Input.GetAxis("Ver_P" + playerNum.ToString()));
        //Debug.Log("Input Debug: Movement_Vector = " + inputVector.ToString() + " Button = " + Input.GetButton("Button_P" + playerNum.ToString()));
        rBody.AddForce((10 * (Vector3)inputVector - rBody.velocity) * movementPenalty);
        

    }

    public Rigidbody getRigidbody()
    {
        return rBody;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Damage player on collision
        for (int k = 0; k < collision.contacts.Length; k++)
        {

            //Debug.Log(Vector3.Dot(transform.forward, collision.contacts[k].normal));

            if (Vector3.Dot(-transform.forward, collision.contacts[k].normal) >= 0.9)
            {
                Debug.Log("Player was damaged");
            }
        }

        //Merge players on contact
        if (attracting && collision.gameObject.name == otherPlayer.name)
        {
            pManager.Merge(this.gameObject);
            attracting = false;
        }
    }

    public void Attract(Transform pTransform)
    {
        otherPlayer = pTransform;
        attracting = true;
        Vector3 toPlayer = (pTransform.position - transform.position).normalized;
        rBody.AddForce(40 * toPlayer - rBody.velocity/2);
    }
}
