using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour {


	// Update is called once per frame
	void Update () {
        PlayerController player = FindObjectOfType<PlayerController>();
        transform.forward = player.transform.forward;
	}
}
