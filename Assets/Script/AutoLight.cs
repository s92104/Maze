using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLight : MonoBehaviour {
    public Light autoLight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            autoLight.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            autoLight.gameObject.SetActive(false);
    }
}
