using Newtonsoft.Json;
using SINCAdvancedModManager.Objects;
using SINCAdvancedModManager.Utils;
using System.ComponentModel;
using System.IO.Compression;
using System.Net;

namespace SINCAdvancedModManager {
    public partial class StartForm : Form {
        public static StartForm Instance { get; private set; }
        private BindingList<ModMeta> mods = new();

        public StartForm() {
            InitializeComponent();
            ListBoxMods.DataSource = mods;
            Instance = this;

            LoadInstalledMods();
        }

        private void LoadInstalledMods() {
            mods.Clear();
            ListBoxMods.ClearSelected();

            string[] modDirectories = Directory.GetDirectories(Program.LocalModsPath);

            foreach (var modDirectory in modDirectories) {
                var metaPath = Path.Join(modDirectory, "meta.json");

                if (!File.Exists(metaPath)) {
                    continue; // Probably a corrupted mod
                }

                ModMeta? meta = JsonConvert.DeserializeObject<ModMeta>(
                        File.ReadAllText(metaPath),
                        new JsonSerializerSettings() {
                            MissingMemberHandling = MissingMemberHandling.Ignore,
                        });

                if (meta == null) {
                    continue;
                }

                mods.Add(meta);
            }

            UpdateSelectedUI();
        }

        private void UpdateSelectedUI() {
            ModMeta? selectedMod = ListBoxMods.SelectedItem as ModMeta;

            if (selectedMod == null) {
                return;
            }

            LabelModTitle.Text = selectedMod.ModDisplayName;
        }

        private void StartForm_Load(object sender, EventArgs e) {
            UpdateLoaderInstallationStatus();
            UpdateInstallerButton();
        }

        private void UpdateLoaderInstallationStatus() {
            LabelLoaderInstallationStatus.Text = $"Loader Installed: {Program.LoaderInstalled}";
        }

        private async void ButtonInstallLoader_Click(object sender, EventArgs e) {
            if (Program.LoaderInstalled) {
                ErrorUtils.ShowError("The loader is already installed!");
                return;
            }

            if (!await DownloadMelonLoader()) return;
            LabelState.Text = "Downloaded MelonLoader!";

            if (!await ExtractMelonLoader()) return;
            LabelState.Text = "Extracted files!";

            if (!await CopyFiles()) return;
            LabelState.Text = "Copied files!";

            if (!await SyncMods()) return;
            LabelState.Text = "Synced mods!";

            UpdateLoaderInstallationStatus();
        }

        private async Task<bool> DownloadMelonLoader() {
            LabelState.Text = "Downloading MelonLoader... (0 mb / ? mb)";
            ProgessBarState.Value = 0;

            WebClient wc = new();
            wc.DownloadProgressChanged += MelonLoader_DownloadProgressChanged;

            try {
                await wc.DownloadFileTaskAsync(
                    new Uri("https://github.com/LavaGang/MelonLoader/releases/download/v0.5.3/MelonLoader.x64.zip"),
                    Path.Join(Program.AppPath, "MelonLoader.zip"));
            } catch (Exception ex) {
                ErrorUtils.ShowError($"Failed to download melonloader!\n" +
                    $"Error: {ex.Message}");
            }

            return true;
        }

