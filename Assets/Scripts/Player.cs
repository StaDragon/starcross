using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1, 2)]
    public int playerNum = 1;

    private Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Hor_P" + playerNum.ToString()), Input.GetAxis("Ver_P" + playerNum.ToString()));
        Debug.Log("Input Debug: Movement_Vector = " + inputVector.ToString() + " Button = " + Input.GetButton("Button_P" + playerNum.ToString()));


        rBody.AddForce(10 * (Vector3) inputVector - rBody.velocity);

    }

    public Rigidbody getRigidbody()
    {
        return rBody;
    }
}
