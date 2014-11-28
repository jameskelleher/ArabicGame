using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class LetterController : MonoBehaviour {

	public string character;
	public Vector3 defaultPosition;

	private bool held;
	private GameObject box;
	

	// Use this for initialization
	void Start () {
		Debug.Log ("My character is " + character);
		defaultPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (held) {
			Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newPosition.z = defaultPosition.z;
			transform.position = newPosition;
		}
		else if (box == null) {
			transform.position = defaultPosition;
		}
		//else the tile is left where it is, i.e. in the input box
	}

	void OnMouseDown() {
		held = true;
		if (box != null) {
			box.GetComponent<InputBoxController>().EmbeddedTile = null;
//			box = null;
		}
	}

	void OnMouseUp() {
		held = false;
		if (box != null) {
			GameObject et = box.GetComponent<InputBoxController>().EmbeddedTile;

			if (et != null) {
				et.transform.position = et.GetComponent<LetterController>().defaultPosition;
				et.GetComponent<LetterController>().box = null;
			}

			box.GetComponent<InputBoxController>().EmbeddedTile = this.gameObject;

			Vector3 newPosition = box.transform.position;
			newPosition.z = defaultPosition.z;
			this.transform.position = newPosition;

		}
	}

	public void LetterCollisionReplacement(GameObject coming, GameObject going) {
		GameObject box = coming.GetComponent<LetterController>().box;
		LetterCollisionReplacement(coming, going, box);
	}

	public void LetterCollisionReplacement(GameObject coming, GameObject going, GameObject box) {
		going.transform.position = going.GetComponent<LetterController>().defaultPosition;
		going.GetComponent<LetterController>().box = null;
		box.GetComponent<InputBoxController>().EmbeddedTile = coming;

		Vector3 newPosition = box.transform.position;
		newPosition.z = defaultPosition.z;
		coming.transform.position = newPosition;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "InputBox" && !other.gameObject.GetComponent<InputBoxController>().IsRoot) {
			box = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "InputBox" && other.gameObject == this.box) {
			InputBoxController bc = box.GetComponent<InputBoxController>();
			if (bc.EmbeddedTile == this.gameObject) {
				bc.EmbeddedTile = null;
			}
			box = null;
		}
	}
}
