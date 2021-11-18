using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYFSM
{
    public class Enemy_01_Idle : FSM<Character_FSM<ENEMY_01_STATE>>
    {
        private Enemy_01_FSM m_Owner;
        public Enemy_01_Idle(Enemy_01_FSM _owner)
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

    public class Enemy_01_Walk : FSM<Character_FSM<ENEMY_01_STATE>>
    {
        private Enemy_01_FSM m_Owner;
        public Enemy_01_Walk(Enemy_01_FSM _owner)
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

    public class Enemy_01_Attack : FSM<Character_FSM<ENEMY_01_STATE>>
    {
        private Enemy_01_FSM m_Owner;
        public Enemy_01_Attack(Enemy_01_FSM _owner)
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

    public class Enemy_01_Follow : FSM<Character_FSM<ENEMY_01_STATE>>
    {
        private Enemy_01_FSM m_Owner;
        public Enemy_01_Follow(Enemy_01_FSM _owner)
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

    public class Enemy_01_Unfollow : FSM<Character_FSM<ENEMY_01_STATE>>
    {
        private Enemy_01_FSM m_Owner;
        public Enemy_01_Unfollow(Enemy_01_FSM _owner)
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

    public class Enemy_01_Die : FSM<Character_FSM<ENEMY_01_STATE>>
    {
        private Enemy_01_FSM m_Owner;
        public Enemy_01_Die(Enemy_01_FSM _owner)
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

