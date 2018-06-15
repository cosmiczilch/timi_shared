using System;
using System.Diagnostics;
using System.Reflection;

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

        [Conditional("TIMI_DEBUG")]
        public static void PrintCodePosition(LogColor logColor) {
            StackFrame stackFrame = new System.Diagnostics.StackFrame(1, true);
            string message = "-> { " + stackFrame.GetMethod().ToString() + " }";
            string[] filePath = stackFrame.GetFileName().Split('/');
            message += " in " + filePath[filePath.Length - 1]; // Append the file name
            message += "::" + stackFrame.GetFileLineNumber().ToString();
            TimiDebug.LogColor(message, logColor);
            return;
        }

    }
}