        private async Task<bool> ExtractMelonLoader() {
            try {
                LabelState.Text = "Extracting files...";
                ProgessBarState.Value = 0;

                string melonLoaderExtractionFolder = Path.Join(Program.AppPath, "MelonLoaderFiles");

                ZipArchive zipArchive = new(
                    File.OpenRead(Path.Join(Program.AppPath, "MelonLoader.zip")),
                    ZipArchiveMode.Read);

                if (Directory.Exists(melonLoaderExtractionFolder))
                    Directory.Delete(melonLoaderExtractionFolder, true);

                Directory.CreateDirectory(melonLoaderExtractionFolder);
                zipArchive.ExtractToDirectory(melonLoaderExtractionFolder);

                ProgessBarState.Value = 100;

                return true;
            } catch (Exception ex) {
                ErrorUtils.ShowError($"Failed to extract melonloader!\n" +
                    $"Error: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> CopyFiles() {
            try {
                LabelState.Text = "Installing loader...";
                ProgessBarState.Value = 0;

                string melonLoaderExtractionFolder = Path.Join(Program.AppPath, "MelonLoaderFiles");
                DirectoryUtils.CopyRecursive(melonLoaderExtractionFolder,
                    Program.InstalledGamePath);

                ProgessBarState.Value = 100;

                return true;
            } catch (Exception ex) {
                ErrorUtils.ShowError($"Failed to install loader!\n" +
                    $"Error: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> SyncMods() {
            try {
                if (mods.Count == 0) return true;

                LabelState.Text = "Syncing mods...";
                ProgessBarState.Value = 0;
                ProgessBarState.Maximum = mods.Count;

                foreach (var mod in mods) {
                    try {
                        InstallModToGame(Path.Join(Program.LocalModsPath, mod.ModFolderName));
                    } catch (Exception ex) {
                        ErrorUtils.ShowError($"Failed to synchronize mod '{mod.ModDisplayName}'!\n" +
                            $"Error: {ex.Message}");
                    }
                }

                return true;
            } catch (Exception ex) {
                ErrorUtils.ShowError($"Failed to synchronize mods!\n" +
                    $"Error: {ex.Message}");
                return false;
            }
        }

        private void MelonLoader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            LabelState.Text = $"Downloading MelonLoader... (" +
                $"{SizeUtils.GetSizeInMegabytes(e.BytesReceived):F2} mb / " +
                $"{SizeUtils.GetSizeInMegabytes(e.TotalBytesToReceive):F2} mb" +
                $")";

            ProgessBarState.Value = e.ProgressPercentage;
        }

        private void UpdateInstallerButton() {
            if (Program.LoaderInstalled) {
                ButtonInstallLoader.Visible = false;
            }
        }

        private void ButtonInstallFromGIT_Click(object sender, EventArgs e) {
            var dialog = new AddFromGITForm();
            dialog.ShowDialog();
        }

        public void InstallFromGIT(string url) {

        }

        private void ButtonInstallFromZip_Click(object sender, EventArgs e) {
            OpenFileDialogZipFile.ShowDialog();
        }

        private void OpenFileDialogZipFile_FileOk(object sender, CancelEventArgs e) {
            // TODO: Add Status Strip Updates
            if (!File.Exists(OpenFileDialogZipFile.FileName)) {
                ErrorUtils.ShowError("The specified file was not found.");
            }

            try {
                var extractPath = Path.Join(Program.AppPath, "tmpextract");

                if (Directory.Exists(extractPath)) {
                    Directory.Delete(extractPath, true);
                }

                using (ZipArchive archive =
                    new ZipArchive(File.OpenRead(OpenFileDialogZipFile.FileName), ZipArchiveMode.Read)) {
                    archive.ExtractToDirectory(extractPath);

                    var meta = InstallModToGame(extractPath);

                    DirectoryUtils.CopyRecursive(Path.Join(extractPath),
                        Path.Join(Program.LocalModsPath, meta.ModFolderName));

                    LoadInstalledMods();
                }
            } catch (Exception ex) {
                ErrorUtils.ShowError("Was not able to install mod.\n\n" +
                    "Error:" + ex.Message);
            }
        }

        private ModMeta InstallModToGame(string path) {
            if (!File.Exists(Path.Join(path, "meta.json"))) {
                throw new InvalidDataException("Mod is missing a meta.json file in root.");
            }

            ModMeta meta = JsonConvert.DeserializeObject<ModMeta>(
                File.ReadAllText(Path.Join(path, "meta.json")),
                new JsonSerializerSettings() {
                    MissingMemberHandling = MissingMemberHandling.Error,
                })
                ?? throw new InvalidDataException("Invalid meta.json");

            if (!Directory.Exists(Path.Join(path, "files"))) {
                throw new InvalidDataException("Mod is missing mod files.");
            }

            var creatorModTypePath = GetPathForModType(meta.ModType);

            if (!Directory.Exists(creatorModTypePath)) {
                Directory.CreateDirectory(creatorModTypePath);
            }

            if (!Directory.Exists(Path.Join(Program.LocalModsPath, meta.ModFolderName))) {
                Directory.CreateDirectory(Path.Join(Program.LocalModsPath, meta.ModFolderName));
            }

            DirectoryUtils.CopyRecursive(Path.Join(path, "files"), creatorModTypePath);

            return meta;
        }

        private string GetPathForModType(ModType modType) {
            var path = Program.InstalledGamePath;

            if (modType == ModType.Mod) {
                path = Path.Join(path, "Mods");
            } else if (modType == ModType.UserLib) {
                path = Path.Join(path, "UserLibs");
            }

            return path;
        }

        private void ButtonRemove_Click(object sender, EventArgs e) {
            ModMeta? selectedMod = ListBoxMods.SelectedItem as ModMeta;

            if (selectedMod == null) {
                return;
            }

            RemoveMod(selectedMod);
        }

        private void RemoveMod(ModMeta meta) {
            var localModPath = Path.Join(Program.LocalModsPath, meta.ModFolderName);

            var creatorModBasePath = GetPathForModType(meta.ModType);

            foreach (var modFile in Directory.GetFiles(Path.Join(localModPath, "files"))) {
                var creatorModFilePath = Path.Join(creatorModBasePath, Path.GetFileName(modFile));

                if (File.Exists(creatorModFilePath)) {
                    File.Delete(creatorModFilePath);
                }
            }

            if (Directory.Exists(localModPath)) {
                Directory.Delete(localModPath, true);
            }

            LoadInstalledMods();
        }
    }
}
