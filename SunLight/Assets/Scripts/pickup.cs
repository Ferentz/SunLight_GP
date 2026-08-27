using UnityEngine;
using UnityEngine.Events;

public class Pickup : Interactable
{
    [SerializeField]short ID;

	static public UnityEvent<short> OnPickup = new UnityEvent<short>();

	// Start is called once before the first execution of Update after the MonoBehaviour is created

	public override void Interact()
	{
        Debug.Log("interact");
        OnPickup.Invoke(ID);
		Destroy(gameObject, 1f);
	}
}
