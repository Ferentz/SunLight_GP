using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private Vector2 moveAmt;
    private bool useLight;
    private float doInteract;
	[SerializeField] public Rigidbody rb;

    public float WalkSpeed = 5;

	public uiBar _light;
	public uiBar sanity;

	public Interactable interactable;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + new Vector3(moveAmt.x, 0, moveAmt.y) * WalkSpeed * Time.deltaTime);

	}

	public void OnMove(InputAction.CallbackContext ctx)
	{
        moveAmt = ctx.ReadValue<Vector2>();
	}

	public void OnLight(InputAction.CallbackContext ctx)
	{
		float val = ctx.ReadValue<float>();
		if (val > 0)
		{
			useLight = true;
            _light.state = uiBar.barState.drain;
			sanity.state = uiBar.barState.gain;
		}
		else
		{
            _light.state = uiBar.barState.station;
			sanity.state = uiBar.barState.drain;
		}
	}

	public void OnInteract(InputAction.CallbackContext ctx)
	{
		doInteract = ctx.ReadValue<float>();
		if(interactable != null)
		{

			interactable.Interact();
		}
	}
}
