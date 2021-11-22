using System;
using UnityEngine;
using CUSTOM_ReadOnly;

namespace MYFSM
{
    abstract public class FSM<T>
    {
        abstract public void Begin();
        abstract public void Run();
        abstract public void Exit();
    }

    abstract public class Character_FSM<T> : MonoBehaviour where T : Enum
    {
        public Machine<Character_FSM<T>> m_state;
        public FSM<Character_FSM<T>>[] m_arrState;

        [ReadOnly] public T m_CurState;
        [ReadOnly] public T m_PreState;
        [ReadOnly] public Animator m_Animator;
        [ReadOnly] public Vector2Int StartPosition;

        public Character_FSM()
        {
            m_state = new Machine<Character_FSM<T>>();
            m_arrState = new FSM<Character_FSM<T>>[(int)Enum.Parse(typeof(T), "END")];
        }

        abstract protected void Init();
        abstract protected void Begin();
        abstract protected void Exit();
        abstract protected void Run();

        abstract protected void Start();
        abstract protected void Update();

        public void ChangeState(T state)
        {
            m_state.Change(m_arrState[(int)Enum.Parse(typeof(T), state.ToString())]);
        }
    }
}


