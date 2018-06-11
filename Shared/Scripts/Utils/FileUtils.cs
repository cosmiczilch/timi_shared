using System.IO;
using TimiShared.Debug;

namespace TimiShared.Utils {
    public static class FileUtils {

        public static FileStream OpenFileStream(string filePath, FileMode openMode, FileAccess accessType) {
            if (string.IsNullOrEmpty(filePath)) {
                TimiDebug.LogErrorColor("File path empty", LogColor.grey);
                return null;
            }
            if (!File.Exists(filePath)) {
                TimiDebug.LogErrorColor("No such file:" + filePath, LogColor.grey);
                return null;
            }
            FileStream fileStream = new FileStream(filePath, openMode, accessType);
            return fileStream;
        }

        public static void CloseFileStream(FileStream fileStream) {
            if (fileStream != null) {
                fileStream.Close();
            }
        }
    }
}
