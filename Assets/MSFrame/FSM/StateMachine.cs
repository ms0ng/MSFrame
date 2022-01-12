using System.Collections.Generic;
using UnityEngine;

namespace MSFrame.FSM
{
    public class StateMachine : State, IStateMachine
    {
        private IState _currentState;
        private IState _defaultState;
        private List<IState> _states;

        private bool _isTransition = false; //是否在过渡
        private ITransition _t;  //当前正在执行的过度

        #region 公开属性
        /// <summary>
        /// 当前状态
        /// </summary>
        public IState CurrentStae
        {
            get
            {
                return _currentState;
            }
        }
        /// <summary>
        /// 默认状态
        /// </summary>
        public IState DefaultState
        {
            get
            {
                return _defaultState;
            }
            set
            {
                AddState(value);
                _defaultState = value;
            }
        }
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">状态机名</param>
        /// <param name="state">初始状态</param>
        public StateMachine(string name, State state) : base(name)
        {
            _states = new List<IState>();
            _defaultState = state;
        }

        public bool AddState(IState state)
        {
            if (state != null && !_states.Contains(state))
            {
                _states.Add(state);
                state.Parent = this;
                if (_defaultState == null)
                {
                    _defaultState = state;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveState(IState state)
        {
            if (_currentState == state)
            {
                return false;
            }
            if (state != null && _states.Contains(state))
            {
                _states.Remove(state);
                state.Parent = null;
                if (_defaultState == state)
                {
                    _defaultState = (_states.Count >= 1) ? _states[0] : null;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public IState GetStateWithTag(string name)
        {
            foreach (IState state in _states)
            {
                if (state.Name.Equals(name)) return state;
            }
            return null;
        }

        /// <summary>
        /// 进入该状态机时的方法
        /// </summary>
        /// <param name="prevState">上一个状态</param>
        public override void OnStateEnter(IState prevState)
        {
            if (_currentState == null) _currentState = DefaultState;
            _currentState.OnStateEnter(prevState);
            base.OnStateEnter(prevState);
        }
        /// <summary>
        /// 退出该状态机
        /// </summary>
        /// <param name="next">下一个状态(机)</param>
        public override void OnStateExit(IState next)
        {
            _currentState.OnStateExit(next);
            //状态归位
            _currentState = DefaultState;
            base.OnStateExit(next);
        }
        /// <summary>
        /// TODO:忽略条件,强制变更状态机内的状态(未完成)
        /// </summary>
        /// <param name="next">下一个状态(机)</param>
        [System.Obsolete]
        public void ForceTransferToState(IState next)
        {
            base.OnStateExit(next);
            //当前状态退出
            _currentState.OnStateExit(next);
            next.OnStateEnter(_currentState);
            //设置当前状态
            _currentState = next;
        }

        public override void OnStateUpdate(float deltaTime)
        {
            base.OnStateUpdate(deltaTime);
            foreach (ITransition t in _currentState.Transitions)
            {
                if (t.Check() && t.OnTransitionStart())
                {
                    DoTransition(t);
                    return;
                }
            }
            _currentState.OnStateUpdate(deltaTime);
        }

        public override void OnStateFixedUpdate()
        {
            OnStateUpdate(Time.fixedDeltaTime);
        }

        private void DoTransition(ITransition t)
        {
            if (t.From == null)
            {
                t.From = _currentState;
            }
            _currentState.OnStateExit(t.To);
            _currentState = t.To;
            _currentState.OnStateEnter(t.From);
        }
    }
}
