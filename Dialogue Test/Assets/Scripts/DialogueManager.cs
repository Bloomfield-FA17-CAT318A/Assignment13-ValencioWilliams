using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Animator animator;

	public Text nameText;

	public Text dialogueTextBox;

	private Queue<string> sentences;

	void Start ()
	{
		sentences = new Queue<string> ();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool ("IsOpen", true);

		sentences.Clear (); //Clear all sentences on the Queue

		nameText.text = dialogue.name; //Change the name place holder to the npc's name

		foreach (string sentence in dialogue.sentences) //Loop through the string array in the dialogue
		{
			sentences.Enqueue (sentence); //Put all sentences from the dialogue in the Queue
		}
	
		DisplayNextSentence ();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0) // if the number of sentences in the queue is 0 
		{
			EndDialogue ();
			return;
		}

		string currentSentence = sentences.Dequeue ();

		StopAllCoroutines ();

		StartCoroutine (TypeSentence (currentSentence));
	}

	void EndDialogue ()
	{
		animator.SetBool ("IsOpen", false);
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueTextBox.text = "";
		foreach (char letter in sentence.ToCharArray ())
		{
			dialogueTextBox.text += letter;
			yield return null;
		}
	}
}
