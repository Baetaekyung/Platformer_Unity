using UnityEngine;

/// <summary>
/// <class name="Debug">
/// <para>
/// If you want to use Debug methods you must do
/// Project Setting -> Player -> Scripting Define Symbols -> Add ENABLE_LOG define
/// </para>
/// when you build this project you remove the ENABLE_LOG define
/// then your project's Debug method will be ignore
/// </class>
/// </summary>
public static class Debug
{
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void Log(object message)
    {
        UnityEngine.Debug.Log(message);
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogWarning(object message)
    {
        UnityEngine.Debug.LogWarning(message);
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogError(object message)
    {
        UnityEngine.Debug.LogError(message);
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawRayWithDuration(Vector3 start, Vector3 dir, Color color, float duration)
    {
        UnityEngine.Debug.DrawRay(start, dir, color, duration);
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawRayWithOutDuration(Vector3 start, Vector3 dir, Color color)
    {
        UnityEngine.Debug.DrawRay(start, dir, color);
    }
}
