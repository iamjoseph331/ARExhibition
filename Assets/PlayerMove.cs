using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMove : NetworkBehaviour
{
	private Slider rotationSlider;
	void Start(){
		rotationSlider = GameObject.Find ("rotationSlider").GetComponent<Slider>();
		GameObject.Find ("rotationSlider").GetComponent<rotationSlider>().addObject (this.gameObject);
	}
	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}
	void Update()
	{
		if (!isLocalPlayer)
			return;
		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, 0, z);
	}

	public void RotateMyObject(float value)
	{
		transform.rotation = Quaternion.Euler(value * 360, 0, 90);
	}
}