using System.Collections.Generic;

namespace MSFrame.FSM
{
    public delegate void StateDelegate();
    public delegate void StateDelegateWithState(IState state);
    public delegate void StateDelegateWithFloat(float f);

    public class State : IState
    {
        public event StateDelegateWithState OnEnter;
        public event StateDelegateWithState OnExit;
        public event StateDelegateWithFloat OnUpdate;
        public event StateDelegate OnFixedUpdate;

        private string _name;
        private IStateMachine _parent;
        private float _timer;
        private List<ITransition> _transitionList;

        #region 公开属性
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public IStateMachine Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        public float Timer
        {
            get
            {
                return _timer;
            }
        }

        public List<ITransition> Transitions
        {
            get
            {
                return _transitionList;
            }
        }
        #endregion

        public State(string name)
        {
            _name = name;
            _transitionList = new List<ITransition>();
        }

        public bool AddTransition(ITransition t)
        {
            if (t != null && !_transitionList.Contains(t))
            {
                _transitionList.Add(t);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void OnStateEnter(IState prevState)
        {
            //重置计时器
            _timer = 0;
            OnEnter?.Invoke(prevState);
        }

        public virtual void OnStateExit(IState next)
        {
            _timer = 0;
            OnExit?.Invoke(next);
        }

        public virtual void OnStateUpdate(float deltaTime)
        {
            //状态记时
            _timer += deltaTime;
            OnUpdate?.Invoke(deltaTime);
        }

        public virtual void OnStateFixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
    }

}
