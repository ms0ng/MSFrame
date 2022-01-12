using System.Collections.Generic;

namespace MSFrame.FSM
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// 状态名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 所属状态机
        /// </summary>
        IStateMachine Parent { get; set; }
        /// <summary>
        /// 进入状态的时长
        /// </summary>
        float Timer { get; }
        /// <summary>
        /// 状态变更条件列表
        /// </summary>
        List<ITransition> Transitions { get; }

        /// <summary>
        /// 进入状态时的方法
        /// </summary>
        /// <param name="prevState">上一个状态</param>
        void OnStateEnter(IState prevState);

        /// <summary>
        /// 退出状态时的方法
        /// </summary>
        /// <param name="nextState">下一个状态</param>
        void OnStateExit(IState nextState);

        /// <summary>
        /// 每帧更新
        /// </summary>
        /// <param name="deltaTime">距离上一次更新所经过的时间</param>
        void OnStateUpdate(float deltaTime);

        /// <summary>
        /// 每一固定时间帧更新
        /// </summary>
        void OnStateFixedUpdate();

        /// <summary>
        /// 添加转换条件
        /// </summary>
        /// <param name="t">转换条件</param>
        bool AddTransition(ITransition t);
    }
}

