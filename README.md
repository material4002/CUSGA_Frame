# CUSGA ��Ŀ���

## ����

Unity �� Mono �����������У��ű�ִ�е�˳���޷��ܺõĿ��ƣ����������;���������ʹ�� Mono ���г�ʼ�������ܻ��������ȳ�ʼ����ɴӶ�����δ��ʼ���Ĺ����������⡣Ϊ��������⣬�˴γ���������νṹ��ȫ���Ķ�����г�ʼ������һ���ճ��������һ�� Mono �ű���Ϊ������ڣ��ڳ�������зֲ㼶������ʵ���������ű�����ֹ��ʼ��˳�򲻿ɿص��³�ͻ��

### ����˳��

��Ϸ��ʼ������ 0������>Entry��Mono������>���������������>������Ϸ��������>�����ڹ�������Mono������>��Ϸ���弰���

Unity ���ܵ�������Ӱ�죬�ڳ����л�ʱ�����ٽű����³��ֺܶ����⣬������û�����ã��¼�û��ȡ�����ĵ�������⡣���Խ�����Ŀ�ܺ�ȫ�ֵ����ݷ����ڵ�����һ�����������ܱ����١�

## ����ģ���ʹ��

### Events �¼�������

    λ��ȫ�֣�ͨ�������������¼�����ϵͳ����ϡ�ӵ�м���/����/ȡ�����ĵȹ���

    �����ռ�
    Mat

    ����
    Events

    ��̬����
    Events Instance #����ģʽʵ��

    ��Ա����
    bool AddListener(string name, Action<EventArgs> listener) #�����¼��������ظ�����
    bool RemoveListener(string name,Action<EventArgs> listener) #ȡ�������¼�
    bool RemoveListeners(string name) #ɾ��һ���¼���ȫ������
    void Clear() #����¼�
    ClearSceneEvent() #��ճ����е��¼�
    bool AddGlobalListener(string name, Action<EventArgs> listener) #���ȫ���¼�
    RemoveGlobalListener(string name, Action<EventArgs> listener) #�Ƴ�ȫ���¼�

    ���ⷽ��
    ͨ�����ӵķ�ʽΪ���е��������TriggerEvent(string name , EventArgs args )�������ɿ��ټ����¼�

### LevelManager ����������

    ����ֱ��ʹ��SceneManager����Unity����������ʹ����LevelManager��Ϊ���ƣ�����ʹ���첽���г������أ�ʹ��Э�̶Խ��Ƚ���׷�١�

    �����ռ�
    Mat

    ����
    LevelManager

    ��̬����
    string Loading #�¼����ƣ����ؽ���
    string IsDone #�¼����ƣ��������
    string Start #�¼����ƣ���ʼ����
    LevelManager Instance #����ģʽ����ȡʵ��

    ��Ա����
    int ENTRY #����������ڳ�����0��
    int COUNT #����ӵ�еĳ�������
    int currentScene #��ǰ�ĳ���

    ��Ա����
    bool LoadScene(int scene) #�л��������Զ���

    �����¼�
    "S_Loading" #ÿ�μ��ؽ��ȸ��´��� ����ProgressArgs��Ϊ����
    "S_IsDone" #������ɴ��� ����EventArgs��Ϊ����
    "S_Start" #��ʼ���ش��� ����EventArgs��Ϊ����

    �¼�����
    EventArgs �ղ�
    ProgressArgs ���� float progress

### Inputs ���������

    ʹ���˸������Ƶ�InputSystem���������룬��Inputs�н����˷�װ��ʹ���¼������н�����

    �����ռ�
    Mat

    ����
    Inputs

    �����¼�
    �ο�PlayerInputs.inputactions�ļ���ע����������ȡ�¼�����
    ���ղ���ΪArgsInput�࣬�̳���EventArgs��������InputAction.CallbackContext context������context.ReadValue<T>()�ɻ�ȡ��Ӧ������

### AudioManager ��Ƶ������ (δ����)

    ������2D����Ϸ�ļ�����Ч������������Ƶ������ScriptableObject�У��ڹ�������ͨ���ַ�����ʵ�ֲ��ţ��ɵ��ڲ�ͬ��Ƶ���������������(audioMixer),�����ڿռ���Ч�������������������Դ��

    �����ռ�
    Mat

    ����
    AudioManager

    ��̬����
    Instance #����ģʽʵ��
    string Music_V = "Music_V"; #���������ƣ���������
    string Music_P = "Music_P"; #���������ƣ���������
    string Sound_V = "Sound_V"; #ͬ�ϣ���Ч
    string Sound_P = "Sound_P"; #ͬ�ϣ���Ч
    string BGM_V = "BGM_V";#ͬ�ϣ�������
    string BGM_P = "BGM_P";#ͬ�ϣ�������
    string Master_V = "Master_V";#ͬ�ϣ�ȫ������
    const string Master_P = "Master_P";#ͬ�ϣ�ȫ������

    ʵ������
    void ChangeMusic(string name) #�л�����
    void PauseMusic() #��ͣ����
    void StopMusic() #ֹͣ����
    void PlayMusic() #��������
    void ResetMusic() #ֹͣ+����
    void ChangeBGM(string name) #�л�������
    void PauseBGM() #��ͣ������
    void StopBGM() #ֹͣ������
    void PlayBGM() #�������ű�����
    void ResetBGM() #ֹͣ+����
    void PlaySound(string name) #������Ƶ(ֻ����һ��)
    bool SetVolume(string mixerName,float volume) #���û�������������ֵ-80����20�����Ʋο���̬����
    bool SetPitch(string mixerName,float pitch) #���û�������������ֵ1����1000�����Ʋο���̬����
