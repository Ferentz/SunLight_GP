using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct item
{
    public GameObject sprite;
    public short ID;
}
[System.Serializable]
public class mDict
{
	public List<item> dict = new List<item>();
}
public class Inventory : MonoBehaviour
{
    private bool visible = false;
    public mDict _items;
	[SerializeField]public Dictionary<short, GameObject> items = new Dictionary<short, GameObject>();
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		Pickup.OnPickup.AddListener(this.RegisterItem);
		foreach (var item in _items.dict)
        {
            items.Add(item.ID, item.sprite);
        }
		var renderers = GetComponentsInChildren<Image>();
		foreach (var ren in renderers)
		{
			ren.enabled = false;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleApearance()
    {
        if (visible)
        {
            var renderers = GetComponentsInChildren<Image>() ;
            foreach (var ren in renderers)
            { 
                ren.enabled = false;
            }
			//this.gameObject.SetActive(false);
            visible = false;

        }
        else
        {
			//this.gameObject.SetActive(true);
			var renderers = GetComponentsInChildren<Image>();
			foreach (var ren in renderers)
			{
				ren.enabled = true;
			}
			visible = true;

		}
    }

    public void RegisterItem(short ID)
    {
        Debug.Log("registered");
        items[ID].SetActive(true);
        if(!visible)
        {
			items[ID].GetComponent<Image>().enabled = false;

		}
		//items[ID].GetComponent<Renderer>().enabled = true;

	}
}
