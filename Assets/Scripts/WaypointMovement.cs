using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isLooping;

    private int _currentPointIndex;
    private bool _isMoving = true;

    private void Update()
    {
        if (_isMoving)
        {
            Move(); 
        }
    }

    private void Move()
    {
        var nextPosition = _points[_currentPointIndex].position;

        transform.position =  Vector3.MoveTowards(transform.position, nextPosition, _speed * Time.deltaTime);

        if (transform.position == nextPosition)
        {
            _currentPointIndex++;

            if (_currentPointIndex >= _points.Length)
            {
                _currentPointIndex = 0;

                if (!_isLooping)
                {
                    _isMoving = false;
                }
            }
        }
    }
}
