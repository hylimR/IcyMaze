using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public float animSpeed;
    private Animator anim;
    private float[] faceDirection = { 270f, 90f, 180f, 0f };  //front back left right
    private float h, v;
    public bool canMove;
    private Vector3 curPosition, lasPosition;
    [SerializeField] private AudioClip[] footstep;
    private AudioSource movingSound;
    private float footStepInterval;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = animSpeed;
        canMove = true;
        movingSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //stop movement animation when user no longer pressing key
        if (Input.GetKeyUp(KeyCode.A))
        {
            StopMove();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            StopMove();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            StopMove();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            StopMove();
        }
        //If user is moving , play the movement sound
        if (isMoving())
        { 
            footStepInterval += Time.deltaTime;
            if (footStepInterval > 0.2f)
            {
                PlayFootStepAudio();
                footStepInterval = 0f;
            }
        }
    }

    void FixedUpdate()
    {   //If current able to move freely, then change facing direction and starting moving depend on key press
        if (canMove)
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
    }

    //Play moving animation and move the character
    void Move(float val)
    {
        anim.SetFloat("Speed", Mathf.Abs(val));
        transform.Translate(new Vector3(0f, 0f, val * Time.deltaTime * moveSpeed));
    }

    //Stop the animation
    void StopMove()
    {
        anim.SetFloat("Speed", 0);
    }

    //Play the footstep audio
    private void PlayFootStepAudio()
    {
        int n = Random.Range(1, footstep.Length);
        movingSound.clip = footstep[n];
        movingSound.PlayOneShot(movingSound.clip);
        footstep[n] = footstep[0];
        footstep[0] = movingSound.clip;

    }

    //Check whether the character is moving
    public bool isMoving()
    {
        curPosition = transform.localPosition;
        if (curPosition == lasPosition)
        {
            return false;
        }
        lasPosition = curPosition;
        return true;
    }
}
