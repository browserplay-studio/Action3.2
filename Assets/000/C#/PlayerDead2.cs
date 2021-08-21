using UnityEngine;
using System.Collections;

public class PlayerDead2 : MonoBehaviour {

	public GameObject Player;
	public GameObject Ragdoll;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Play") {
			Destroy (Player);
			
				Instantiate(Ragdoll, transform.position, transform.rotation );
		}
	}


}