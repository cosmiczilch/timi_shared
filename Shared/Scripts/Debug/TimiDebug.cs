using System.Diagnostics;

namespace TimiShared.Debug {

    public static class TimiDebug {

        [Conditional("TIMI_DEBUG")]
        public static void Log(string message) {
            UnityEngine.Debug.Log(message);
        }

        [Conditional("TIMI_DEBUG")]
        public static void LogError(string message) {
            UnityEngine.Debug.LogError(message);
        }

        [Conditional("TIMI_DEBUG")]
        public static void LogWarning(string message) {
            UnityEngine.Debug.LogWarning(message);
        }

        [Conditional("TIMI_DEBUG")]
        public static void LogColor(string message, LogColor color) {
            message = color.Prefix + message + color.Postfix;
            TimiDebug.Log(message);
        }

        [Conditional("TIMI_DEBUG")]
        public static void LogWarningColor(string message, LogColor color) {
            message = color.Prefix + message + color.Postfix;
            TimiDebug.LogWarning(message);
        }

        [Conditional("TIMI_DEBUG")]
        public static void LogErrorColor(string message, LogColor color) {
            message = color.Prefix + message + color.Postfix;
            TimiDebug.LogError(message);
        }
    }
}