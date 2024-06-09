using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    //Cannon Controls
    private Vector2 CannonPosition;
    private Vector2 MousePosition;
    private Vector2 direction;

    //Cannon Bulllet fire
    public GameObject cannonBall;
    public float fireForce;
    public Transform firePoint;

    //trajectoryPoints
    public GameObject point;
    public GameObject[] points;
    public int numofPoints;
    public float spaceBetweenPoints;
    // Start is called before the first frame update
    void Start()
    {
        // for every point instantiate base on the point and firepoint pos

        points = new GameObject[numofPoints];
        for (int i = 0; i < numofPoints; i++) 
        {
            points[i] = Instantiate(point,firePoint.position,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        CannonRotate();

        Vector2 PointPosition(float p)
        {
            Vector2 position = ((Vector2)firePoint.position + direction.normalized * fireForce * p) + 0.5f * Physics2D.gravity * (p * p); // Returns of the position of the points 
            return position;
        }
        for (int i = 0; i < numofPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints); //Adds interval between the points
        }
    }

    public void CannonRotate()
    {
        // cannon rotation using mousepos input

        CannonPosition = transform.position;
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = MousePosition - CannonPosition;
        transform.right = direction;
    }

    public void Fire() 
    {
        GameObject _cannonBall = Instantiate(cannonBall, firePoint.position,firePoint.rotation);
        _cannonBall.GetComponent<Rigidbody2D>().velocity = transform.right * fireForce;
    }
}
