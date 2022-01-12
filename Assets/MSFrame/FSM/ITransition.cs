
namespace MSFrame.FSM
{
    public interface ITransition
    {
        /// <summary>
        /// 源状态
        /// </summary>
        IState From { get; set; }

        /// <summary>
        /// 目标状态
        /// </summary>
        IState To { get; set; }

        /// <summary>
        /// 过渡名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 开始变换状态的方法
        /// </summary>
        bool OnTransitionStart();

        /// <summary>
        /// 变换状态的判断方法
        /// </summary>
        bool Check();

    }
}
