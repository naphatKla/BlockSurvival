using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Player player;
    private Enemy _enemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0) return;
        Instantiate(enemy,transform);
    }
}
