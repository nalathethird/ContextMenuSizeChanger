using System.Reflection;
using System.Reflection.Emit;

using ResoniteModLoader;
using HarmonyLib;

using FrooxEngine;
using FrooxEngine.UIX;
using Renderite.Shared;
using Elements.Core;

namespace ContextMenuCustomizer;
public class ContextMenuCustomizer : ResoniteMod {
	internal const string VERSION_CONSTANT = "1.0.1";
	public override string Name => "ContextMenuCustomizer";
	public override string Author => "NalaTheThird";
	public override string Version => VERSION_CONSTANT;
	public override string Link => "https://github.com/nalathethird/ContextMenuCustomizer";

	// === VISUAL CUSTOMIZATION ===
	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_MENU_SCALE = new ModConfigurationKey<float>(
		"MenuScale",
		"Visual scale multiplier for the context menu (default: 1.0, range: 0.1 to 3.0)",
		() => 1.0f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_ITEM_SEPARATION = new ModConfigurationKey<float>(
		"ItemSeparation",
		"Spacing between menu items in degrees (default: 6.0, range: 1.0 to 20.0)",
		() => 6.0f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_LABEL_WIDTH = new ModConfigurationKey<float>(
		"LabelWidth",
		"Width of menu item labels (default: 400, range: 100 to 800)",
		() => 400f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_LABEL_HEIGHT = new ModConfigurationKey<float>(
		"LabelHeight",
		"Height of menu item labels (default: 120, range: 50 to 300)",
		() => 120f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_INNER_RADIUS = new ModConfigurationKey<float>(
		"InnerRadiusRatio",
		"Size ratio of the inner circle (default: 0.5, range: 0.1 to 0.9)",
		() => 0.5f
	);

	// === COLOR CUSTOMIZATION ===
	[AutoRegisterConfigKey]
	private static ModConfigurationKey<color> KEY_TEXT_COLOR = new ModConfigurationKey<color>(
		"TextColor",
		"Color for menu item text (default: white)",
		() => new color(1f, 1f, 1f, 1f)
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<color> KEY_TEXT_OUTLINE_COLOR = new ModConfigurationKey<color>(
		"TextOutlineColor",
		"Color for text outline (default: black)",
		() => new color(0f, 0f, 0f, 1f)
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<color> KEY_ICON_TINT_COLOR = new ModConfigurationKey<color>(
		"IconTintColor",
		"Tint color for menu icons (default: white/no tint)",
		() => new color(1f, 1f, 1f, 1f)
	);

	// === ICON/SPRITE CUSTOMIZATION ===
	[AutoRegisterConfigKey]
	private static ModConfigurationKey<bool> KEY_CLAMP_ICONS = new ModConfigurationKey<bool>(
		"ClampIcons",
		"Use Clamp wrap mode for menu icons instead of Repeat (default: true)",
		() => true
	);

	// === ANIMATION CUSTOMIZATION ===
	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_OPEN_SPEED = new ModConfigurationKey<float>(
		"OpenSpeed",
		"Speed multiplier for menu opening animation (default: 1.0, range: 0.1 to 5.0)",
		() => 1.0f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_CLOSE_SPEED = new ModConfigurationKey<float>(
		"CloseSpeed",
		"Speed multiplier for menu closing animation (default: 1.0, range: 0.1 to 5.0)",
		() => 1.0f
	);

	// === INTERACTION CUSTOMIZATION ===
	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_EXIT_START = new ModConfigurationKey<float>(
		"ExitStartDistance",
		"Distance multiplier to start closing the menu (default: 1.0, range: 0.5 to 2.0)",
		() => 1.0f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_EXIT_END = new ModConfigurationKey<float>(
		"ExitEndDistance",
		"Distance multiplier to fully close the menu (default: 1.0, range: 0.5 to 2.0)",
		() => 1.0f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<bool> KEY_ACCEPT_PHYSICAL_TOUCH = new ModConfigurationKey<bool>(
		"AcceptPhysicalTouch",
		"Enable direct physical touch interaction (default: false, useful for touchscreens/Quest)",
		() => false
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<bool> KEY_ACCEPT_EXISTING_TOUCH = new ModConfigurationKey<bool>(
		"AcceptExistingTouch",
		"Enable touch interaction from existing touch sources (default: true)",
		() => true
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<bool> KEY_LASER_PASSTHROUGH = new ModConfigurationKey<bool>(
		"LaserPassThrough",
		"Only interact with menu items, not empty space (default: false, useful with touch/Quest)",
		() => false
	);

	// === TEXT CUSTOMIZATION ===
	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_TEXT_SIZE = new ModConfigurationKey<float>(
		"TextSize",
		"Font size for menu item text (default: 50, range: 20 to 100)",
		() => 50f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_TEXT_OUTLINE = new ModConfigurationKey<float>(
		"TextOutlineThickness",
		"Thickness of text outline (default: 0.2, range: 0.0 to 1.0)",
		() => 0.2f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_TEXT_LINE_HEIGHT = new ModConfigurationKey<float>(
		"TextLineHeight",
		"Line spacing for multi-line text (default: 0.8, range: 0.5 to 2.0)",
		() => 0.8f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<string> KEY_CUSTOM_FONT_URL = new ModConfigurationKey<string>(
		"CustomFontURL",
		"Custom font URL (resdb:// or other, leave empty for default)",
		() => ""
	);

	// === ARC VISUAL CUSTOMIZATION ===
	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_ARC_OUTLINE_THICKNESS = new ModConfigurationKey<float>(
		"ArcOutlineThickness",
		"Thickness of menu item arc outlines (default: 3.0, range: 0.0 to 10.0)",
		() => 3.0f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_ARC_CORNER_RADIUS = new ModConfigurationKey<float>(
		"ArcCornerRadius",
		"Corner radius for menu item arcs (default: 16.0, range: 0.0 to 50.0)",
		() => 16.0f
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_MENU_OPACITY = new ModConfigurationKey<float>(
		"MenuOpacity",
		"Overall opacity/transparency of the menu (default: 1.0, range: 0.1 to 1.0)",
		() => 1.0f
	);

	private static ModConfiguration Config;
	
	// Track when settings need refresh
	private static bool _needsMaterialRefresh = false;

	public override void OnEngineInit() {
		Config = GetConfiguration();
		Config.OnThisConfigurationChanged += OnConfigurationChanged;

		Harmony harmony = new("com.NalaTheThird.ContextMenuCustomizer");
		harmony.PatchAll();
	}
	
	private void OnConfigurationChanged(ConfigurationChangedEvent configurationChangedEvent) {
		// Clamp all values to reasonable ranges
		ClampConfig(KEY_MENU_SCALE, 0.1f, 3.0f);
		ClampConfig(KEY_ITEM_SEPARATION, 1.0f, 20.0f);
		ClampConfig(KEY_LABEL_WIDTH, 100f, 800f);
		ClampConfig(KEY_LABEL_HEIGHT, 50f, 300f);
		ClampConfig(KEY_INNER_RADIUS, 0.1f, 0.9f);
		ClampConfig(KEY_OPEN_SPEED, 0.1f, 5.0f);
		ClampConfig(KEY_CLOSE_SPEED, 0.1f, 5.0f);
		ClampConfig(KEY_EXIT_START, 0.5f, 2.0f);
		ClampConfig(KEY_EXIT_END, 0.5f, 2.0f);
		ClampConfig(KEY_TEXT_SIZE, 20f, 100f);
		ClampConfig(KEY_TEXT_OUTLINE, 0.0f, 1.0f);
		ClampConfig(KEY_TEXT_LINE_HEIGHT, 0.5f, 2.0f);
		ClampConfig(KEY_ARC_OUTLINE_THICKNESS, 0.0f, 10.0f);
		ClampConfig(KEY_ARC_CORNER_RADIUS, 0.0f, 50.0f);
		ClampConfig(KEY_MENU_OPACITY, 0.1f, 1.0f);
		
		_needsMaterialRefresh = true;
	}

	private void ClampConfig(ModConfigurationKey<float> key, float min, float max) {
		float value = Config.GetValue(key);
		if (value < min || value > max) {
			Config.Set(key, MathX.Clamp(value, min, max));
		}
	}

	// Cache to track which context menus we've already scaled
	private static Dictionary<ContextMenu, FrooxEngine.Slot> _visualSlotCache = new Dictionary<ContextMenu, FrooxEngine.Slot>();
	
	// Cache for shared custom font component (one per ContextMenu)
	private static Dictionary<ContextMenu, StaticFont> _customFontCache = new Dictionary<ContextMenu, StaticFont>();

	// === OnAttach - Set initial values and customize materials ===
	[HarmonyPatch(typeof(ContextMenu), "OnAttach")]
	class ContextMenu_OnAttach_Patch {
		static void Postfix(ContextMenu __instance) {
			if (__instance.Slot.ActiveUserRoot.ActiveUser != __instance.LocalUser) return;

			// Find and cache the Visual slot
			FrooxEngine.Slot visualSlot = __instance.Slot.FindChild("Visual");
			if (visualSlot != null) {
				_visualSlotCache[__instance] = visualSlot;
			}

			// Apply custom values to Separation, LabelSize, and RadiusRatio
			float separation = Config?.GetValue(KEY_ITEM_SEPARATION) ?? 6.0f;
			float labelWidth = Config?.GetValue(KEY_LABEL_WIDTH) ?? 400f;
			float labelHeight = Config?.GetValue(KEY_LABEL_HEIGHT) ?? 120f;
			float innerRadius = Config?.GetValue(KEY_INNER_RADIUS) ?? 0.5f;

			__instance.Separation.Value = separation;
			__instance.LabelSize.Value = new float2(labelWidth, labelHeight);
			__instance.RadiusRatio.Value = innerRadius;

			// Apply material customizations
			RefreshMenuMaterials(__instance);

			// Configure touch interaction settings
			var canvas = __instance.GetSyncMember(9) as SyncRef<Canvas>;
			if (canvas?.Target != null) {
				bool acceptPhysicalTouch = Config?.GetValue(KEY_ACCEPT_PHYSICAL_TOUCH) ?? false;
				bool acceptExistingTouch = Config?.GetValue(KEY_ACCEPT_EXISTING_TOUCH) ?? true;
				
				canvas.Target.AcceptPhysicalTouch.Value = acceptPhysicalTouch;
				canvas.Target.AcceptExistingTouch.Value = acceptExistingTouch;
			}
			
			// Create shared custom font component if needed
			string customFontUrl = Config?.GetValue(KEY_CUSTOM_FONT_URL) ?? "";
			if (!string.IsNullOrWhiteSpace(customFontUrl)) {
				try {
					Uri fontUri;
					if (Uri.TryCreate(customFontUrl, UriKind.Absolute, out fontUri)) {
						__instance.World.RunSynchronously(() => {
							var fontSlot = __instance.Slot.FindChild("SharedCustomFont");
							if (fontSlot == null) {
								fontSlot = __instance.Slot.AddSlot("SharedCustomFont");
							}
							
							var staticFont = fontSlot.GetComponent<StaticFont>();
							if (staticFont == null) {
								staticFont = fontSlot.AttachComponent<StaticFont>();
							}
							
							staticFont.URL.Value = fontUri;
							_customFontCache[__instance] = staticFont;
						});
					}
				} catch (Exception e) {
					Warn($"Failed to create shared custom font: {e.Message}");
				}
			}
		}
	}
	
	// === HELPER: Refresh material settings ===
	private static void RefreshMenuMaterials(ContextMenu menu) {
		try {
			// Customize text material
			float textOutline = Config?.GetValue(KEY_TEXT_OUTLINE) ?? 0.2f;
			var fontMaterial = menu.GetSyncMember(23) as SyncRef<UI_TextUnlitMaterial>;
			if (fontMaterial?.Target != null) {
				fontMaterial.Target.OutlineThickness.Value = textOutline;
				
				// Set custom text outline color
				var outlineColor = Config?.GetValue(KEY_TEXT_OUTLINE_COLOR) ?? new color(0f, 0f, 0f, 1f);
				fontMaterial.Target.OutlineColor.Value = new colorX(outlineColor.r, outlineColor.g, outlineColor.b, outlineColor.a);
			}
		} catch (Exception e) {
			Error($"Exception refreshing menu materials: {e}");
		}
	}

	// === PATCH: OpenMenuIntern - Refresh settings before menu opens ===
	[HarmonyPatch(typeof(ContextMenu), "OpenMenuIntern")]
	class ContextMenu_OpenMenuIntern_Patch {
		static bool Prepare() {
			// Only patch if the method exists
			return AccessTools.Method(typeof(ContextMenu), "OpenMenuIntern") != null;
		}
		
		static void Prefix(ContextMenu __instance) {
			if (__instance.Slot.ActiveUserRoot.ActiveUser != __instance.LocalUser) return;
			try {
				var canvas = __instance.GetSyncMember(9) as SyncRef<Canvas>;
				if (canvas?.Target != null) {
					__instance.World.RunSynchronously(() => {
						// Refresh materials if config changed
						if (_needsMaterialRefresh) {
							RefreshMenuMaterials(__instance);
							_needsMaterialRefresh = false;
						}
						
						// Apply all canvas settings (touch + laser passthrough)
						// Doing this on every menu open ensures settings are always current
						bool acceptPhysicalTouch = Config?.GetValue(KEY_ACCEPT_PHYSICAL_TOUCH) ?? false;
						bool acceptExistingTouch = Config?.GetValue(KEY_ACCEPT_EXISTING_TOUCH) ?? true;
						bool laserPassThrough = Config?.GetValue(KEY_LASER_PASSTHROUGH) ?? false;
						
						canvas.Target.AcceptPhysicalTouch.Value = acceptPhysicalTouch;
						canvas.Target.AcceptExistingTouch.Value = acceptExistingTouch;
						canvas.Target.LaserPassThrough.Value = laserPassThrough;
					}, immediatellyIfPossible: true);
				}
			} catch (Exception e) {
				Error($"Exception in OpenMenuIntern prefix: {e}");
			}
		}
	}

	// === PATCH: OnChanges - Apply visual scale and opacity ===
	[HarmonyPatch(typeof(ContextMenu), "OnChanges")]
	class ContextMenu_OnChanges_Patch {
		static void Postfix(ContextMenu __instance) {
			if (__instance.Slot.ActiveUserRoot.ActiveUser != __instance.LocalUser) return;

			// Get the configured scale multiplier
			float scaleMultiplier = Config?.GetValue(KEY_MENU_SCALE) ?? 1.0f;
			float menuOpacity = Config?.GetValue(KEY_MENU_OPACITY) ?? 1.0f;
			
			// Get the cached Visual slot, or try to find it if not cached
			FrooxEngine.Slot visualSlot = null;
			if (!_visualSlotCache.TryGetValue(__instance, out visualSlot)) {
				visualSlot = __instance.Slot.FindChild("Visual");
				if (visualSlot != null) {
					_visualSlotCache[__instance] = visualSlot;
				}
			}
			
			if (visualSlot != null && !visualSlot.IsRemoved) {
				// The original code sets scale to: float3.One * (0.2f / canvas.Size.Value.y)
				// We multiply by our scale multiplier to scale the entire menu
				float baseScale = 0.2f / 512f; // Canvas size is 512x512
				visualSlot.LocalScale = float3.One * (baseScale * scaleMultiplier);
			} else if (visualSlot != null && visualSlot.IsRemoved) {
				// Clean up both caches if the slot was removed
				_visualSlotCache.Remove(__instance);
				_customFontCache.Remove(__instance);
			}

			// Apply opacity to color fades if different from default
			if (!MathX.Approximately(menuOpacity, 1.0f)) {
				var fillFade = __instance.GetSyncMember(37) as FieldDrive<colorX>;
				var outlineFade = __instance.GetSyncMember(38) as FieldDrive<colorX>;
				var textFade = __instance.GetSyncMember(39) as FieldDrive<colorX>;
				var iconFade = __instance.GetSyncMember(40) as FieldDrive<colorX>;

				// Multiply alpha by opacity for all fade drives
				if (fillFade?.Target != null) {
					var currentColor = fillFade.Target.Value;
					fillFade.Target.Value = currentColor.SetA(currentColor.a * menuOpacity);
				}
				if (outlineFade?.Target != null) {
					var currentColor = outlineFade.Target.Value;
					outlineFade.Target.Value = currentColor.SetA(currentColor.a * menuOpacity);
				}
				if (textFade?.Target != null) {
					var currentColor = textFade.Target.Value;
					textFade.Target.Value = currentColor.SetA(currentColor.a * menuOpacity);
				}
				if (iconFade?.Target != null) {
					var currentColor = iconFade.Target.Value;
					iconFade.Target.Value = currentColor.SetA(currentColor.a * menuOpacity);
				}
			}
		}
	}

	// === PATCH: OnCommonUpdate - Customize animation speeds ===
	[HarmonyPatch(typeof(ContextMenu), "OnCommonUpdate")]
	class ContextMenu_OnCommonUpdate_Patch {
		static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
			var codes = new List<CodeInstruction>(instructions);
			int patchCount = 0;

			for (int i = 0; i < codes.Count; i++) {
				// Look for the animation speed constants (6f for OPEN_SPEED and CLOSE_SPEED)
				if (codes[i].opcode == OpCodes.Ldc_R4 && codes[i].operand is float floatValue) {
					// Check if this is one of the speed constants
					if (MathX.Approximately(floatValue, 6f)) {
						// NOTE: The first occurrence is OPEN speed, second is CLOSE speed
						// (verified by testing - it was backwards before!)
						if (patchCount == 0) {
							// First occurrence - this is OPEN speed
							codes[i] = new CodeInstruction(OpCodes.Call,
								AccessTools.Method(typeof(ContextMenu_OnCommonUpdate_Patch), nameof(GetOpenSpeed)));
							patchCount++;
						} else if (patchCount == 1) {
							// Second occurrence - this is CLOSE speed
							codes[i] = new CodeInstruction(OpCodes.Call,
								AccessTools.Method(typeof(ContextMenu_OnCommonUpdate_Patch), nameof(GetCloseSpeed)));
							patchCount++;
						}
					}
					// Exit distance constants - these need smart adjustment for physical touch
					else if (MathX.Approximately(floatValue, 0.9775f)) {
						codes[i] = new CodeInstruction(OpCodes.Call,
							AccessTools.Method(typeof(ContextMenu_OnCommonUpdate_Patch), nameof(GetExitStart)));
						patchCount++;
					}
					else if (MathX.Approximately(floatValue, 1.4875001f)) {
						codes[i] = new CodeInstruction(OpCodes.Call,
							AccessTools.Method(typeof(ContextMenu_OnCommonUpdate_Patch), nameof(GetExitEnd)));
						patchCount++;
					}
				}
			}

			Msg($"Patched {patchCount} animation/interaction constants in OnCommonUpdate");
			return codes.AsEnumerable();
		}

		static float GetCloseSpeed() {
			float multiplier = Config?.GetValue(KEY_CLOSE_SPEED) ?? 1.0f;
			return 6f * multiplier;
		}

		static float GetOpenSpeed() {
			float multiplier = Config?.GetValue(KEY_OPEN_SPEED) ?? 1.0f;
			return 6f * multiplier;
		}

		static float GetExitStart() {
			// Smart exit distance for physical touch
			// If physical touch is enabled, we need larger distances so hand can reach buttons
			// Otherwise use user's configured multiplier
			bool physicalTouchEnabled = Config?.GetValue(KEY_ACCEPT_PHYSICAL_TOUCH) ?? false;
			float userMultiplier = Config?.GetValue(KEY_EXIT_START) ?? 1.0f;
			
			if (physicalTouchEnabled) {
				// For physical touch: use larger distance (min 2.0x) to allow hand to reach
				// But respect user's setting if they want even larger
				float touchMultiplier = MathX.Max(2.0f, userMultiplier);
				return 0.9775f * touchMultiplier;
			}
			
			// For laser/non-touch: use user's configured multiplier
			return 0.9775f * userMultiplier;
		}

		static float GetExitEnd() {
			// Smart exit distance for physical touch
			// Similar logic to ExitStart but with slightly larger multiplier
			bool physicalTouchEnabled = Config?.GetValue(KEY_ACCEPT_PHYSICAL_TOUCH) ?? false;
			float userMultiplier = Config?.GetValue(KEY_EXIT_END) ?? 1.0f;
			
			if (physicalTouchEnabled) {
				// For physical touch: use larger distance (min 2.5x) for full close
				// This ensures menu closes when hand moves far away
				float touchMultiplier = MathX.Max(2.5f, userMultiplier);
				return 1.4875001f * touchMultiplier;
			}
			
			// For laser/non-touch: use user's configured multiplier
			return 1.4875001f * userMultiplier;
		}
	}

	// === PATCH: AddItem - THE ULTIMATE CUSTOMIZATION ===
	[HarmonyPatch]
	class ContextMenu_AddItem_Patch {
		static IEnumerable<MethodBase> TargetMethods() {
			// Find the private AddItem method with the specific signature
			return AccessTools.GetDeclaredMethods(typeof(ContextMenu))
				.Where(m => m.Name == "AddItem" && 
					m.GetParameters().Length == 6 &&
					m.GetParameters()[0].ParameterType.IsByRef &&
					m.GetParameters()[4].ParameterType.IsByRef);
		}

		static void Postfix(ContextMenu __instance, ContextMenuItem __result) {
			if (__instance.Slot.ActiveUserRoot.ActiveUser != __instance.LocalUser) return;
			if (__result == null) return;

			try {
				// Get config values
				float textSize = Config?.GetValue(KEY_TEXT_SIZE) ?? 50f;
				float arcOutline = Config?.GetValue(KEY_ARC_OUTLINE_THICKNESS) ?? 3.0f;
				float arcCorner = Config?.GetValue(KEY_ARC_CORNER_RADIUS) ?? 16.0f;
				bool clampIcons = Config?.GetValue(KEY_CLAMP_ICONS) ?? true;
				var textColor = Config?.GetValue(KEY_TEXT_COLOR) ?? new color(1f, 1f, 1f, 1f);
				float lineHeight = Config?.GetValue(KEY_TEXT_LINE_HEIGHT) ?? 0.8f;
				var iconTint = Config?.GetValue(KEY_ICON_TINT_COLOR) ?? new color(1f, 1f, 1f, 1f);
				
				// Find the text component
				var text = __result.Slot.GetComponentInChildren<Text>();
				if (text != null) {
					// Apply text customizations
					text.Size.Value = textSize;
					text.AutoSizeMax.Value = textSize;
					text.Color.Value = new colorX(textColor.r, textColor.g, textColor.b, textColor.a);
					text.LineHeight.Value = lineHeight;

					// Apply shared custom font if available
					if (_customFontCache.TryGetValue(__instance, out StaticFont sharedFont) && sharedFont != null) {
						text.Font.Target = sharedFont;
					}
				}

				// Find the arc component and apply visual settings
				var arc = __result.Slot.GetComponentInChildren<OutlinedArc>();
				if (arc != null) {
					arc.OutlineThickness.Value = arcOutline;
					arc.RoundedCornerRadius.Value = arcCorner;
				}
				
				// Apply icon tint
				var image = __result.Icon?.Target;
				if (image != null) {
					// Only apply tint if not driven by ContextMenuItemSource
					if (!image.Tint.IsDriven) {
						image.Tint.Value = new colorX(iconTint.r, iconTint.g, iconTint.b, iconTint.a);
					}
					
					// Fix icon wrap mode to Clamp instead of Repeat
					if (clampIcons && image.Sprite.Target != null) {
						var spriteProvider = image.Sprite.Target as SpriteProvider;
						if (spriteProvider?.Texture.Target != null) {
							var texture = spriteProvider.Texture.Target as StaticTexture2D;
							if (texture != null) {
								texture.WrapModeU.Value = TextureWrapMode.Clamp;
								texture.WrapModeV.Value = TextureWrapMode.Clamp;
							}
						}
					}
				}
			} catch (Exception e) {
				Error($"Exception in AddItem patch: {e}");
			}
		}
	}
}
