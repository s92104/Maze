using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour {
    float x, y;
    public float movespeed = 2;
    public float mousespeed = 3;
    public Camera main;
    public Camera second;
    public float distance;
    public Vector3 cameraFix;
    public GameObject m_character;
    public GameObject mini_character;
    public GameObject maxmap;
    public GameObject point;
    public Text text_score;
    public ParticleSystem pickup;
    static public int score;
    public Animator m_animator;
    GameObject dir;
    bool firstperson = true;
	// Use this for initialization
	void Start () {
        //初始化
        dir = new GameObject();
        dir.name = "dir";
        score = 0;
        m_animator = GetComponent<Animator>();
        //隱藏滑鼠
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        //開啟地圖
        if (Input.GetKeyDown(KeyCode.M))
        {
            maxmap.SetActive(!maxmap.activeInHierarchy);
            point.SetActive(!point.activeInHierarchy);    
        }
        //切換視角
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (firstperson)
            {
                // Camera.main.transform.position -= dir.transform.forward;
                //Camera.main.transform.position += new Vector3(0, 1, 0);
                main.gameObject.SetActive(false);
                second.gameObject.SetActive(true);
                firstperson = false;
            }
            else
            {
                //Camera.main.transform.localPosition= new Vector3(0, 0.5f, -0.5f);
                main.gameObject.SetActive(true);
                second.gameObject.SetActive(false);
                firstperson = true;
            }
        }
        //滑鼠互動
        if(Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit = new RaycastHit();
            Ray mouseray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(mouseray, out hit,Mathf.Infinity,2048)) 
            {
                if(hit.transform.tag=="Item")
                {
                    score += 20;
                    text_score.text = "Score:" + score.ToString();
                    Instantiate<ParticleSystem>(pickup, new Vector3(hit.transform.position.x, 0.4f, hit.transform.position.z), new Quaternion());
                    hit.transform.gameObject.SetActive(false);
                }
            }            
        }
    }

    void FixedUpdate()
    {
        //取得滑鼠位移
        x += Input.GetAxis("Mouse X") * mousespeed;
        y += Input.GetAxis("Mouse Y") * mousespeed;
        //保證在360
        if (x > 360)
            x -= 360;
        else if (x < 0)
            x += 360;      
        //旋轉相機
        Camera.main.transform.rotation = Quaternion.Euler(-y, x, 0);             
        //旋轉人物
        if (!firstperson)
        {
            m_character.SetActive(true); //顯示人物
            this.transform.rotation = Quaternion.Euler(0, x, 0);
            Camera.main.transform.position = transform.position + Quaternion.Euler(-y, x, 0) * new Vector3(0, 0, -distance) + cameraFix; 
        }
        else
            m_character.SetActive(false);
        //旋轉小地圖人物         
        mini_character.transform.rotation = Quaternion.Euler(0, x, 0);
        //取得現在方向
        dir.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        //移動相機
        CharacterController character = GetComponent<CharacterController>();
        if (Input.GetKey(KeyCode.W))
            character.SimpleMove(dir.transform.forward * movespeed);
        if (Input.GetKey(KeyCode.S))
            character.SimpleMove(-dir.transform.forward * movespeed);
        if (Input.GetKey(KeyCode.A))
            character.SimpleMove(-dir.transform.right * movespeed);
        if (Input.GetKey(KeyCode.D))
            character.SimpleMove(dir.transform.right * movespeed);       
        //控制動畫
        //走路
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            m_animator.SetFloat("Speed", 1);
        //停止走路
        else
            m_animator.SetFloat("Speed", 0);
        //攻擊
        if (Input.GetMouseButtonDown(0))
            m_animator.SetTrigger("Attack");   
    }
}
