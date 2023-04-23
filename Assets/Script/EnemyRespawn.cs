using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Delete this later.
public class EnemyRespawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Player player;
    private Enemy _enemy;
    private bool _isRespawning;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount > 0) return;
        if(_isRespawning) return;
        StartCoroutine(Respawn());
    }
    
    public IEnumerator Respawn()
    {
        _isRespawning = true;
        yield return new WaitForSeconds(1);
        Instantiate(enemy, transform);
        _isRespawning = false;
    }
}
