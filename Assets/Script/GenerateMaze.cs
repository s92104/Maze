using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour {
    public GameObject WallType;
    public int width = 10;
    public int height = 10; //地圖的長寬

    class Cell //地板的狀態
    {
        public int x;
        public int y;
        public bool[] wall= {true,true,true,true};
        public bool walk = false;
    }
    Cell[][] cell;
	// Use this for initialization
	void Start () {      
        //產生迷宮資料
        CreateMaze();
        //畫出迷宮
        DrawMaze();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateMaze()
    {
        //初始化
        cell = new Cell[height][];
        for (int i = 0; i < height; i++)
        {
            cell[i] = new Cell[width];
            for (int j = 0; j < width; j++)
            {
                cell[i][j] = new Cell();
                cell[i][j].x = j;
                cell[i][j].y = i;
            }
        }
        //建立路徑
        CreatePath(cell[0][0]);
    }
    
    void CreatePath(Cell currentCell)
    {
        cell[currentCell.y][currentCell.x].walk = true;

        List<int> direction=new List<int>(); //存可以走的方向
        if (currentCell.y < height - 1) direction.Add(0); //上
        if (currentCell.x < width - 1) direction.Add(1); //右
        if (currentCell.y > 0) direction.Add(2); //下
        if (currentCell.x > 0) direction.Add(3); //左

        int count = direction.Count;
        for (int i = 0; i < count; i++) //檢查有沒有走過
        {
            int dir = direction[Random.Range(0, direction.Count)];
            direction.Remove(dir);
            switch (dir)
            {
                case 0: if (!cell[currentCell.y + 1][currentCell.x].walk)
                    { cell[currentCell.y][currentCell.x].wall[0] = false; cell[currentCell.y + 1][currentCell.x].wall[2] = false; CreatePath(cell[currentCell.y + 1][currentCell.x]); } break;
                case 1: if (!cell[currentCell.y][currentCell.x + 1].walk)
                    { cell[currentCell.y][currentCell.x].wall[1] = false; cell[currentCell.y][currentCell.x + 1].wall[3] = false; CreatePath(cell[currentCell.y][currentCell.x + 1]); } break;
                case 2: if (!cell[currentCell.y - 1][currentCell.x].walk)
                    { cell[currentCell.y][currentCell.x].wall[2] = false; cell[currentCell.y - 1][currentCell.x].wall[0] = false; CreatePath(cell[currentCell.y - 1][currentCell.x]); } break;
                case 3: if (!cell[currentCell.y][currentCell.x - 1].walk)
                    { cell[currentCell.y][currentCell.x].wall[3] = false; cell[currentCell.y][currentCell.x - 1].wall[1] = false; CreatePath(cell[currentCell.y][currentCell.x - 1]); } break;
            }
        }    
    }

    void DrawMaze()
    {
        float sizeX = WallType.GetComponent<BoxCollider>().size.x * WallType.transform.localScale.x; //牆壁長度
        float sizeY = WallType.GetComponent<BoxCollider>().size.y * WallType.transform.localScale.y; //牆壁高度
        GameObject tmp;
        GameObject maze = GameObject.Find("Maze");
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {             
                if(i==0 && cell[i][j].wall[2]) //底部牆壁
                {
                    tmp = Instantiate(WallType, new Vector3((cell[i][j].x + 0.5f) * sizeX, sizeY*0.5f, cell[i][j].y * sizeX), new Quaternion());
                    tmp.transform.parent = maze.transform;
                }
                if (cell[i][j].wall[0])
                {
                    tmp = Instantiate(WallType, new Vector3((cell[i][j].x + 0.5f) * sizeX, sizeY * 0.5f, (cell[i][j].y + 1) * sizeX), new Quaternion());
                    tmp.transform.parent = maze.transform;
                }
                if (cell[i][j].wall[1])
                {
                    tmp = Instantiate(WallType, new Vector3((cell[i][j].x + 1) * sizeX, sizeY * 0.5f, (cell[i][j].y + 0.5f) * sizeX), Quaternion.Euler(new Vector3(0, 90, 0)));
                    tmp.transform.parent = maze.transform;
                }
                if (cell[i][j].wall[3])
                {
                    tmp = Instantiate(WallType, new Vector3(cell[i][j].x * sizeX, sizeY * 0.5f, (cell[i][j].y + 0.5f) * sizeX), Quaternion.Euler(new Vector3(0, 90, 0)));
                    tmp.transform.parent = maze.transform;
                }
            }
        }
    }
}
