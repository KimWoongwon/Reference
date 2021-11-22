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
            m_Owner.m_CurState = PLAYER_STATE.IDLE;
        }

        public override void Exit()
        {
            m_Owner.m_PreState = PLAYER_STATE.IDLE;
        }

        public override void Run()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                m_Owner.ChangeState(PLAYER_STATE.WALK);

            if (Input.GetKey(KeyCode.Space))
                m_Owner.ChangeState(PLAYER_STATE.ATTACK);
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
            m_Owner.m_CurState = PLAYER_STATE.WALK;
        }

        public override void Exit()
        {
            m_Owner.m_PreState = PLAYER_STATE.WALK;
        }

        public override void Run()
        {
            SetMoveVector();
            if (m_Owner.MoveVector == Vector2Int.zero)
                m_Owner.ChangeState(PLAYER_STATE.IDLE);

            m_Owner.m_rbody.velocity = m_Owner.MoveVector.normalized * m_Owner.MoveSpeed;
        }

        void SetMoveVector()
        {
            m_Owner.MoveVector = Vector2Int.zero;

            if (Input.GetKey(KeyCode.W))
                m_Owner.MoveVector.y = 1;
            else if (Input.GetKey(KeyCode.S))
                m_Owner.MoveVector.y = -1;

            if (Input.GetKey(KeyCode.D))
                m_Owner.MoveVector.x = 1;
            else if (Input.GetKey(KeyCode.A))
                m_Owner.MoveVector.x = -1;
        }
    }

    public class Player_Attack : FSM<Character_FSM<PLAYER_STATE>>
    {
        private Player_FSM m_Owner;
        private bool AttackFlag;


        public Player_Attack(Player_FSM _owner)
        {
            m_Owner = _owner;
        }
        public override void Begin()
        {
            m_Owner.m_CurState = PLAYER_STATE.ATTACK;
        }

        public override void Exit()
        {
            m_Owner.m_PreState = PLAYER_STATE.ATTACK;
        }

        public override void Run()
        {
            AttackFlag = false;
            if (Input.GetKey(KeyCode.Space))
                AttackFlag = true;

            if (AttackFlag)
            {
                
            }
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
