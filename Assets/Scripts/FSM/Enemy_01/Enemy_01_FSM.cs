using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYFSM;
using CUSTOM_ReadOnly;
using System;

public class Enemy_01_FSM : Character_FSM<ENEMY_01_STATE>
{
    public Enemy_01_FSM()
    {
        Init();
    }

    protected override void Init()
    {
        m_arrState[(int)ENEMY_01_STATE.IDLE] = new Enemy_01_Idle(this);
        m_arrState[(int)ENEMY_01_STATE.WALK] = new Enemy_01_Walk(this);
        m_arrState[(int)ENEMY_01_STATE.ATTACK] = new Enemy_01_Attack(this);
        m_arrState[(int)ENEMY_01_STATE.FOLLOW] = new Enemy_01_Follow(this);
        m_arrState[(int)ENEMY_01_STATE.UNFOLLOW] = new Enemy_01_Unfollow(this);
        m_arrState[(int)ENEMY_01_STATE.DIE] = new Enemy_01_Die(this);

        m_state.SetState(m_arrState[(int)ENEMY_01_STATE.IDLE], this);
    }

    protected override void Begin()
    {
        throw new NotImplementedException();
    }

    protected override void Exit()
    {
        throw new NotImplementedException();
    }

    protected override void Start()
    {
        throw new NotImplementedException();
    }

    protected override void Update()
    {
        throw new NotImplementedException();
    }

    


}
