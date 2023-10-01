using SINCAdvancedModManager.Utils;
using System.Reflection;

namespace SINCAdvancedModManager {
    internal static class Program {
        public static string InstalledGamePath { get; private set; } = "";

        public static string AppPath { get; private set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string LocalModsPath { get; private set; } = Path.Join(AppPath, "installed");


        public static bool LoaderInstalled => File.Exists(Path.Join(InstalledGamePath, "version.dll"));

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            ApplicationConfiguration.Initialize();

            ErrorUtils.ShowWarn("The contributors of this project are not responsible for any damage caused by installing a mod using this tool. Use at your own risk.");

            if (!File.Exists(Path.Join(AppPath, "sincpath"))) {
                ErrorUtils.ShowError("You must select your Software Inc. installation to use this program.");
                SelectNewPath();
            }

            InstalledGamePath = File.ReadAllText(Path.Join(AppPath, "sincpath"));

            while (!File.Exists(Path.Join(InstalledGamePath, "Software Inc.exe"))) {
                ErrorUtils.ShowError("Your previously selected path seems to be outdated. Please select the installation path of Software Inc again");
                SelectNewPath();
            }

            if (!Directory.Exists(LocalModsPath)) {
                Directory.CreateDirectory(LocalModsPath);
            }

            Application.Run(new StartForm());
        }

        static void SelectNewPath() {
            var fileOpenDialog = new OpenFileDialog {
                Title = "Select your Software Inc. installation",
                Filter = "Software Inc|Software Inc.exe"
            };

            if (fileOpenDialog.ShowDialog() == DialogResult.OK) {
                InstalledGamePath = Path.GetDirectoryName(fileOpenDialog.FileName);
                File.WriteAllText(Path.Join(AppPath, "sincpath"), InstalledGamePath);
            } else {
                MessageBox.Show("You must select your Software Inc. installation to use this program.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}