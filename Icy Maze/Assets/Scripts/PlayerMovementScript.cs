using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public float animSpeed = 3.5f;
    private Animator anim;
    private float[] faceDirection = {270f, 90f, 180f, 0f };  //front back left right
    private float h, v;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = animSpeed;
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetFloat("Speed", 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetFloat("Speed", 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetFloat("Speed", 0);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetFloat("Speed", 0);
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.localEulerAngles = new Vector3(0f, faceDirection[2], 0f); 
            Move(-h);
        }
       if (Input.GetKey(KeyCode.D))
        {
            transform.localEulerAngles = new Vector3(0f, faceDirection[3], 0f);
            Move(h);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.localEulerAngles = new Vector3(0f, faceDirection[0], 0f);
            Move(v);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localEulerAngles = new Vector3(0f, faceDirection[1], 0f);
            Move(-v);
        }

        
    }

    void Move(float val)
    {
        anim.SetFloat("Speed", Mathf.Abs(val));
        transform.Translate(new Vector3(0f, 0f, val * Time.deltaTime * moveSpeed));
    }
}
