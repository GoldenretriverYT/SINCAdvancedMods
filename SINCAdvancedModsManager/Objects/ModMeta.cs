using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINCAdvancedModManager.Objects {
    public class ModMeta {
        public string ModDisplayName { get; set; } = "Unknown";
        public string ModFolderName { get; set; } = "Unknown";
        public ModType ModType { get; set; } = ModType.Mod;

        public override string ToString() {
            return ModType.ToString() + " | " + ModDisplayName;
        }
    }

    public enum ModType {
        Mod,
        UserLib
    }
}
