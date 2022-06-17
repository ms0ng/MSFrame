# MSFrame

## Content

#### AntiCheat

一个尝试

- Mint 

#### EventCenter

基于委托的事件中心

See [EventCenter](https://github.com/ms0ng/UnityEventCenter)

#### FSM

有限状态机

- State
- StateMachine
- Transition

#### ObjectPool

对象池，旧版本Unity并没有

#### RunningTime

- ActionQueue: 排序执行的Action
- CoroutineManager: 协程管理
- YieldHelper: 协程工具

#### SaveAndLoad

存档/读档工具，可序列化自定义类

#### UI

施工中 +.+ ...

> 基于Panel GamePrefab 的窗口管理并不是合理的。虽然同属于一个“窗口”，“静态”的UI和“动态”的组件也不应在同一个Canvas下

- BaseAltasManager: 图集管理类
- WindowsEntity: 窗口
- WindowsManager: 基于Panel GamePrefab 的窗口管理
- UICanvasManager: ///-!- 施工中 -!-///

#### Xml

读取Xml，反序列化成类。

> 源码需要根据项目实际情况进行改写，搭配其他资源管理框架（如Addressable）读取Xml文件

> 同样也可查看我的 [PyExcel2XML](https://github.com/ms0ng/PyExcel2XML) ,该工具可将Excel表格转成XML

- XmlBaseReader: 读取类
- XmlObject: 读取的字段反序列成XmlObject
- XmlTool
- Exmaple: 示例

#### Logger

调试输出

#### Singleton

单例

- Singleton
- MonoSingleton: 可以应用Unity Component特性
- SingletonR: 基于反射的单例, 支持private构造函数, 会稍微损失一点性能

#### Tools

拓展类
