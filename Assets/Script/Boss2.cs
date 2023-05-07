using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Enemy
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(RandomSkill());

    }

    protected override void Update()
    {
        base.Update();

    }

    private IEnumerator RandomSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            
            float random = Random.Range(0, 101);

            if (random >= 75)
            {
                float angle = 36;

                for (int i = 0; i < 10; i++)
                {
                    Instantiate(bulletPrefab, transform.position , transform.rotation * Quaternion.Euler(0,0,angle));
                    angle -= 36;
                }
            }
            else if (random >= 50)
            {
                float time = 5f;
                float timeCount = 0;
                float originalFireRate = fireRate;

                fireRate /= 4f;
                yield return new WaitForSeconds(3f);
                fireRate = originalFireRate;
            }
        }
    }
}
