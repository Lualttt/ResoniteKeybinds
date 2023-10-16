using HarmonyLib;
using ResoniteModLoader;
using System;
using System.Reflection;
using Elements.Core;
using FrooxEngine;
using FrooxEngine.CommonAvatar;
using FrooxEngine.Undo;

namespace ResoniteKeybinds {
    public class ResoniteKeybinds : ResoniteMod
    {
        public override string Name => "ResoniteKeybinds";
        public override string Author => "Lualt";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/LualtOfficial/ResoniteKeybinds";
    
        // Movement
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_FORWARD = new ModConfigurationKey<Key>("keyForwad", "Forward", () => Key.W);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_BACKWARD = new ModConfigurationKey<Key>("keyBackward", "Backward", () => Key.S);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_LEFT = new ModConfigurationKey<Key>("keyLeft", "Left", () => Key.A);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_RIGHT = new ModConfigurationKey<Key>("keyRight", "Right", () => Key.D);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_SPRINT = new ModConfigurationKey<Key>("keySprint", "Sprint", () => Key.Shift);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_SLOW = new ModConfigurationKey<Key>("keySlow", "Slow", () => Key.Z);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_JUMP = new ModConfigurationKey<Key>("keyJump", "Jump", () => Key.Space);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> KEY_CROUCH = new ModConfigurationKey<Key>("keyCrouch", "Crouch", () => Key.C);
        
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER1 = new ModConfigurationKey<dummy>(" ", "");

        // Interactions
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<MouseButton> INTERACTION_INTERACT = new ModConfigurationKey<MouseButton>("interactionInteract", "Interact", () => MouseButton.Left);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<MouseButton> INTERACTION_GRAB = new ModConfigurationKey<MouseButton>("interactionGrab", "Grab", () => MouseButton.Right);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<MouseButton> INTERACTION_MENU_MOUSE = new ModConfigurationKey<MouseButton>("interactionMenuMouse", "Menu (mouse)", () => MouseButton.Middle);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> INTERACTION_MENU_KEYBOARD = new ModConfigurationKey<Key>("interactionMenuKeyboard", "Menu (keyboard)", () => Key.T);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<MouseButton> INTERACTION_SECONDARY_MOUSE = new ModConfigurationKey<MouseButton>("interactionSecondaryMouse", "Secondary (mouse)", () => MouseButton.Button4);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> INTERACTION_SECONDARY_KEYBOARD = new ModConfigurationKey<Key>("interactionSecondaryKeyboard", "Secondary (keyboard)", () => Key.R);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<MouseButton> INTERACTION_FOCUS_UI = new ModConfigurationKey<MouseButton>("interactionFocusUI", "Focus UI", () => MouseButton.Left);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> INTERACTION_FOCUS_UI_GATE = new ModConfigurationKey<Key>("interactionFocusUIGate", "Focus UI (gate)", () => Key.Control);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> INTERACTION_TOGGLE_EDIT_MODE = new ModConfigurationKey<Key>("interactionToggleEditMode", "Toggle Edit Mode", () => Key.F2);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER2 = new ModConfigurationKey<dummy>("  ", "");

        // Clipboard
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> CLIPBOARD_PASTE = new ModConfigurationKey<Key>("clipboardPaste", "Paste", () => Key.V);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> CLIPBOARD_PASTE_GATE = new ModConfigurationKey<Key>("clipboardPasteGate", "Paste (gate)", () => Key.Control);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER3 = new ModConfigurationKey<dummy>("   ", "");

        // Photo
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> PHOTO_TAKE = new ModConfigurationKey<Key>("photoTake", "Take Photo", () => Key.Print);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> PHOTO_TAKE_GATE = new ModConfigurationKey<Key>("photoTakeGate", "Take Photo (gate)", () => Key.Shift);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> PHOTO_TIMER = new ModConfigurationKey<Key>("photoTimer", "Start Timer Photo", () => Key.Print);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> PHOTO_TIMER_GATE = new ModConfigurationKey<Key>("photoTimerGate", "Start Timer Photo (gate)", () => Key.Control);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER4 = new ModConfigurationKey<dummy>("    ", "");

