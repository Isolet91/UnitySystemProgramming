using UnityEngine;

public class SingletonBehavior<T> : MonoBehaviour where T : SingletonBehavior<T>
{
    // 씬 전환 시 삭제 여부
    protected bool m_IsDestroyOnLoad = false;

    // 해당 클래스의 스태틱 인스턴스 변수
    protected static T m_Instance;

    public static T Instance
    { get { return m_Instance; } }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if(m_Instance == null)
        {
            m_Instance = (T)this;

            if(!m_IsDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 삭제 시 실행되는 함수
    protected virtual void OnDestory()
    {
        Dispose();
    }

    // 삭제 시 추가적으로 처리해야하는 것들을 여기서 처리
    protected virtual void Dispose()
    {
        m_Instance = null;
    }
}
