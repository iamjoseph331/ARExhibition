using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class qrcode_reader : MonoBehaviour {
	public string url = "";
	private WebCamTexture camTexture;
	private Rect screenRect;
	// Use this for initialization
	void Start () {
		screenRect = new Rect(0, 0, Screen.width, Screen.height);
		camTexture = new WebCamTexture();
		camTexture.requestedHeight = Screen.height;
		camTexture.requestedWidth = Screen.width;
		if (camTexture != null) {
			camTexture.Play();
		}
	}

	void OnGUI () {
		// drawing the camera on screen
		//GUI.DrawTexture (screenRect, camTexture, ScaleMode.ScaleToFit);
		// do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
		try {
			IBarcodeReader barcodeReader = new BarcodeReader ();
			// decode the current frame
			var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width , camTexture.height);
			if (result != null) {
				Debug.Log("DECODED TEXT FROM QR: " + result.Text);
				url = result.Text;
			}
		} catch(System.Exception ex) { Debug.LogWarning (ex.Message); }
	}

	// Update is called once per frame
	void Update () {
		
	}
}