        // Laser
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<MouseButton> LASER_ALIGN = new ModConfigurationKey<MouseButton>("laserAlign", "Laser Hold Align", () => MouseButton.Left);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> LASER_ROTATE = new ModConfigurationKey<Key>("laserRotate", "Laser Hold Rotate", () => Key.E);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> LASER_SCALE = new ModConfigurationKey<Key>("laserScale", "Laser Hold Scale", () => Key.Shift);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER5 = new ModConfigurationKey<dummy>("     ", "");

        // Global
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> GLOBAL_DASH = new ModConfigurationKey<Key>("globalDash", "Toggle Dashboard", () => Key.Escape);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> GLOBAL_TALK_KEYBOARD = new ModConfigurationKey<Key>("globalTalkKeyboard", "Talk (keyboard)", () => Key.V);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> GLOBAL_TALK_KEYBOARD_GATE = new ModConfigurationKey<Key>("globalTalkKeyboardGate", "Talk (keyboard gate)", () => Key.Control);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<MouseButton> GLOBAL_TALK_MOUSE = new ModConfigurationKey<MouseButton>("globalTalkMouse", "Talk (mouse)", () => MouseButton.Button5);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> GLOBAL_MUTE = new ModConfigurationKey<Key>("globalMute", "Toggle Mute", () => Key.M);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER6 = new ModConfigurationKey<dummy>("      ", "");

        // Undo
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> UNDO_UNDO = new ModConfigurationKey<Key>("undoUndo", "Undo", () => Key.Z);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> UNDO_UNDO_GATE = new ModConfigurationKey<Key>("undoUndoGate", "Undo (gate)", () => Key.Control);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> UNDO_REDO = new ModConfigurationKey<Key>("undoRedo", "Redo", () => Key.Y);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> UNDO_REDO_GATE = new ModConfigurationKey<Key>("undoRedoGate", "Redo (gate)", () => Key.Control);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER7 = new ModConfigurationKey<dummy>("       ", "");

        // Screen
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> SCREEN_PRESPECTIVE = new ModConfigurationKey<Key>("screenPerspective", "Toggle Perspective", () => Key.F5);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> SCREEN_FREEFORM = new ModConfigurationKey<Key>("screenFreeform", "Toggle Freeform Camera", () => Key.F6);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> SCREEN_FOCUS = new ModConfigurationKey<Key>("screenFocus", "Focus", () => Key.F);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> SCREEN_FOCUS_GATE = new ModConfigurationKey<Key>("screenFocusGate", "Focus (gate)", () => Key.Control);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> SCREEN_UNFOCUS = new ModConfigurationKey<Key>("screeUnfocus", "Unfocus", () => Key.F);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> SCREEN_UNFOCUS_GATE = new ModConfigurationKey<Key>("screenUnfocusGate", "Unfocus (gate)", () => Key.Alt);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER8 = new ModConfigurationKey<dummy>("        ", "");

        // Head (so many controls oml, also hi :3)
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> HEAD_CROUCH = new ModConfigurationKey<Key>("headCrouch", "Crouch", () => Key.C);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER9 = new ModConfigurationKey<dummy>("         ", "");

        // General Locomotion
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> GENERAL_LOCOMOTION_NEXT = new ModConfigurationKey<Key>("generalLocomotionNext", "Next Module", () => Key.PageUp);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> GENERAL_LOCOMOTION_PREVIOUS = new ModConfigurationKey<Key>("generalLocomotionPrevious", "Previous Module", () => Key.PageDown);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER10 = new ModConfigurationKey<dummy>("          ", "");

        // Smooth Locomotion
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> SMOOTH_LOCOMOTION_JUMP = new ModConfigurationKey<Key>("smoothLocomotionJump", "Jump", () => Key.Space);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER11 = new ModConfigurationKey<dummy>("           ", "");

        // Anchor Locomotion
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> ANCHOR_LOCOMOTION_SECONDARY = new ModConfigurationKey<Key>("ancholLocomotionSecondary", "Anchor Lomocotion Secondary Action", () => Key.RightControl);

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<dummy> DUMMY_SPACER12 = new ModConfigurationKey<dummy>("            ", "");

        // Dev Tool
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> DEV_TOOL_FOCUS = new ModConfigurationKey<Key>("devToolFocus", "Dev Tool Focus", () => Key.F);
        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Key> DEV_TOOL_INSPECTOR = new ModConfigurationKey<Key>("devToolInspector", "Dev Tool Inspector", () => Key.I);

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

