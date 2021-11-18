using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYFSM
{
    public class Player_Idle : FSM<Character_FSM<PLAYER_STATE>>
    {
        private Player_FSM m_Owner;
        public Player_Idle(Player_FSM _owner)
        {
            m_Owner = _owner;
        }

        public override void Begin()
        {
            Debug.Log("Player Idle Begin!");
            m_Owner.m_CurState = PLAYER_STATE.IDLE;
        }

        public override void Exit()
        {
            Debug.Log("Player Idle Begin!");
            m_Owner.m_PreState = PLAYER_STATE.IDLE;
        }

        public override void Run()
        {

        }
    }

    public class Player_Walk : FSM<Character_FSM<PLAYER_STATE>>
    {
        private Player_FSM m_Owner;
        public Player_Walk(Player_FSM _owner)
        {
            m_Owner = _owner;
        }
        public override void Begin()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void Run()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Player_Attack : FSM<Character_FSM<PLAYER_STATE>>
    {
        private Player_FSM m_Owner;
        public Player_Attack(Player_FSM _owner)
        {
            m_Owner = _owner;
        }
        public override void Begin()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void Run()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Player_Die : FSM<Character_FSM<PLAYER_STATE>>
    {
        private Player_FSM m_Owner;
        public Player_Die(Player_FSM _owner)
        {
            m_Owner = _owner;
        }
        public override void Begin()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void Run()
        {
            throw new System.NotImplementedException();
        }
    }

}
