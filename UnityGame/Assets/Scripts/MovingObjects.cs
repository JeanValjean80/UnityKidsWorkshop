using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    public GameObject movingObject;
    public Transform startPoint;
    public Transform stopPoint;

    public float speed;


    private Vector3 _curTarget;



    // Start is called before the first frame update
    void Start()
    {
        _curTarget = stopPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, _curTarget, speed * Time.deltaTime);

        if (movingObject.transform.position == stopPoint.position)
        {
            _curTarget = startPoint.position;
        }

        if (movingObject.transform.position == startPoint.position)
        {
            _curTarget = stopPoint.position;
        }
    }
}
