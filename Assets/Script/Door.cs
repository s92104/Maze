using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    bool openDoor = false;
    public float speed=1;
    GameObject doorRoot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit = new RaycastHit();
            Ray mouseray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(mouseray, out hit, Mathf.Infinity, 2048))
            {
                if (hit.transform.name == "Door")
                {
                    openDoor = true;
                    doorRoot = hit.transform.parent.gameObject;
                }
            }
        }
        if(openDoor)
        {
            if (doorRoot.transform.rotation.eulerAngles.y > 290 || doorRoot.transform.rotation.eulerAngles.y == 0)
                doorRoot.transform.Rotate(new Vector3(0, -speed, 0));
        }
    }
}
