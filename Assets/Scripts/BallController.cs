using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject ballStartPos;
    public GameObject shooter;
    public int ballCount;
    public int AddBallPowerUpNum = 0;
    public List<GameObject> BallList = new List<GameObject>();

    public void BallCreator()
    {
        ballCount++;
    }

    public IEnumerator BallShoot()
    {
        BallListClear();
        for (int i = 0; i < ballCount; i++)
        {
            GameObject ball = Instantiate(Ball, ballStartPos.transform.position, Quaternion.identity);
            BallList.Add(ball);
            ball.GetComponent<Rigidbody2D>().AddForce(shooter.transform.up * 500.0f);
            yield return new WaitForSeconds(0.15f);

        }
    }
    void BallListClear()
    {
        for (int i = 0; i < BallList.Count; i++)
        {
            Destroy(BallList[i].gameObject);
        }
        BallList.Clear();
    }
    public void AddBallPowerUp()
    {
        ballCount++;
        AddBallPowerUpNum++;
        PlayerPrefs.SetInt("ballCount", ballCount);
        PlayerPrefs.SetInt("AddBallPowerUpNum", AddBallPowerUpNum);
    }
}


