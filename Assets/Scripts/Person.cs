﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HappinessState
{
    Happy,
    Neutral,
    Mad
}

public class Person : MonoBehaviour
{
    public Sprite happy;
    public Sprite neutral;
    public Sprite mad;
    
    private Queue _enterance;

    private SpriteRenderer _spriteRenderer;
    private HappinessState currentHappiness = HappinessState.Happy;

    private Queue<Vector3> _movingPositions;

    private void Awake()
    {
        _movingPositions = new Queue<Vector3>();
        _enterance = GameObject.Find("Queue").GetComponent<Queue>();
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
        }
    }

    private bool _isMoving = false;
    public float movementSpeed = 5f;

    private void Start()
    {
        StartCoroutine(HappinessTimer());
    }

    private IEnumerator HappinessTimer()
    {
        yield return new WaitForSeconds(5f);
        switch (currentHappiness)
        {
            case HappinessState.Happy:
                currentHappiness = HappinessState.Neutral;
                _spriteRenderer.sprite = neutral;
                break;
            case HappinessState.Neutral:
                currentHappiness = HappinessState.Mad;
                _spriteRenderer.sprite = mad;
                break;
            case HappinessState.Mad:
                Destroy(gameObject);
                _enterance.RemovePerson(this);
                break;
            default:
                break;
        }
        StartCoroutine(HappinessTimer());
    }

    private void Update()
    {
        if (_isMoving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _movingPositions.Peek(), movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _movingPositions.Peek()) < 0.001f)
            {
                _movingPositions.Dequeue();
            }

            if (_movingPositions.Count == 0)
            {
                _isMoving = false;
            }
        }
    }

    public void MoveTo(Vector3 pos)
    {
        _movingPositions.Enqueue(pos);
        isMoving = true;
    }

    public void SetHappiness(HappinessState happiness)
    {
        switch (happiness)
        {
            case HappinessState.Happy:
                _spriteRenderer.sprite = happy;
                break;
            case HappinessState.Neutral:
                _spriteRenderer.sprite = neutral;
                break;
            case HappinessState.Mad:
                _spriteRenderer.sprite = mad;
                break;
            default:
                break;
        }
    }
}