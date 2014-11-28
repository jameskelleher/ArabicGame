using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameController : MonoBehaviour {

	public Sprite[] spriteLetters;
	public Sprite[] spriteDiacrits;
	public GameObject letterPrefab;
	public GameObject diacritPrefab;
	public GameObject inputPrefab;
	public TextAsset letterList;
	public TextAsset module;

	private Dictionary<string, Sprite> imgDict;
	private GameObject letterParent;
	private GameObject inputBoxParent;

	// Use this for initialization
	void Start () {
		imgDict = new Dictionary<string, Sprite>();

		string[] letterArray = letterList.text.Split('\n');

		for (int i = 0; i < letterArray.Length; i++) {
			imgDict.Add(letterArray[i], spriteLetters[i]);
		}

		letterParent = new GameObject();
		letterParent.name = "Letters";

		inputBoxParent = new GameObject();
		inputBoxParent.name = "Input Boxes";

		CreateLetter(letterArray[ 5],  0, -1);
		CreateLetter(letterArray[ 7],  2, -1);
		CreateLetter(letterArray[31],  1, -1);
		CreateLetter(letterArray[26], -1, -1);
		CreateLetter(letterArray[15], -2, -1);

		CreateInputBox(8, -6f, 2f);
		CreateInputBox(4, letterArray[5], 0f, 2f);

	}
	
	// Update is called once per frame
	void Update () {
	}

	public GameObject[] CreateInputRow(int len, int firstRootChar, float y, string[] rootLetters) {
		GameObject[] row = new GameObject[9];
		for (int i = 0; i < len; i++) {
			if (i == firstRootChar) {
				i += rootLetters.Length - 1;
				continue;
			}
			row[i] = CreateInputBox(i, 6f - 1.5f * i, y);
		}

		for (int i = firstRootChar; i < firstRootChar + rootLetters.Length; i++) {
			row[i] = CreateInputBox(i, rootLetters[i-firstRootChar], 6f - 1.5f * i, y);
		}
		return row;
	}
	
	public GameObject CreateLetter(string character, float xDefault, float yDefault) {
		Vector3 defaultPosition = new Vector3(xDefault, yDefault, -1f);
		return CreateLetter(character, imgDict[character], defaultPosition);
	}

	public GameObject CreateLetter(string character, Sprite sprite, float xDefault, float yDefault) {
		Vector3 defaultPosition = new Vector3(xDefault, yDefault, -1f);
		return CreateLetter(character, sprite, defaultPosition);
	}

	public GameObject CreateLetter(string character, Sprite sprite, Vector3 defaultPosition) {
		GameObject clone = (GameObject) Instantiate(letterPrefab, defaultPosition, Quaternion.identity);
		clone.GetComponent<SpriteRenderer>().sprite = sprite;
		clone.GetComponent<LetterController>().character = character;
		clone.transform.parent = letterParent.transform;
		clone.name = character;
		return clone;
	}

	public GameObject CreateInputBox(int index, float x, float y) {
		return CreateInputBox(index, "", new Vector3(x,y));
	}

	public GameObject CreateInputBox(int index, string rootLetter, float x, float y) {
		return CreateInputBox(index, rootLetter, new Vector3(x,y));
	}

	public GameObject CreateInputBox(int index, string rootLetter, Vector3 pos) {
		GameObject clone = (GameObject) Instantiate(inputPrefab, pos, Quaternion.identity);
		clone.GetComponent<InputBoxController>().Index = index;
		clone.transform.parent = inputBoxParent.transform;
		clone.name = "Input Box " + index;
		if (rootLetter != "") {
			clone.GetComponent<SpriteRenderer>().sprite = imgDict[rootLetter];
			clone.GetComponent<InputBoxController>().RootLetter = rootLetter;
		}
		return clone;
	}
}
