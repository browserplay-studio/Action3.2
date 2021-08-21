using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACTIVOBJECT2 : MonoBehaviour
{
     public GameObject Player;
	public GameObject Ragdoll;


	void OnTriggerEnter(Collider other){
		if (other.tag == "Play2") {
			
			
			
            Ragdoll.SetActive (true);
			
		}
	}


}