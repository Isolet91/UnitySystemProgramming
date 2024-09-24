using System.Diagnostics;

// 1. 추가적인 정보 표시(ex. 타임스템프) >> 버그 발생시 원인 파악에 도움을 줄 수 있음
// 2. 출시 빌드 시 통합 로그 제거
public static class Logger
{
    [Conditional("DEV_VER")]
    public static void Log(string msg)
    {
        UnityEngine.Debug.LogFormat("[{0}] [{1}]", System.DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"), msg);
    }

    [Conditional("DEV_VER")]
    public static void LogWarning(string msg)
    {
        UnityEngine.Debug.LogWarningFormat("[{0}] [{1}]", System.DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"), msg);
    }

    public static void LogError(string msg)
    {
        UnityEngine.Debug.LogErrorFormat("[{0}] [{1}]", System.DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"), msg);
    }
}
