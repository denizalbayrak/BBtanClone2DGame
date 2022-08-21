using UnityEngine;
using System.Collections;

namespace UnityLibrary
{
    public class ScreenEdgeColliders : MonoBehaviour
    {

        public GameObject Left;
        public GameObject Right;
        void Awake()
        {
            AddCollider();
        }

        void AddCollider()
        {         

            var cam = Camera.main;
           

            var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
            var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
            var edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

            Instantiate(Left, new Vector2(topLeft.x, topLeft.y + bottomLeft.y), Quaternion.Euler(0, 0, 88));
            Instantiate(Right, new Vector2(topRight.x, topRight.y + bottomRight.y), Quaternion.Euler(0, 0, -88));
            var edgePoints = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
            edge.points = edgePoints;
        }
    }
}