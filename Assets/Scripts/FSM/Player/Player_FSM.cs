using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYFSM;
using CUSTOM_ReadOnly;
using System;

public class Player_FSM : MonoBehaviour
{
    public Machine<Player_FSM> m_state;
    public FSM<Player_FSM>[] m_arrState = new FSM<Player_FSM>[(int)PLAYER_STATE.END];

    [ReadOnly] public PLAYER_STATE m_CurState;
    [ReadOnly] public PLAYER_STATE m_PreState;

    [ReadOnly] public Animator m_Animator;

    [ReadOnly] public Vector2Int StartPosition;

    public Player_FSM()
    {
        Init();
    }

    private void Init()
    {
        m_state = new Machine<Player_FSM>();

        m_arrState[(int)PLAYER_STATE.IDLE] = new Player_Idle(this);
        m_arrState[(int)PLAYER_STATE.WALK] = new Player_Walk(this);
        m_arrState[(int)PLAYER_STATE.ATTACK] = new Player_Attack(this);
        m_arrState[(int)PLAYER_STATE.DIE] = new Player_Die(this);

        m_state.SetState(m_arrState[(int)PLAYER_STATE.IDLE], this);
    }

    public void ChangeState(PLAYER_STATE state)
    {
        m_state.Change(m_arrState[(int)state]);
    }

    public void Begin()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        m_state.Begin();
    }

    // Update is called once per frame
    void Update()
    {
        m_state.Run();
    }
}
