using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float runSpeed = 15f;
    public float gravity = -9.81f;
    public float jumpSpeed = 1F;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    float currSpeed;

    [SerializeField]
    private string forward_kname = "forward";
    [SerializeField]
    private string backward_kname = "backward";
    [SerializeField]
    private string left_kname = "left";
    [SerializeField]
    private string right_kname = "right";
    [SerializeField]
    private string space_kname = "space";

    // Update is called once per frame
    void Update()
    {
        KeyCode forw = (KeyCode)PlayerPrefs.GetInt(forward_kname);
        KeyCode back = (KeyCode)PlayerPrefs.GetInt(backward_kname);
        KeyCode left = (KeyCode)PlayerPrefs.GetInt(left_kname);
        KeyCode right = (KeyCode)PlayerPrefs.GetInt(right_kname);
        KeyCode space = (KeyCode)PlayerPrefs.GetInt(space_kname);
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        currSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
            currSpeed = runSpeed;
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = 0f;
        float z = 0f;
        if (Input.GetKey(left))
            x = -1f;
        if (Input.GetKey(right))
            x += 1;
        if (Input.GetKey(back))
            z = -1f;
        if (Input.GetKey(forw))
            z += 1;
        if (Input.GetKey(space) && isGrounded)
            velocity.y += jumpSpeed;
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}