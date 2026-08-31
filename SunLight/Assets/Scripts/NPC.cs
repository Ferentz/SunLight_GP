 
using System;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.UI;

// this feels silly and roundabout
[System.Serializable]
public struct Sentence
{
	public string sentence;
}
[System.Serializable]
public class Convo
{
	public List<Sentence> convo = new List<Sentence>();
}

public class NPC : Interactable
{
    //public string conversations;
	public List<Convo> conversations = new List<Convo>();
	private int convIDX, sentIDX;
	public TMP_Text text;
	const float coolDownTime = 1;
	float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
		if (timer >= 0)
		{
			timer -= Time.deltaTime;
		}

    }


	public override void Interact()
	{
		if (timer > 0) return;
		timer += coolDownTime;
		// if there are no more conversations to be had
		if (convIDX >= conversations.Count) return;

		// end of a conversation was reached;
		if (sentIDX >= conversations[convIDX].convo.Count)
		{
			convIDX++;
			sentIDX = 0;
			text.gameObject.transform.parent.gameObject.SetActive(false);
			return;
		}

		text.gameObject.transform.parent.gameObject.SetActive(true);
		text.text = conversations[convIDX].convo[sentIDX].sentence;
		sentIDX++;
	}
}
