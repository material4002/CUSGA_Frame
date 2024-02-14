# CUSGA 项目框架

## 概述

Unity 中 Mono 的生命周期中，脚本执行的顺序无法很好的控制，若管理器和具体的组件都使用 Mono 进行初始化，可能会出现组件先初始化完成从而调用未初始化的管理器的问题。为解决此问题，此次程序采用树形结构对全部的对象进行初始化，在一个空场景中添加一个 Mono 脚本作为程序入口，在程序入口中分层级地依次实例化各级脚本，防止初始化顺序不可控导致冲突。

### 加载顺序：

游戏开始（场景 0）——>Entry（Mono）——>各顶层管理器——>加载游戏场景——>场景内管理器（Mono）——>游戏物体及组件

Unity 中受到场景的影响，在场景切换时会销毁脚本导致出现很多问题，如数据没有重置，事件没有取消订阅等许多问题。所以将顶层的框架和全局的数据放置在单独的一个场景，不能被销毁。

## 常用模块的使用

### Events 事件管理器

    位于全局，通过管理器管理事件减少系统的耦合。拥有激活/订阅/取消订阅等功能

    命名空间
    Mat

    类名
    Events

    静态属性
    Events Instance #单例模式实例

    成员方法
    bool AddListener(string name, Action<EventArgs> listener) #订阅事件，不能重复订阅
    bool RemoveListener(string name,Action<EventArgs> listener) #取消订阅事件
    bool RemoveListeners(string name) #删除一个事件的全部订阅
    void Clear() #清空事件
    ClearSceneEvent() #清空场景中的事件
    bool AddGlobalListener(string name, Action<EventArgs> listener) #添加全局事件
    RemoveGlobalListener(string name, Action<EventArgs> listener) #移除全局事件

    特殊方法
    通过附加的方式为所有的类添加了TriggerEvent(string name , EventArgs args )方法，可快速激活事件

### LevelManager 场景管理器

    由于直接使用SceneManager会与Unity重名，所以使用了LevelManager作为名称，用于使用异步进行场景加载，使用协程对进度进行追踪。

    命名空间
    Mat

    类名
    LevelManager

    静态属性
    string Loading #事件名称，加载进度
    string IsDone #事件名称，加载完成
    string Start #事件名称，开始加载
    LevelManager Instance #单例模式，获取实例

    成员属性
    int ENTRY #程序入口所在场景（0）
    int COUNT #程序拥有的场景数量
    int currentScene #当前的场景

    成员方法
    bool LoadScene(int scene) #切换场景（自动）

    触发事件
    "S_Loading" #每次加载进度更新触发 接收ProgressArgs作为参数
    "S_IsDone" #加载完成触发 接收EventArgs作为参数
    "S_Start" #开始加载触发 接收EventArgs作为参数

    事件参数
    EventArgs 空参
    ProgressArgs 进度 float progress

### Inputs 输入管理器

    使用了更加完善的InputSystem来管理输入，在Inputs中进行了封装，使用事件来进行交互。

    命名空间
    Mat

    类名
    Inputs

    发布事件
    参考PlayerInputs.inputactions文件中注册内容来获取事件名称
    接收参数为ArgsInput类，继承自EventArgs，增加了InputAction.CallbackContext context变量，context.ReadValue<T>()可获取相应变量。

### AudioManager 音频管理器 (未测试)

    适用于2D简单游戏的简易音效管理器，将音频储存于ScriptableObject中，在管理器中通过字符串可实现播放，可调节不同音频轨道的音量和音调(audioMixer),脱离于空间音效，无需在意监听器和音源。

    命名空间
    Mat

    类名
    AudioManager

    静态属性
    Instance #单例模式实例
    string Music_V = "Music_V"; #混音器名称，音乐音量
    string Music_P = "Music_P"; #混音器名称，音乐音调
    string Sound_V = "Sound_V"; #同上，音效
    string Sound_P = "Sound_P"; #同上，音效
    string BGM_V = "BGM_V";#同上，背景音
    string BGM_P = "BGM_P";#同上，背景音
    string Master_V = "Master_V";#同上，全部声音
    const string Master_P = "Master_P";#同上，全部声音

    实例方法
    void ChangeMusic(string name) #切换音乐
    void PauseMusic() #暂停音乐
    void StopMusic() #停止音乐
    void PlayMusic() #播放音乐
    void ResetMusic() #停止+播放
    void ChangeBGM(string name) #切换背景音
    void PauseBGM() #暂停背景音
    void StopBGM() #停止背景音
    void PlayBGM() #继续播放背景音
    void ResetBGM() #停止+播放
    void PlaySound(string name) #播放音频(只播放一次)
    bool SetVolume(string mixerName,float volume) #设置混音器音量，数值-80——20，名称参考静态属性
    bool SetPitch(string mixerName,float pitch) #设置混音器音调，数值1——1000，名称参考静态属性
