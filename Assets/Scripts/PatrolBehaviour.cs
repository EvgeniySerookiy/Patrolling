using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class PatrolBehaviour : MonoBehaviour
{ 
    [SerializeField] private GameObject _spherePrefab;
    [SerializeField] private int _count;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _pauseTimePosition;
    
    private float _pauseTime;
    
    private Transform[] _sphereArray;
    
    private int _currentIndex = 1;
    
    
    private void Start()
    {
        _sphereArray = new Transform[_count];
        for (var i = 0; i < _count; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(20f, -20f), 0.5f, Random.Range(20f, -20f));
            var sphereInstance = Instantiate(_spherePrefab, randomPosition, Quaternion.identity);
            _sphereArray[i] = sphereInstance.transform;
            
            if (i == 0)
            {
                transform.position = randomPosition;
            }
        }
    }

    private void Update()
    {
        if (_pauseTime > 0f)
        {
            _pauseTime -= Time.deltaTime;
            return;
        }
        
        var travelDistance = _movementSpeed * Time.deltaTime;
        
        transform.LookAt(_sphereArray[_currentIndex].position);
        if (Vector3.Distance(transform.position, _sphereArray[_currentIndex].position) <= travelDistance)
        {
            _pauseTime = _pauseTimePosition;
            
            if (_currentIndex == _count - 1)
            {
                _currentIndex = 0;
                return;
            }
            _currentIndex++;
        }
        transform.position = Vector3.MoveTowards(transform.position, _sphereArray[_currentIndex].position, travelDistance);
        
    }
}
