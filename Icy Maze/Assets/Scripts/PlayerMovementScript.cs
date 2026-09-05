using IcyMaze;
using UnityEngine;

/// Top-down character controller. The 2015 version polled four hard-coded KeyCodes,
/// snapped the character to one of four cardinal angles and applied both translations
/// when two keys were held, which made diagonals travel 41% faster than straight lines.
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementScript : MonoBehaviour
{
    static readonly int SpeedParam = Animator.StringToHash("Speed");

    public float moveSpeed = 5f;
    public float animSpeed = 1.5f;
    public bool canMove = true;

    [SerializeField] AudioClip[] footstep;
    [SerializeField] float turnDegreesPerSecond = 720f;
    [SerializeField] float footStepInterval = 0.28f;

    Animator anim;
    Rigidbody body;
    AudioSource movingSound;
    Vector3 lastPosition;
    float footStepTimer;
    bool movedThisStep;

    void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        movingSound = GetComponent<AudioSource>();
        lastPosition = transform.position;

        // Rendered at display rate while the body is stepped at the physics rate.
        body.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void OnEnable() => PlayerRef.Register(gameObject);

    void OnDisable() => PlayerRef.Unregister(gameObject);

    void Start()
    {
        anim.speed = animSpeed;
        // Two of the three scenes ship with canMove serialized false; the original forced
        // it true here and the scenes were never re-saved, so it has to stay forced.
        canMove = true;
    }

    void Update()
    {
        if (movedThisStep)
        {
            footStepTimer += Time.deltaTime;
            if (footStepTimer >= footStepInterval)
            {
                PlayFootStepAudio();
                footStepTimer = 0f;
            }
        }
        else
        {
            footStepTimer = footStepInterval;
        }
    }

    void FixedUpdate()
    {
        movedThisStep = (transform.position - lastPosition).sqrMagnitude > 1e-6f;
        lastPosition = transform.position;

        Vector2 input = GameInput.Move;
        // The scene cameras look down the +X axis, so screen-up is world -X and
        // screen-right is world +Z. The original encoded that as a table of four angles.
        Vector3 direction = new Vector3(-input.y, 0f, input.x);
        float magnitude = Mathf.Min(direction.magnitude, 1f);

        if (canMove && magnitude > 0.01f)
        {
            direction /= direction.magnitude;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(direction, Vector3.up),
                turnDegreesPerSecond * Time.fixedDeltaTime);
            transform.position += direction * (magnitude * moveSpeed * Time.fixedDeltaTime);
        }

        anim.SetFloat(SpeedParam, canMove ? magnitude : 0f, 0.08f, Time.fixedDeltaTime);
    }

    void PlayFootStepAudio()
    {
        if (movingSound == null || footstep == null || footstep.Length == 0) return;

        int n = footstep.Length > 1 ? Random.Range(1, footstep.Length) : 0;
        AudioClip clip = footstep[n];
        if (clip == null) return;

        movingSound.PlayOneShot(clip);
        // Move the clip just played to slot 0 so it cannot repeat next step.
        footstep[n] = footstep[0];
        footstep[0] = clip;
    }

    /// True when the character actually changed position during the last physics step,
    /// including while being shoved around by ice, magnets or blockers.
    public bool isMoving() => movedThisStep;
}
