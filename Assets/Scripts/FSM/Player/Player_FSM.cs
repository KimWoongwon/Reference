using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYFSM;
using CUSTOM_ReadOnly;
using System;

public class Player_FSM : Character_FSM<PLAYER_STATE>
{
    public Player_FSM()
    {
        Init();
    }

    protected override void Init()
    {
        m_arrState[(int)PLAYER_STATE.IDLE] = new Player_Idle(this);
        m_arrState[(int)PLAYER_STATE.WALK] = new Player_Walk(this);
        m_arrState[(int)PLAYER_STATE.ATTACK] = new Player_Attack(this);
        m_arrState[(int)PLAYER_STATE.DIE] = new Player_Die(this);

        m_state.SetState(m_arrState[(int)PLAYER_STATE.IDLE], this);
    }

    protected override void Begin()
    {
        throw new NotImplementedException();
    }

    protected override void Exit()
    {
        throw new NotImplementedException();
    }



    // Start is called before the first frame update
    protected override void Start()
    {
        m_state.Begin();
    }

    // Update is called once per frame
    protected override void Update()
    {
        m_state.Run();
    }
}
