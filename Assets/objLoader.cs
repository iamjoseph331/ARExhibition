// This example loads an OBJ file from the WWW, including the MTL file and any textures that might be referenced

using UnityEngine;
using System.Collections;



namespace UnityEngine.XR.iOS
{
	public class objLoader : MonoBehaviour {
		
		GameObject thePlayer;
		public string objFileName = "http://www.starscenesoftware.com/objtest/Spot.obj";

		public Material standardMaterial;	// Used if the OBJ file has no MTL file
		public ObjReader.ObjData objData;
		string loadingText = "";
		bool loading = false;

		IEnumerator Load () {
			loading = true;
			if (objData != null && objData.gameObjects != null) {
				for (var i = 0; i < objData.gameObjects.Length; i++) {
					Destroy (objData.gameObjects[i]);
				}
			}

			objData = ObjReader.use.ConvertFileAsync (objFileName, true, standardMaterial);
			while (!objData.isDone) {
				loadingText = "Loading... " + (objData.progress*100).ToString("f0") + "%";
				if (Input.GetKeyDown (KeyCode.Escape)) {
					objData.Cancel();
					loadingText = "Cancelled download";
					loading = false;
					yield break;
				}
				yield return null;
			}
			loading = false;
			if (objData == null || objData.gameObjects == null) {
				loadingText = "Error loading file";
				yield return null;
				yield break;
			}
			foreach(GameObject gam in objData.gameObjects){
				gam.transform.position = new Vector3 (-10,0,0);
			}
			loadingText = "Import completed";
		}

		void OnGUI () {
			thePlayer = GameObject.Find("ObjManager");
			GUILayout.BeginArea (new Rect(10, 10, 400, 400));
			objFileName = GUILayout.TextField (objFileName, GUILayout.Width(400));
			if(thePlayer.GetComponent<qrcode_reader>().url != null ){
				objFileName = thePlayer.GetComponent<qrcode_reader>().url;
			}
			if (GUILayout.Button ("Import") && !loading) {
				StartCoroutine (Load());
			}
			GUILayout.Label (loadingText);
			GUILayout.EndArea();
		}
			
	}
}