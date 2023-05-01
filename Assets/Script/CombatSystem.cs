using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class CombatSystem : MonoBehaviour
{
    #region Declare Variables
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletOffSetScale;
    public bool isGunTypeAssaultRifle;
    public bool isGunTypeShotgun;
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
        if (_player.playerLevel >= 0 && !isGunTypeShotgun && !isGunTypeAssaultRifle)
        {
            BulletDefaultGunPatternSpawn();
            //BulletDefaultGunPatternSpawn();
        }

        if (_player.playerLevel >= 5 && isGunTypeShotgun)
        {
            BulletShotGunPatternSpawn();
        }
        
        if (_player.playerLevel >= 5 && isGunTypeAssaultRifle)
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
    
    private void BulletAssaultRifleGunPatternSpawn()
    {
        _player._playerMaxAttackSpeed = 0.05f;
        _player.playerAttackSpeed -= 0.2f;
        Vector3 bulletOffSet = _player.playerTransform.up * bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(bullet, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        Destroy(bulletSpawn,0.5f);
    }
    


    #endregion
}
