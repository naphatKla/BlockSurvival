using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    #region Declare Variables
    
    [SerializeField] private Player player;
    [SerializeField] private GameObject bullet;
    [SerializeField] public float bulletSpeedAttack;
    [SerializeField] private float bulletOffSetScale;
    private float _currentCooldown;

    #endregion
    
    #region Unity Method
    
    void Update()
    {
        
        if (_currentCooldown == 0)
        {
            BulletSpawn();
            _currentCooldown += 1;
        }
        PlayerRotateOnMouseCursor();
        _currentCooldown -= Time.deltaTime * bulletSpeedAttack;
        if (_currentCooldown < 0)
        {
            _currentCooldown = 0;
        }
    }
    #endregion

    
    #region Method
    private void BulletSpawn()
    {
        if (player.playerLevel == 0)
        {
            BulletDeafultGunPatternSpawn();
        }

        if (player.playerLevel >= 1)
        {
            BulletShotGunPatternSpawn();
            
        }
    }
    
    private void BulletDeafultGunPatternSpawn()
    {
        Vector3 bulletOffSet = transform.up * bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(bullet, transform.position + bulletOffSet, transform.rotation);
        Destroy(bulletSpawn,0.5f);
    }

    private void BulletShotGunPatternSpawn()
    {
        Vector3 bulletOffSet = transform.up * bulletOffSetScale;
        float angle = 30;

        for (int i = 0; i < 3; i++)
        {
            GameObject bulletSpawn = Instantiate(bullet, transform.position + bulletOffSet, transform.rotation * Quaternion.Euler(0,0,angle));
            Destroy(bulletSpawn,0.5f);
            angle -= 30;
        }
    }
    
    private void PlayerRotateOnMouseCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = mousePosition - transform.position;
        transform.up = direction;
    }
    #endregion
}
