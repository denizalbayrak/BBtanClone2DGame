using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BlockController : MonoBehaviour
{
    public GameObject Square;
    public GameObject Triangle;
    public GameObject Blocks;
    public GameObject AddBall;
    public GameObject LevelTxt;
    public GameObject MaxScoreTxt;

    public List<GameObject> BlockList = new List<GameObject>();
    public List<GameObject> tempGrid = new List<GameObject>();

    public BallController ballController;
    public ShooterMovement shooterMovement;
    public GridManager gridManager;
    public GameManager gameManager;

    public bool isGameOver;
    public int Level;
    int BlockNum;

    void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        //if (PlayerPrefs.HasKey("level"))
        //{
        //    Level = PlayerPrefs.GetInt("level");
        //    LevelTxt.GetComponent<Text>().text = Level.ToString();
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("level", 0);
        //    LevelTxt.GetComponent<Text>().text = Level.ToString();
        //}

        //TODO: Set PlayerPrefs

        PlayerPrefs.SetInt("level", 0);
        LevelTxt.GetComponent<Text>().text = Level.ToString();

        if (PlayerPrefs.HasKey("MaxScore"))
        {
            MaxScoreTxt.GetComponent<Text>().text = PlayerPrefs.GetInt("MaxScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("MaxScore", 0);
            MaxScoreTxt.GetComponent<Text>().text = PlayerPrefs.GetInt("MaxScore").ToString();
        }
    }
    
    public void RandomBlockNumberCreate()
    {
        int blockNum = Random.Range(1, 7);
        BlockCreate(blockNum);
    }

    void BlockCreate(int blockNum)
    {
        Level++;
        PlayerPrefs.SetInt("level", Level);
        LevelTxt.GetComponent<Text>().text = Level.ToString();
        for (int i = 0; i < blockNum; i++)
        {
            int random = Random.Range(0, 7);
            if (gridManager.GridList[random].gameObject.GetComponent<Grid>().isEmpty)
            {
                GameObject block;
                if (BlockList.Count % 6 == 0)
                {
                    block = Instantiate(Triangle, gridManager.GridList[random].gameObject.transform.position, Quaternion.identity);
                    block.transform.parent = gridManager.GridList[random].transform;
                    block.GetComponent<Block>().isSquare = false;
                }
                else
                {
                    block = Instantiate(Square, gridManager.GridList[random].gameObject.transform.position, Quaternion.identity);
                    block.transform.parent = gridManager.GridList[random].transform;
                    block.GetComponent<Block>().isSquare = true;
                }
                BlockList.Add(block);
                block.GetComponent<Block>().Health = Level;
  
                block.GetComponentInChildren<TextMeshPro>().text = Level.ToString();
                gridManager.GridList[random].gameObject.GetComponent<Grid>().isEmpty = false;

            }

        }
        AddBallPoint();
    }


    void AddBallPoint()
    {
        for (int i = 0; i < 7; i++)
        {
            if (gridManager.GridList[i].gameObject.GetComponent<Grid>().isEmpty)
            {
                tempGrid.Add(gridManager.GridList[i].gameObject);
            }
        }
        int random = Random.Range(0, tempGrid.Count);
        GameObject addBall = Instantiate(AddBall, tempGrid[random].gameObject.transform.position, Quaternion.identity);
        tempGrid[random].gameObject.GetComponent<Grid>().isEmpty = false;
        addBall.transform.parent = tempGrid[random].transform;
        tempGrid.Clear();
        GrillCleaner();

    }

    public void GrillCleaner()
    {
        for (int i = gridManager.GridList.Count - 1; i >= 0; i--)
        {
            if (gridManager.GridList[i] != null && !gridManager.GridList[i].gameObject.GetComponent<Grid>().isEmpty && gridManager.GridList[i].transform.childCount >= 1)
            {
                if (i + 7 >= 63)
                {
                    gameManager.GameOver();
                    GameOver();
                    break;
                }
                else
                {
                    gridManager.GridList[i].transform.GetChild(0).gameObject.transform.DOMove(gridManager.GridList[i + 7].transform.position, 0.25f);
                    gridManager.GridList[i].transform.GetChild(0).gameObject.transform.parent = gridManager.GridList[i + 7].transform;
                    gridManager.GridList[i + 7].gameObject.GetComponent<Grid>().isEmpty = false;
                    
                }

            }
        }
       
        for (int i = 0; i < 7; i++)
        {
            gridManager.GridList[i].gameObject.GetComponent<Grid>().isEmpty = true;
        }
        StartCoroutine(touchEnable());
    }

    public IEnumerator touchEnable()
    {
        shooterMovement.BallsDown = false;
        yield return new WaitForSeconds(0.25f);
        shooterMovement.BallsDown = true;
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            Level = 0;
            shooterMovement.BallsDown = false;
            isGameOver = true;
            GameOverGridCleaner();
            StartGame();
            if (Level > PlayerPrefs.GetInt("MaxScore"))
            {
                PlayerPrefs.SetInt("MaxScore", Level);
            }
        }
    }

    void GameOverGridCleaner()
    {
        for (int i = 0; i < gridManager.GridList.Count; i++)
        {
            if (gridManager.GridList[i].transform.childCount >= 1)
            {
                Destroy(gridManager.GridList[i].transform.GetChild(0).gameObject);
            }
            gridManager.GridList[i].gameObject.GetComponent<Grid>().isEmpty = true;
        }
    }
}
