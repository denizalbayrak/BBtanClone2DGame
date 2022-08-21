using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseController : MonoBehaviour
{

    public ShooterMovement shooterMovement;
    public BallController ballController;
    public BlockController blockController;

    public bool isShooted;
    public int falled;
    public GameObject Shooter;
    Vector3 firstCollisionPos;
    public int falledBallsCount;
    public bool newLevelCheck = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ball")
        {
            falledBallsCount++;
            if (falledBallsCount == 1)
            {
                firstCollisionPos = collision.transform.position;
                Shooter.transform.DORotate(new Vector3(0, 0, 0), 0.1f);
                Shooter.transform.DOMoveX(collision.transform.position.x, 0.25f);
            }
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            collision.gameObject.transform.DOMoveX(firstCollisionPos.x, 0.2f);

            StartCoroutine(DestroyBall(collision.gameObject));
        }
    }

    IEnumerator DestroyBall(GameObject ball)
    {
        yield return new WaitForSeconds(0.2f);
        ball.SetActive(false);
        StartCoroutine(ball.GetComponent<Ball>().DestroyItself());

        if (falledBallsCount == ballController.ballCount - ballController.AddBallPowerUpNum && !newLevelCheck)
        {
            newLevelCheck = true;
            StartCoroutine(NewLevel());
        }


    }

    IEnumerator NewLevel()
    {
        ballController.AddBallPowerUpNum = 0;
        falledBallsCount = 0;
        yield return new WaitForSeconds(0.2f);
        blockController.RandomBlockNumberCreate();
        newLevelCheck = false;
       
        
    }
}
