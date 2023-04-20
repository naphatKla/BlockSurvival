using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeedAttack;

    [SerializeField] private float offSetScale;
    
    
    
    
    void Start()
    {
        InvokeRepeating("BulletSpawn",0,bulletSpeedAttack);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRoatateOnMouseCursor();
    }

    private void BulletSpawn()
    {
        Vector3 bulletOffSet = transform.up * offSetScale;
        
        GameObject bulletSpawn = Instantiate(bullet, transform.position + bulletOffSet, transform.rotation);
        Destroy(bulletSpawn,0.5f);
    }
    
    private void PlayerRoatateOnMouseCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = mousePosition - transform.position;
        transform.up = direction;
        
    }
    
    
    
    
}
