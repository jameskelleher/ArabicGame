using UnityEngine;
using System.Collections;

public class InputTextController : MonoBehaviour {


	[HideInInspector] public string[] inputArray = new string[9];

	private GUIText gText;
	private string inputString;
	private string defaultText;

	// Use this for initialization
	void Start () {
		defaultText = "This is where arabic is displayed, yo";

		gText = this.gameObject.guiText;
		gText.text = defaultText;
	}
	
	// Update is called once per frame
	void Update () {
		inputString = "";
		for (int i = 0; i < inputArray.Length; i++) inputString += inputArray[i]; 
		if (inputString == "") gText.text = defaultText;
		else gText.text = RTLService.RTL.GetText(inputString);
	}

	public void UpdateGUI(string input, int index) {
		inputArray[index] = input;
	}
}
