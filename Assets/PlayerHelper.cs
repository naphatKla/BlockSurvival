using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.position = _player.transform.position;
        transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
    }
}
