using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private Transform teleportLocation;
    [SerializeField] private Camera targetCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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