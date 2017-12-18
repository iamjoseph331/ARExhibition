using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;


namespace UnityEngine.XR.iOS
{

	public class qrcode_reader : MonoBehaviour {
		public string url = "";
		//public RenderTexture camTexture;

		private WebCamTexture camTexture;
		private Rect screenRect;
		private GameObject MC, ARC;

		void Start() {
			screenRect = new Rect(0, 0, Screen.width, Screen.height);
			camTexture = new WebCamTexture();
			camTexture.requestedHeight = Screen.height;
			camTexture.requestedWidth = Screen.width;
			MC = GameObject.Find("Main Camera");
			MC.GetComponent<UnityARVideo>().enabled = false;
			ARC = GameObject.Find("ARCameraManager");
			ARC.GetComponent<UnityARCameraManager> ().enabled = false;
			if (camTexture != null) {
				camTexture.Play();
			}
		}

		void OnGUI () {
			if (url != "") {
				return;
			}
			// drawing the camera on screen
			GUI.DrawTexture (screenRect, camTexture, ScaleMode.ScaleToFit);
			// do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
			try {
				IBarcodeReader barcodeReader = new BarcodeReader ();
				// decode the current frame
				var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width , camTexture.height);
				if (result != null) {
					Debug.Log("DECODED TEXT FROM QR: " + result.Text);
					url = result.Text;
					camTexture.Stop();
					MC.GetComponent<UnityARVideo>().enabled = true;
					ARC.GetComponent<UnityARCameraManager> ().enabled = true;
				}
			} catch(System.Exception ex) { Debug.LogWarning (ex.Message); }
		}
	}
}