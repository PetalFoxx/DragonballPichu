using Terraria.ModLoader;

namespace DragonballPichu.Common.Systems
{
    // Acts as a container for keybinds registered by this mod.
    // See Common/Players/ExampleKeybindPlayer for usage.
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind ChargeKeybind { get; private set; }
        public static ModKeybind TransformKeybind { get; private set; }
        public static ModKeybind RevertFormKeybind { get; private set; }
        public static ModKeybind AltFormKeybind { get; private set; }
        public static ModKeybind ShowMenuKeybind { get; private set; }
        public static ModKeybind AdminGiveFormPoint { get; private set; }

        public override void Load()
        {
            // Registers a new keybind
            // We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual text displayed to english users is in en-US.hjson
            AltFormKeybind = KeybindLoader.RegisterKeybind(Mod, "AltForm", "Z");
            TransformKeybind = KeybindLoader.RegisterKeybind(Mod, "Transform", "X");
            ChargeKeybind = KeybindLoader.RegisterKeybind(Mod, "Charge", "C");
            RevertFormKeybind = KeybindLoader.RegisterKeybind(Mod, "RevertForm", "V");
            ShowMenuKeybind = KeybindLoader.RegisterKeybind(Mod, "ShowMenu", "K");
            AdminGiveFormPoint = KeybindLoader.RegisterKeybind(Mod, "AdminGiveFormPoint", "L");
        }
        public override void Unload()
        {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            ChargeKeybind = null;
            TransformKeybind = null;
            RevertFormKeybind = null;
            AltFormKeybind = null;
            ShowMenuKeybind = null;
            AdminGiveFormPoint = null;
        }
    }
}