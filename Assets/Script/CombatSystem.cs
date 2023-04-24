using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    #region Declare Variables
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletOffSetScale;
    private Player _player;
    #endregion
    
    #region Unity Method
    void Start()
    {
        _player = GetComponent<Player>();
        Invoke("BulletSpawn", _player.playerAttackSpeed);
    }
    void Update()
    {

    }
    #endregion

    
    #region Method
    private void BulletSpawn()
    {
        if (_player.playerLevel >= 0)
        {
            BulletShotGunPatternSpawn();
            //BulletDefaultGunPatternSpawn();
        }

        if (_player.playerLevel >= 5)
        {
            BulletShotGunPatternSpawn();
        }
        
        Invoke("BulletSpawn", _player.playerAttackSpeed);
    }
    
    private void BulletDefaultGunPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(bullet, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        Destroy(bulletSpawn,0.5f);
    }

    private void BulletShotGunPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * bulletOffSetScale;
        float angle = 30;

        for (int i = 0; i < 3; i++)
        {
            GameObject bulletSpawn = Instantiate(bullet, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation * Quaternion.Euler(0,0,angle));
            Destroy(bulletSpawn,0.5f);
            angle -= 30;
        }
    }
    #endregion
}
