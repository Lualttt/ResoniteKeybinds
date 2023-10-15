using HarmonyLib;
using ResoniteModLoader;
using System;
using System.Reflection;
using Elements.Core;
using FrooxEngine;

namespace ResoniteKeybinds {
    public class ResoniteKeybinds : ResoniteMod
    {
        public override string Name => "ResoniteKeybinds";
        public override string Author => "Lualt";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/LualtOfficial/ResoniteKeybinds";
    
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_FORWARD = new ModConfigurationKey<Key>("keyForwad", "Forward", () => Key.W);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_BACKWARD = new ModConfigurationKey<Key>("keyBackward", "Backward", () => Key.S);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_LEFT = new ModConfigurationKey<Key>("keyLeft", "Left", () => Key.A);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_RIGHT = new ModConfigurationKey<Key>("keyRight", "Right", () => Key.D);
        
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER1 = new ModConfigurationKey<dummy>(" ", "");
        
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_SPRINT = new ModConfigurationKey<Key>("keySprint", "Sprint", () => Key.Shift);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_SLOW = new ModConfigurationKey<Key>("keySlow", "Slow", () => Key.Z);
        
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER2 = new ModConfigurationKey<dummy>("  ", "");
        
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_JUMP = new ModConfigurationKey<Key>("keyJump", "Jump", () => Key.Space);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_CROUCH = new ModConfigurationKey<Key>("keyCrouch", "Crouch", () => Key.C);
        
        private static ModConfiguration Config;
        
        public override void OnEngineInit() {
            Config = GetConfiguration();
            Config.Save(true);
            
            Config.OnThisConfigurationChanged += ConfigChanged;
            
            Harmony harmony = new Harmony("lt.lua.ResoniteKeybinds");
            harmony.PatchAll();
        }
        
        private static void ConfigChanged(ConfigurationChangedEvent @event)
        {
            // TODO: add hot swappable keybinds :)
        }

        // ReSharper disable once InconsistentNaming
        [HarmonyPatch(typeof(KeyboardAndMouseBindingGenerator), "GenerateScreenDirection")]
        class KeyboardAndMouseBindingGenerator_GenerateScreenDirection_Patch
        {
            static ScreenLocomotionDirection Postfix(ScreenLocomotionDirection __result, float? overrideFast = null, float? overrideSlow = null, LocomotionReference? overrideReference = null)
            {
                ScreenLocomotionDirection screenLocomotionDirection = new ScreenLocomotionDirection();
                screenLocomotionDirection.Forward = InputNode.Key(Config.GetValue(KEY_FORWARD));
                screenLocomotionDirection.Backward = InputNode.Key(Config.GetValue(KEY_BACKWARD));
                screenLocomotionDirection.Left = InputNode.Key(Config.GetValue(KEY_LEFT));
                screenLocomotionDirection.Right = InputNode.Key(Config.GetValue(KEY_RIGHT));
                screenLocomotionDirection.Up = InputNode.Key(Config.GetValue(KEY_JUMP));
                screenLocomotionDirection.Down = InputNode.Key(Config.GetValue(KEY_CROUCH));
                
                screenLocomotionDirection.Fast = InputNode.Any(new IInputNode<bool>[]
                {
                    InputNode.MultiTap(new KeyboardInputSource[]
                    {
                        InputNode.Key(Config.GetValue(KEY_FORWARD)),
                        InputNode.Key(Config.GetValue(KEY_LEFT)),
                        InputNode.Key(Config.GetValue(KEY_BACKWARD)),
                        InputNode.Key(Config.GetValue(KEY_RIGHT))
                    }, 2, 0.25f),
                    InputNode.Key(Config.GetValue(KEY_SPRINT))
                });
                
                screenLocomotionDirection.Slow = InputNode.Key(Config.GetValue(KEY_SLOW)).Toggle(null);
                
                if (overrideFast != null)
                    screenLocomotionDirection.FastMultiplier = overrideFast.Value;
                if (overrideSlow != null)
                    screenLocomotionDirection.SlowMultiplier = overrideSlow.Value;
                if (overrideReference != null)
                    screenLocomotionDirection.OverrideReference = new LocomotionReference?(overrideReference.Value);

                return screenLocomotionDirection;
            }
        }
    }
}