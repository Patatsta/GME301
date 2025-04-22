using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{

    private Vector3 _targetPos;
    private float _moveSpeed = 5f; 
    private bool _isWalking = false;

    private void OnEnable()
    {
        PointerScript.OnPointerMove += WalkToPoint;
    }
    private void OnDisable()
    {
        PointerScript.OnPointerMove -= WalkToPoint;
    }
   
    private void WalkToPoint(Vector3 point)
    {
        _targetPos = point;
        _isWalking = true;

    }



    private void Update()
    {
        if (_isWalking)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);

            if (transform.position == _targetPos)
            {
                _isWalking = false;
            }
        }
    }
}
