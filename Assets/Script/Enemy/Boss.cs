using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
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
            if (Random.Range(0, 101) >= 50)
            {
                float time = 5f;
                float timeCount = 0;
                Debug.Log("Boss Skill");

                while (timeCount < time)
                {
                    timeCount += Time.deltaTime;
                    if (timeCount < 2.5)
                    {
                        _currentSpeed = 0;
                    }
                    else
                    {
                        _currentSpeed = 30f;
                    }
                    yield return null;
                }
            }
        }
    }
}
