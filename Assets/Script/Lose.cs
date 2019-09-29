using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : MonoBehaviour {
    public Slider healthSlider;
    public Image lose;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        if (healthSlider.value <= 0)
            lose.gameObject.SetActive(true);
	}
}
