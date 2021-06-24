using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float follow;
    public float smoothness;

    private Vector3 _targetPos;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _targetPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

        if (target.transform.localScale.x > 0f)
        {
            _targetPos = new Vector3(_targetPos.x + follow, _targetPos.y, _targetPos.z);
        }
        else
        {
            _targetPos = new Vector3(_targetPos.x - follow, _targetPos.y, _targetPos.z);
        }

        //transform.position = _targetPos;
        transform.position = Vector3.Lerp(transform.position, _targetPos, smoothness * Time.deltaTime);
    }
}
