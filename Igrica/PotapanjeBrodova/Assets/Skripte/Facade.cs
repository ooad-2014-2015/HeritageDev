using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System.Windows.Forms;

public class Facade : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;

	//lokacije idu random
	private int[] lokacije = {1, 0, 1, 1, 0, 1,
							  1, 0, 0, 0, 0, 0,
							  1, 0, 1, 0, 0, 0,
							  1, 0, 1, 0, 1, 1,
					 		  0, 0, 1, 0, 0, 0,
							  0, 1, 0, 0, 1, 1};

	private int pogodaka, promasaja;
	public Text pogodakaText, promasajaText, krajText;


	// Use this for initialization
	void Start () {
		pogodaka = promasaja = 0;
		postaviTextPogodaka ();
		postaviTextPromasaja ();
		krajText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {

		int brod;
		int.TryParse(hit.collider.gameObject.name, out brod);

			if (Input.GetMouseButtonDown (0)) {
				//MessageBox.Show(brod.ToString());
				if(lokacije[brod-1] == 1 && hit.collider.renderer.material.color != Color.red && hit.collider.renderer.material.color != Color.green) {
					hit.collider.renderer.material.color = Color.red;
					pogodaka++;
				}
				else if(lokacije[brod-1] == 0 && hit.collider.renderer.material.color != Color.red && hit.collider.renderer.material.color != Color.green) {
					hit.collider.renderer.material.color = Color.green;
					promasaja++;
				}
				postaviTextPogodaka ();
				postaviTextPromasaja ();

				if(pogodaka + promasaja == 36)
					krajText.text = "Kraj igre!!!";

			}
			//if (Physics.Raycast (ray, out hit, 1000.0f) && Input.GetMouseButtonDown (0)) {
			//	Destroy (hit.collider.gameObject);
			//}
		}
	}
	void postaviTextPromasaja() {
		promasajaText.text = "Promasaja: " + promasaja.ToString ();
	}

	void postaviTextPogodaka() {
		pogodakaText.text = "Pogodaka: " + pogodaka.ToString ();
	}

}
