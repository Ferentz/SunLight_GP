using UnityEngine;
using UnityEngine.UIElements;

public abstract class Interactable : MonoBehaviour
{
	public Sprite inRangeSprite;
	public Sprite outRangeSprite;

	public SpriteRenderer renderer;

	private void Start()
	{
		renderer.sprite = outRangeSprite;
	}
	public abstract void Interact();

	void OnTriggerEnter(Collider collision)
	{
		Debug.Log("entered!");

		var player = collision.gameObject.GetComponent<Player>();
		if (player != null)
		{
			renderer.sprite = inRangeSprite;
			player.interactable = this;
		}
	}

	void OnTriggerExit(Collider collision)
	{
		Debug.Log("left!");
		renderer.sprite = outRangeSprite;
		var player = collision.gameObject.GetComponent<Player>();
		if (player != null)
		{
			if (player.interactable == this)
			{
				player.interactable = null;
			}
		}
	}
}
