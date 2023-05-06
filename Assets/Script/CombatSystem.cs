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
    [SerializeField] public Bullet bullet;
    [SerializeField] private Bullet assaultRifleBullet;
    [SerializeField] public Bullet shotgunBullet;
    [SerializeField] public Bullet sniperBullet;
    [SerializeField] public Bullet missileBullet;
    [SerializeField] public Bullet sword;
    [SerializeField] public GameObject explode;
    public PlayerClass playerClass;
    
 
    private Player _player;
    public float bulletSpeed;
    public Vector3 explodeScale;
    public float missileDamage;
    
    public enum PlayerClass
    {
        Default,
        AssaultRifle,
        Shotgun,
        Sniper,
        Missile,
        Sword
    }

    #endregion
    
    #region Unity Method
    void Start()
    {
        bulletSpeed = bullet.GetComponent<Bullet>().bulletSpeed;
        explodeScale = explode.GetComponent<Explode>().transform.localScale;
        missileDamage = explode.GetComponent<Explode>().explodeDamage;
        _player = GetComponent<Player>();
        playerClass = PlayerClass.Default;
        Invoke("BulletSpawn", _player.playerAttackSpeed);
    }
    void Update()
    {
        
    }
    #endregion

    
    #region Method
    private void BulletSpawn()
    {
        switch (playerClass)
        {
            case PlayerClass.Default:
                BulletDefaultPatternSpawn();
                break;
            case PlayerClass.Shotgun:
                BulletShotGunPatternSpawn();
                break;
            case PlayerClass.AssaultRifle:
                BulletAssaultRiflePatternSpawn();
                break;
            case PlayerClass.Sniper:
                BulletSniperPatternSpawn();
                break;
            case PlayerClass.Missile:
                BulletMissilePatternSpawn();
                break;
            case PlayerClass.Sword:
                BulletSwordPatternSpawn();
                break;
        }

        Invoke("BulletSpawn", _player.playerAttackSpeed);
    }
    
    private void BulletDefaultPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * bullet.bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(bullet.gameObject, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        bulletSpawn.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
    }

    private void BulletShotGunPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * shotgunBullet.bulletOffSetScale;
        float angle = 30;

        for (int i = 0; i < 3; i++)
        {
            GameObject bulletSpawn = Instantiate(shotgunBullet.gameObject, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation * Quaternion.Euler(0,0,angle));
            angle -= 30;
            bulletSpawn.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        }
    }
    private void BulletAssaultRiflePatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * assaultRifleBullet.bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(assaultRifleBullet.gameObject, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        bulletSpawn.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
    }
    
    private void BulletSniperPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * sniperBullet.bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(sniperBullet.gameObject, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        bulletSpawn.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
    }
    
    private void BulletMissilePatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * missileBullet.bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(shotgunBullet.gameObject, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation);
        bulletSpawn.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
    }
    
    private void BulletSwordPatternSpawn()
    {
        Vector3 bulletOffSet = _player.playerTransform.up * sword.bulletOffSetScale;
        float angle = 75;

        for (int i = 0; i < 3; i++)
        {
            GameObject bulletSpawn = Instantiate(sword.gameObject, _player.playerTransform.position + bulletOffSet, _player.playerTransform.rotation * Quaternion.Euler(0,0,angle));
            bulletSpawn.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            Destroy(bulletSpawn,0.2f);
            angle -= 75;
        }
    }
    #endregion
}
