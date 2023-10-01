namespace SINCAdvancedModManager.Utils {
    internal class SizeUtils {
        public static float GetSizeInMegabytes(long bytes) {
            return (float)bytes / 1024 / 1024;
        }
    }
}
