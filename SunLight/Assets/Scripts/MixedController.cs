using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MixedController : MonoBehaviour
{
    public float speed = 5f;
    public float groundDistance = 1f;
    public float slopeLimit = 45f;
    public LayerMask groundLayer;
    public Camera cam;

    public Rigidbody rb;
    public SpriteRenderer sr;

    private Vector3 groundNormal = Vector3.up;
    private bool isGrounded;

    private Vector3 targetPosition;
    private bool hasTarget = false;

    private Vector2 input;

    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Prevent the player from falling over
        rb.freezeRotation = true;

        if (cam == null)
        {
            cam = Camera.main;
        }


        targetPosition = transform.position;
    }

    void Update()
    {
        CheckGround();

        input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
            {
                input.x = -1;
            }

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
            {
                input.x = 1;
            }

            if (Keyboard.current.sKey.isPressed ||
                Keyboard.current.downArrowKey.isPressed)
            {
                input.y = -1;
            }

            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed)
            {
                input.y = 1;
            }


            //if (Keyboard.current.qKey.wasPressedThisFrame)
            //{
            //    LoadPreviousScene();
            //}

            //if (Keyboard.current.eKey.wasPressedThisFrame)
            //{
            //    LoadNextScene();
            //}
        }

        //

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {

            if (cam != null)
            {

                Ray ray = cam.ScreenPointToRay(
                Mouse.current.position.ReadValue()
                );

                if (Physics.Raycast(
                    ray,
                    out RaycastHit hit,
                    100f,
                    groundLayer))
                {
                    targetPosition = hit.point;
                    hasTarget = true;
                }
            }
        }

        if (input.x != 0)
        {
            if (input.x < 0)
                sr.flipX = true;
            else if (input.x > 0)
                sr.flipX = false;
        }
        else if (hasTarget)
        {
            if (targetPosition.x < transform.position.x)
                sr.flipX = true;
            else if (targetPosition.x > transform.position.x)
                sr.flipX = false;
        }
    }

    void FixedUpdate()
    {

        if (!canMove) return; // fuck allat movement

        Vector3 moveDir = Vector3.zero;

        if (input != Vector2.zero)
        {
            moveDir = new Vector3(input.x, 0f, input.y);

            moveDir.Normalize();

            hasTarget = false;
        }


        else if (hasTarget)
        {
            Vector3 direction = targetPosition - transform.position;

            // Ignore vertical difference
            direction.y = 0f;

            if (direction.magnitude < 0.1f)
            {
                hasTarget = false;
            }
            else
            {
                moveDir = direction.normalized;
            }
        }


        if (isGrounded)
        {
            moveDir = Vector3.ProjectOnPlane(
                moveDir,
                groundNormal
            ).normalized;
        }


        Vector3 velocity = moveDir * speed;

        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    private void CheckGround()
    {
        RaycastHit hit;

        Vector3 castPosition =
            transform.position + Vector3.up * 0.2f;

        if (Physics.Raycast(
            castPosition,
            Vector3.down,
            out hit,
            groundDistance + 0.5f,
            groundLayer))
        {
            float slopeAngle =
                Vector3.Angle(hit.normal, Vector3.up);

            if (slopeAngle <= slopeLimit)
            {
                isGrounded = true;
                groundNormal = hit.normal;
            }
            else
            {
                isGrounded = false;
                groundNormal = Vector3.up;
            }
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
        }
    }

    private void LoadPreviousScene()
    {
        int currentScene =
            SceneManager.GetActiveScene().buildIndex;

        int totalScenes =
            SceneManager.sceneCountInBuildSettings;

        int previousScene = currentScene - 1;

        if (previousScene < 0)
        {
            previousScene = totalScenes - 1;
        }

        SceneManager.LoadScene(previousScene);
    }

    private void LoadNextScene()
    {
        int currentScene =
            SceneManager.GetActiveScene().buildIndex;

        int totalScenes =
            SceneManager.sceneCountInBuildSettings;

        int nextScene = currentScene + 1;

        if (nextScene >= totalScenes)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    public void SetCamera(Camera newCamera)
    {
        cam = newCamera;

        //targetPosition = null;
        hasTarget = false;
    }

}
