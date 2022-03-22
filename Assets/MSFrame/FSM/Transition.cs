
namespace MSFrame.FSM
{

    public delegate bool TransitionDelegate();
    public class Transition : ITransition
    {
        public event TransitionDelegate OnTransition;
        public event TransitionDelegate OnCheck;

        private IState _from;
        private IState _to;
        private string _name;

        #region 公开属性
        public IState From
        {
            get
            {
                return _from;
            }

            set
            {
                _from = value;
            }
        }

        public IState To
        {

            get
            {
                return _to;
            }
            set
            {
                _to = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
        #endregion

        public Transition(string name, IState fromState, IState toState)
        {

            _name = name;
            _from = fromState;
            _to = toState;
            if (fromState != null) fromState.AddTransition(this);
        }
        public Transition(string name, IState toState) : this(name, null, toState)
        {

        }
        public bool OnTransitionStart()
        {
            if (OnTransition != null)
            {
                return OnTransition();
            }
            return true;
        }

        public bool Check()
        {
            if (OnCheck != null)
            {
                return OnCheck();
            }
            return false;
        }
    }
}