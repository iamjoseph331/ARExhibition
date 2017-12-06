using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotationSlider : MonoBehaviour {

	GameObject rotationObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void valueChanged(float value){
		float realValue = this.gameObject.GetComponent<Slider> ().value; // Value passed is set to 0, need to find real one
		if (rotationObject) {
			rotationObject.GetComponent<PlayerMove>().RotateMyObject (realValue);
		}
	}

	public void addObject(GameObject obj){
		rotationObject = obj;
	}
}
