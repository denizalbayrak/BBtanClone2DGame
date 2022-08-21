using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMovement : MonoBehaviour
{
    #region Scripts
    public BaseController baseController;
    public BallController ballController;
    #endregion
    public Vector3 shooterStartPos;
    public LineRenderer lr;
    public float moveSpeed = 4;

    public bool BallsDown;

    private void Start()
    {
        shooterStartPos = transform.position;
    }
    void Update()
    {
        if (BallsDown)
        {          

            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                lr.enabled = true;
                if (Physics.Raycast(transform.position, transform.up, out hit))
                {
                    if (hit.collider)
                    {
                        lr.SetPosition(1, hit.point);

                    }
                }
                else
                {
                    lr.SetPosition(1, transform.up * 50);
                }
                if (transform.rotation.z > -0.65f && transform.rotation.z < 0.65f)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    transform.Rotate(0, 0, -touchDeltaPosition.x * moveSpeed * Time.deltaTime);
                }

                else
                {
                    if (transform.rotation.z < -0.65f)
                    {
                        Quaternion rotation = Quaternion.Euler(0, 0, -63f);
                        transform.rotation = rotation;
                    }

                    if (transform.rotation.z > 0.65f)
                    {
                        Quaternion rotation = Quaternion.Euler(0, 0, 65f);
                        transform.rotation = rotation;

                    }
                }
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                lr.enabled = false;
                StartCoroutine(ballController.BallShoot());
                baseController.falledBallsCount = 0;
                BallsDown = false;
            }
        }
        }
    }
