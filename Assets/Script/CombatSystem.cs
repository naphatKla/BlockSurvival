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
    [Header("Bullet Type")]
    [SerializeField] public GameObject bullet;
    [SerializeField] private GameObject smallBullet;
    [SerializeField] public GameObject bigBullet;
    [SerializeField] public GameObject veryBigBullet;
    [SerializeField] public GameObject explode;
    [SerializeField] private float bulletOffSetScale;
    public bool isGunTypeAssaultRifle;
    public bool isGunTypeShotgun;
    public bool isGunTypeSniper;
    public bool isGunTypeMissile;
    private Player _player;
    private Level _level;
    public float bulletSpeed;
    public float missileDamage;

    #endregion
    
    #region Unity Method
    void Start()
    {
        bulletSpeed = bullet.GetComponent<Bullet>().bulletSpeed;
        missileDamage = explode.GetComponent<Explode>().explodeDamage;
        _player = GetComponent<Player>();
        _level = GetComponent<Level>();
        Invoke("BulletSpawn", _player.playerAttackSpeed);
    }
    void Update()
    {
        
    }
    #endregion

    
    #region Method
    private void BulletSpawn()
    {
        if (_player.playerLevel >= 0 && !isGunTypeShotgun && !isGunTypeAssaultRifle && !isGunTypeSniper && !isGunTypeMissile)
        {
            BulletDefaultGunPatternSpawn();
            //BulletDefaultGunPatternSpawn();
        }

        if (_level.playerLevel >= 5 && isGunTypeShotgun)
        {
            BulletShotGunPatternSpawn();
        }
        
        if (_level.playerLevel >= 5 && isGunTypeAssaultRifle)
        {
            BulletAssaultRifleGunPatternSpawn();
        }
        
        if (_level.playerLevel >= 5 && isGunTypeSniper)
        {
            BulletSniperGunPatternSpawn();
        }

        if (_level.playerLevel >= 5 && isGunTypeMissile)
        {
            BulletMissileGunPatternSpawn();
        }
        
        Invoke("BulletSpawn", _player.playerAttackSpeed);
    }
    
    private void BulletDefaultGunPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(bullet, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        Destroy(bulletSpawn,0.7f);
    }

    private void BulletShotGunPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * bulletOffSetScale;
        float angle = 30;

        for (int i = 0; i < 3; i++)
        {
            GameObject bulletSpawn = Instantiate(bigBullet, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation * Quaternion.Euler(0,0,angle));
            Destroy(bulletSpawn,0.3f);
            angle -= 30;
        }
    }
    
    private void BulletAssaultRifleGunPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(smallBullet, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        Destroy(bulletSpawn,0.5f);
    }
    
    private void BulletSniperGunPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(veryBigBullet, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        bulletSpawn.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        Destroy(bulletSpawn,1f);
    }
    
    private void BulletMissileGunPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(bigBullet, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        bulletSpawn.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        Destroy(bulletSpawn,1f);
    }
    

    #endregion
}
