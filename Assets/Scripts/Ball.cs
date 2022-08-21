using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(visibleStart());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {

            if (collision.gameObject.GetComponent<Block>().Health - 1 == 0)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<Block>().Health--;
                collision.gameObject.GetComponentInChildren<TextMeshPro>().text = collision.gameObject.GetComponent<Block>().Health.ToString();
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AddBallPowerUp")
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);   
            BallController ballController = FindObjectOfType<BallController>();
            ballController.AddBallPowerUp();

        }

    }
    IEnumerator visibleStart()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public IEnumerator DestroyItself()
    {
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
}
