﻿using UnityEngine;
using System.Collections;

public class TriggersheetScript : MonoBehaviour {
	public bool isOn;
	Behaviour halo;
	// Use this for initialization
	void Start () {
		isOn = false;
		halo = (Behaviour)GetComponent("Halo");
		halo.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision other){
		if (other.collider.tag=="Playerchan") {

			isOn = true;
			halo.enabled= true;

		}

	
}

}