        public static ScreenLocomotionDirection GenerateScreenLocomotionDirection(float? overrideFast = null, float? overrideSlow = null, LocomotionReference? overrideReference = null)
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

        // ReSharper disable once InconsistentNaming
        [HarmonyPatch(typeof(KeyboardAndMouseBindingGenerator), "Bind")]
        class KeyboardAndMouseBindingGenerator_Bind_Patch
        {
            static void ReversePatch(object __instance, ref InputGroup group)
            {
                InteractionHandlerInputs interactionHandlerInputs = group as InteractionHandlerInputs;
                if (interactionHandlerInputs != null)
                {
                    interactionHandlerInputs.Interact.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.MouseButton(Config.GetValue(INTERACTION_INTERACT)), null), null, null, 0);
                    interactionHandlerInputs.Strength.AddBinding(InputNode.PrimarySecondary<float>(InputNode.MouseButton(Config.GetValue(INTERACTION_INTERACT)).ToAnalog(8f, CurvePreset.Smooth), null), null, null, 0);
                    interactionHandlerInputs.Grab.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.MouseButton(Config.GetValue(INTERACTION_GRAB)), null), null, null, 0);
                    interactionHandlerInputs.Menu.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.MouseButton(Config.GetValue(INTERACTION_MENU_MOUSE)), null), null, null, 0);
                    interactionHandlerInputs.Menu.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.Key(Config.GetValue(INTERACTION_MENU_KEYBOARD)), null), null, null, 0);
                    interactionHandlerInputs.Secondary.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.MouseButton(Config.GetValue(INTERACTION_SECONDARY_MOUSE)), null), null, null, 0);
                    interactionHandlerInputs.Secondary.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.Key(Config.GetValue(INTERACTION_SECONDARY_KEYBOARD)), null), null, null, 0);
                    interactionHandlerInputs.TouchAxis.AddBinding(InputNode.PrimarySecondary<float2>(InputNode.MouseScroll(false), null), null, null, 0);
                    interactionHandlerInputs.FocusUI.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.MouseButton(Config.GetValue(INTERACTION_FOCUS_UI)).Gate(InputNode.Key(Config.GetValue(INTERACTION_FOCUS_UI_GATE)), true, false), null), null, null, 10);
                    interactionHandlerInputs.ToggleEditMode.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.Key(Config.GetValue(INTERACTION_TOGGLE_EDIT_MODE)), null), null, null, 0);
                    return;
                }

                CommonActionsInputs commonActionsInputs = group as CommonActionsInputs;
                if (commonActionsInputs == null)
                {
                    if (!(group is ContextMenuInputs))
                    {
                        ClipboardInputs clipboardInputs = group as ClipboardInputs;
                        if (clipboardInputs != null)
                        {
                            clipboardInputs.Paste.AddBinding(InputNode.Key(Config.GetValue(CLIPBOARD_PASTE)).Gate(InputNode.Key(Config.GetValue(CLIPBOARD_PASTE_GATE)), true, false), null, null, 0);
                            return;
                        }

                        PhotoInputs photoInputs = group as PhotoInputs;
                        if (photoInputs != null)
                        {
                            photoInputs.TakePhoto.AddBinding(InputNode.Key(Config.GetValue(PHOTO_TAKE)).Gate(InputNode.Key(Config.GetValue(PHOTO_TAKE_GATE)), true, false), null, null, 0);
                            photoInputs.StartTimerPhoto.AddBinding(InputNode.Key(Config.GetValue(PHOTO_TIMER)).Gate(InputNode.Key(Config.GetValue(PHOTO_TIMER_GATE)), true, false), null, null, 0);
                            return;
                        }

                        LaserHoldInputs laserHoldInputs = group as LaserHoldInputs;
                        if (laserHoldInputs != null)
                        {
                            laserHoldInputs.Align.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.MouseButton(Config.GetValue(LASER_ALIGN)), null), null, null, 0);
                            laserHoldInputs.Slide.AddBinding(InputNode.PrimarySecondary<float>(InputNode.MouseScroll(true).Y().Multiply(InputNode.Setting<float>("Input.Screen.MouseGrabSensitivity", 15f)), null), null, null, 0);
                            laserHoldInputs.Rotate.AddBinding(InputNode.PrimarySecondary<float>(InputNode.MouseScroll(true).X().Multiply(InputNode.Setting<float>("Input.Screen.MouseGrabSensitivity", 15f)), null), null, null, 0);
                            laserHoldInputs.Rotate.AddBinding(InputNode.PrimarySecondary<float>(InputNode.MouseMovement(true).Multiply(InputNode.Setting<float>("Input.Screen.MouseRotateSensitivity", 45f)).X().Gate(InputNode.Key(Config.GetValue(LASER_ROTATE))), null), null, null, 0);
                            laserHoldInputs.FreeformRotateDelta.AddBinding(InputNode.PrimarySecondary<float3>(InputNode.XYZ(InputNode.MouseMovement(true).YX().Multiply(new float2(1f, -1f)).Multiply(InputNode.Setting<float>("Input.Screen.MouseFreeformRotateSensitivity", 360f)).Gate(InputNode.Key(Config.GetValue(LASER_ROTATE)), true, false), null, true), null), null, null, 0);
                            laserHoldInputs.FreezeCursor.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.Key(Config.GetValue(LASER_ROTATE)), null), null, null, 0);
                            laserHoldInputs.ScaleDelta.AddBinding(InputNode.PrimarySecondary<float>(InputNode.MouseScroll(true).Y().Gate(InputNode.Key(Config.GetValue(LASER_SCALE)), true, false), null), null, null, 10);
                            return;
                        }

                        GlobalActions globalActions = group as GlobalActions;
                        if (globalActions != null)
                        {
                            globalActions.ToggleDash.AddBinding(InputNode.Key(Config.GetValue(GLOBAL_DASH)), null, null, 0);
                            globalActions.ActivateTalk.AddBinding(InputNode.Key(Config.GetValue(GLOBAL_TALK_KEYBOARD)).Gate(InputNode.Key(Config.GetValue(GLOBAL_TALK_KEYBOARD_GATE)).Invert(), true, false), null, null, 0);
                            globalActions.ActivateTalk.AddBinding(InputNode.MouseButton(Config.GetValue(GLOBAL_TALK_MOUSE)), null, null, 0);
                            globalActions.ToggleMute.AddBinding(InputNode.Key(Config.GetValue(GLOBAL_MUTE)), null, null, 0);
                            return;
                        }

                        UndoInputs undoInputs = group as UndoInputs;
                        if (undoInputs != null)
                        {
                            undoInputs.Undo.AddBinding(InputNode.Key(Config.GetValue(UNDO_UNDO)).Gate(InputNode.Key(Config.GetValue(UNDO_UNDO_GATE)), true, false), null, null, 0);
                            undoInputs.Redo.AddBinding(InputNode.Key(Config.GetValue(UNDO_REDO)).Gate(InputNode.Key(Config.GetValue(UNDO_REDO_GATE)), true, false), null, null, 0);
                            return;
                        }

                        ScreenInputs screenInputs = group as ScreenInputs;
                        if (screenInputs != null)
                        {
                           screenInputs.ToggleFirstAndThirdPerson.AddBinding(InputNode.Key(Config.GetValue(SCREEN_PRESPECTIVE)), null, null, 0);
                           screenInputs.ToggleFreeformCamera.AddBinding(InputNode.Key(Config.GetValue(SCREEN_FREEFORM)), null, null, 0);
                           screenInputs.Focus.AddBinding(InputNode.Key(Config.GetValue(SCREEN_FOCUS)).Gate(InputNode.Key(Config.GetValue(SCREEN_FOCUS_GATE)), true, false), null, null, 0);
                           screenInputs.Unfocus.AddBinding(InputNode.Key(Config.GetValue(SCREEN_UNFOCUS)).Gate(InputNode.Key(Config.GetValue(SCREEN_UNFOCUS_GATE)), true, false), null, null, 0);
                            return;
                        }

                        ScreenCameraInputs screenCameraInputs = group as ScreenCameraInputs;
                        if (screenCameraInputs != null)
                        {
                            screenCameraInputs.Look.AddBinding(InputNode.MouseMovement(true).Multiply(InputNode.Setting<float>("Input.Screen.MouseLookSensitivity", 100f)).dT_inv<float2>(), null, null, 0);
                            screenCameraInputs.Pan.AddBinding(InputNode.MouseMovement(true).Gate(true, false, new IInputNode<bool>[]
                            {
                                InputNode.Key(Key.Control),
                                InputNode.MouseButton(MouseButton.Right)
                            }), null, null, 10);
                            screenCameraInputs.ResetPan.AddBinding(true.Constant<bool>().Gate(false, false, new IInputNode<bool>[]
                            {
                                InputNode.Key(Key.Control),
                                InputNode.MouseButton(MouseButton.Right)
                            }).MultiTap(2, 0.25f, true), null, null, 10);
                            screenCameraInputs.ForceHeadLook.AddBinding(InputNode.MouseButton(MouseButton.Right).NoBlocks<bool>(), null, null, 0);
                            screenCameraInputs.AlignHeadLook.AddBinding(InputNode.MouseButton(MouseButton.Right).MultiTap(2, 0.25f, false).NoBlocks<bool>(), null, null, 0);
                            return;
                        }

                        UICameraInputs uicameraInputs = group as UICameraInputs;
                        if (uicameraInputs != null)
                        {
                            uicameraInputs.LockCursor.AddBinding(true.Constant<bool>().Gate(true, true, new IInputNode<bool>[]
                            {
                                InputNode.Key(Key.Control),
                                InputNode.MouseButton(MouseButton.Right)
                            }), null, null, 0);
                            uicameraInputs.Zoom.AddBinding(InputNode.MouseScroll(true).Y().Negate<float>().dT_inv<float>().Gate(InputNode.Key(Key.Control), true, false), null, null, 0);
                            uicameraInputs.Pan.AddBinding(InputNode.MouseMovement(true).Multiply(InputNode.Setting<float>("Input.Screen.MousePanSensitivity", 1f)).dT_inv<float2>().Gate(InputNode.GroupAction<bool>("LockCursor"), true, false), null, null, 0);
                            return;
                        }

                        FreeformCameraInputs freeformCameraInputs = group as FreeformCameraInputs;
                        if (freeformCameraInputs != null)
                        {
                            freeformCameraInputs.LockCursor.AddBinding(true.Constant<bool>().Gate(true, true, new IInputNode<bool>[]
                            {
                                InputNode.Any(new IInputNode<bool>[]
                                {
                                    InputNode.Key(Key.Control),
                                    InputNode.Key(Key.Alt)
                                }),
                                InputNode.MouseButton(MouseButton.Right)
                            }), null, null, 0);
                            freeformCameraInputs.CameraLook.AddBinding(InputNode.MouseMovement(true).YX().XY_().Multiply(InputNode.Setting<float>("Input.Screen.MouseLookSensitivity", 100f)).dT_inv<float3>().Gate(InputNode.Key(Key.Alt).Invert(), false, false).Gate(InputNode.GroupAction<bool>("LockCursor"), true, false), null, null, 0);
                            freeformCameraInputs.CameraOrbit.AddBinding(InputNode.MouseMovement(true).Multiply(InputNode.Setting<float>("Input.Screen.MouseLookSensitivity", 100f)).dT_inv<float2>().Gate(InputNode.Key(Key.Alt), false, false).Gate(InputNode.GroupAction<bool>("LockCursor"), true, false), null, null, 0);
                            freeformCameraInputs.CameraMove.AddBinding(ResoniteKeybinds.GenerateScreenLocomotionDirection(new float?(4f), null, new LocomotionReference?(LocomotionReference.View)).Gate(InputNode.GroupAction<bool>("LockCursor"), true, false), null, null, 0);
                            freeformCameraInputs.CameraZoom.AddBinding(InputNode.MouseScroll(true).Y().Negate<float>().dT_inv<float>().Gate(InputNode.GroupAction<bool>("LockCursor"), true, false), null, null, 0);
                            return;
                        }

                        HeadInputs headInputs = group as HeadInputs;
                        if (headInputs != null)
                        {
                            headInputs.Crouch.AddBinding(InputNode.Any(new IInputNode<bool>[]
                            {
                               InputNode.Key(Config.GetValue(HEAD_CROUCH)),
                               InputNode.Key(Config.GetValue(HEAD_CROUCH)).TapToggle(0.15f)
                            }).ToAnalog(4f, CurvePreset.Smooth), null, null, 0);
                            return;
                        }

                        GeneralLocomotionInputs generalLocomotionInputs = group as GeneralLocomotionInputs;
                        if (generalLocomotionInputs != null)
                        {
                            generalLocomotionInputs.SelfScaleDelta.AddBinding(InputNode.MouseScroll(true).Y().Gate(InputNode.Key(Key.Control), true, false), null, null, 0);
                            generalLocomotionInputs.NextModule.AddBinding(InputNode.Key(Config.GetValue(GENERAL_LOCOMOTION_NEXT)), null, null, 0);
                            generalLocomotionInputs.PreviousModule.AddBinding(InputNode.Key(Config.GetValue(GENERAL_LOCOMOTION_PREVIOUS)), null, null, 0);
                            return;
                        }

                        SmoothLocomotionInputs smoothLocomotionInputs = group as SmoothLocomotionInputs;
                        if (smoothLocomotionInputs != null)
                        {
                            smoothLocomotionInputs.Move.AddBinding(ResoniteKeybinds.GenerateScreenLocomotionDirection(null, null, null), null, null, 0);
                            smoothLocomotionInputs.Jump.AddBinding(InputNode.Key(Config.GetValue(SMOOTH_LOCOMOTION_JUMP)), null, null, 0);
                            return;
                        }

                        SmoothThreeAxisLocomotionInputs smoothThreeAxisLocomotionInputs = group as SmoothThreeAxisLocomotionInputs;
                        if (smoothThreeAxisLocomotionInputs != null)
                        {
                            smoothThreeAxisLocomotionInputs.Move.AddBinding(ResoniteKeybinds.GenerateScreenLocomotionDirection(null, null, null), null, null, 0);
                            smoothThreeAxisLocomotionInputs.Jump.AddBinding(InputNode.Key(Config.GetValue(SMOOTH_LOCOMOTION_JUMP)), null, null, 0);
                            return;
                        }

                        AnchorReleaseInputs anchorReleaseInputs = group as AnchorReleaseInputs;
                        if (anchorReleaseInputs != null)
                        {
                            anchorReleaseInputs.Release.AddBinding(InputNode.Key(Config.GetValue(SMOOTH_LOCOMOTION_JUMP)), null, null, 0);
                            anchorReleaseInputs.ReleaseStrength.AddBinding(InputNode.Key(Config.GetValue(SMOOTH_LOCOMOTION_JUMP)).ToAnalog(4f, CurvePreset.Smooth), null, null, 0);
                            return;
                        }

                        AnchorLocomotionInputs anchorLocomotionInputs = group as AnchorLocomotionInputs;
                        if (anchorLocomotionInputs != null)
                        {
                            anchorLocomotionInputs.PrimaryAction.AddBinding(InputNode.Key(Config.GetValue(SMOOTH_LOCOMOTION_JUMP)), null, null, 0);
                            anchorLocomotionInputs.PrimaryAxis.AddBinding(InputNode.Axis(InputNode.Key(Config.GetValue(KEY_FORWARD)), InputNode.Key(Key.A), InputNode.Key(Key.S), InputNode.Key(Key.D)), null, null, 0);
                            anchorLocomotionInputs.SecondaryAction.AddBinding(InputNode.Key(Config.GetValue(ANCHOR_LOCOMOTION_SECONDARY)), null, null, 0);
                            anchorLocomotionInputs.SecondaryAxis.AddBinding(InputNode.Axis(InputNode.Key(Key.UpArrow), InputNode.Key(Key.LeftArrow), InputNode.Key(Key.DownArrow), InputNode.Key(Key.RightArrow)), null, null, 0);
                            return;
                        }

                        DevToolInputs devToolInputs = group as DevToolInputs;
                        if (devToolInputs != null)
                        {
                            devToolInputs.Focus.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.Key(Config.GetValue(DEV_TOOL_FOCUS)), null), null, null, 0);
                            devToolInputs.Inspector.AddBinding(InputNode.PrimarySecondary<bool>(InputNode.Key(Config.GetValue(DEV_TOOL_INSPECTOR)), null), null, null, 0);
                            return;
                        }

                        UniLog.Warning(string.Format("Cannot bind {0} to Keyboard & Mouse", group), false);
                    }
                }
            }
        }
    }
}