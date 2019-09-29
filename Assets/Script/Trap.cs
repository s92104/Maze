using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public GameObject trapType;
    public float speed = 0.05f;
    public enum dir { up, down, left, right };
    public dir direction;
    GameObject end;
    GameObject trap;
    
    // Use this for initialization
    void Start()
    {
        end = new GameObject();
        Vector3 rayDir=new Vector3();
        switch (direction)
        {
            case dir.up: rayDir = Vector3.forward;break;
            case dir.down: rayDir = Vector3.back; break;
            case dir.left: rayDir = Vector3.left; break;
            case dir.right: rayDir = Vector3.right; break;
        }
        RaycastHit hit=new RaycastHit();
        Physics.Raycast(this.transform.position, rayDir,out hit);
        end.transform.position = hit.transform.position;
        trap = Instantiate(trapType, this.transform.position, new Quaternion()); //在牆壁產生物件       
    }

    // Update is called once per frame
    void Update()
    {       
        //移動
        switch (direction)
        {
            case dir.up: trap.transform.Translate(Vector3.forward * speed, Space.World);if (trap.transform.position.z > end.transform.position.z) trap.transform.position = this.transform.position; break;
            case dir.down: trap.transform.Translate(Vector3.back * speed, Space.World); if (trap.transform.position.z < end.transform.position.z) trap.transform.position = this.transform.position; break;
            case dir.left: trap.transform.Translate(Vector3.left * speed, Space.World); if (trap.transform.position.x < end.transform.position.x) trap.transform.position = this.transform.position; break;
            case dir.right: trap.transform.Translate(Vector3.right * speed, Space.World); if (trap.transform.position.x > end.transform.position.x) trap.transform.position = this.transform.position; break;
        }
    }

}
