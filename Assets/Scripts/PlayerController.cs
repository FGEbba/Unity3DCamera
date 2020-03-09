using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private CharacterController cc;
    public float moveSpeed = 1;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");



        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed;


        cc.Move(moveDirection * Time.deltaTime);
        cc.SimpleMove(Physics.gravity);


    }
}
