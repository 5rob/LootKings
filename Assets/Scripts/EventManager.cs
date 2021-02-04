using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnEnemyKilled;
    public void EnemyKilled()
    {
        if(OnEnemyKilled!=null)
        {
            OnEnemyKilled();
        }
    }

    public event Action OnAttackBegin;
    public void AttackBegin()
    {
        OnAttackBegin?.Invoke();
    }

    public event Action OnAttackEnd;
    public void AttackEnd()
    {
        if (OnAttackEnd != null)
        {
            OnAttackEnd();
        }
    }


}
