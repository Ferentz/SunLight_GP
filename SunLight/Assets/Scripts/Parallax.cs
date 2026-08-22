using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public Transform cam;
    public float parallaxEffect;

    private float spriteWidth;
    private float startX;

    private void Start()
    {
        cam = Camera.main.transform;
        startX = transform.position.x;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void LateUpdate()
    {
        // Move with parallax
        float distance = cam.position.x * parallaxEffect;

        transform.position = new Vector3(
            startX + distance,
            transform.position.y,
            transform.position.z
        );


        // Recycle sprite when it goes too far away
        float cameraDistance = cam.position.x - transform.position.x;

        if (cameraDistance > spriteWidth * 1.5f)
        {
            startX += spriteWidth * 3;
        }
        else if (cameraDistance < -spriteWidth * 1.5f)
        {
            startX -= spriteWidth * 3;
        }
    }
}