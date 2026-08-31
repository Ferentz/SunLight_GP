using UnityEngine;

public class Teleport_Zones : MonoBehaviour 
{
    [SerializeField] private Transform teleportLocation;
    [SerializeField] private Camera targetCamera;

    [Header("Teleport Cooldown")]
    [SerializeField] private float cooldown = 3f;
    private bool isOnCooldown = false;

    private float cooldownTimer = 0f;

    private void Update()
    {
        // Count down the cooldown
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cooldownTimer > 0f)
                return;

            // Start cooldown
            cooldownTimer = cooldown;

            // Teleport player
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                playerRb.position = teleportLocation.position;
                playerRb.linearVelocity = Vector3.zero;
            }
            else
            {
                other.transform.position = teleportLocation.position;
            }


            // Switch camera
            if (targetCamera != null)
            {
                SwitchCamera(targetCamera);

                MixedController playerController = other.GetComponent<MixedController>();

                if (playerController != null)
                {
                    playerController.SetCamera(targetCamera);
                }
            }
        }
    }


    private void SwitchCamera(Camera newCamera)
    {
        // Disable all cameras
        Camera[] cameras = FindObjectsOfType<Camera>();

        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }

        // Enable selected camera
        newCamera.enabled = true;
    }
}