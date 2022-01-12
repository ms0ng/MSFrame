namespace MSFrame.FSM
{
    public interface IStateMachine
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        IState CurrentStae { get; }
        /// <summary>
        /// 启动时的初始状态
        /// </summary>
        IState DefaultState { set; get; }
        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state">欲添加的状态</param>
        bool AddState(IState state);
        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="state">欲删除的状态</param>
        bool RemoveState(IState state);
        /// <summary>
        /// 根据名称寻找并返回对应的状态
        /// </summary>
        /// <param name="name">状态标签</param>
        IState GetStateWithTag(string name);
    }

}

