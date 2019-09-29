using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapTrigger : MonoBehaviour {
    Slider healthSlider;
    Text healthText;
	// Use this for initialization
	void Start () {
        healthSlider = GameObject.Find("Health").GetComponent<Slider>();
        healthText = GameObject.Find("HealthNumber").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            healthSlider.value -= 10;
            healthText.text = "HP:" + healthSlider.value;
        }
    }
}
