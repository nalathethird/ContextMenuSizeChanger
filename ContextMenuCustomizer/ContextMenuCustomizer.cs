using System.Reflection;
using System.Reflection.Emit;

using ResoniteModLoader;
using HarmonyLib;

using FrooxEngine;
using Renderite.Shared;
using FrooxEngine.UIX;
using Elements.Core;

namespace ContextMenuCustomizer;
public class ContextMenuCustomizer : ResoniteMod {
	internal const string VERSION_CONSTANT = "1.0.0";
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
	private static ModConfigurationKey<color> KEY_ARC_FILL_COLOR = new ModConfigurationKey<color>(
		"ArcFillColor",
		"Fill color for menu item backgrounds (default: disabled, set alpha > 0 to enable)",
		() => new color(0f, 0f, 0f, 0f)
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<color> KEY_ARC_OUTLINE_COLOR = new ModConfigurationKey<color>(
		"ArcOutlineColor",
		"Color for menu item borders (default: disabled, set alpha > 0 to enable)",
		() => new color(0f, 0f, 0f, 0f)
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<color> KEY_ICON_TINT_COLOR = new ModConfigurationKey<color>(
		"IconTintColor",
		"Tint color for menu icons (default: white/no tint)",
		() => new color(1f, 1f, 1f, 1f)
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<bool> KEY_ENABLE_GRADIENT = new ModConfigurationKey<bool>(
		"EnableGradient",
		"Enable color gradient on menu items (default: false)",
		() => false
	);

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<color> KEY_GRADIENT_COLOR = new ModConfigurationKey<color>(
		"GradientColor",
		"Secondary color for gradient effect (requires EnableGradient)",
		() => new color(0.5f, 0.5f, 1f, 1f)
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

	public override void OnEngineInit() {
		Config = GetConfiguration();
		Config.OnThisConfigurationChanged += OnConfigurationChanged;

		Harmony harmony = new("com.nalathethird.ContextMenuCustomizer");
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
	}

	private void ClampConfig(ModConfigurationKey<float> key, float min, float max) {
		float value = Config.GetValue(key);
		if (value < min || value > max) {
			Config.Set(key, MathX.Clamp(value, min, max));
		}
	}

	// Cache to track which context menus we've already scaled
	private static Dictionary<ContextMenu, FrooxEngine.Slot> _visualSlotCache = new Dictionary<ContextMenu, FrooxEngine.Slot>();

	// === PATCH: OnAttach - Set initial values and customize materials ===
	[HarmonyPatch(typeof(ContextMenu), "OnAttach")]
	class ContextMenu_OnAttach_Patch {
		static void Postfix(ContextMenu __instance) {
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

			// Customize text material
			float textOutline = Config?.GetValue(KEY_TEXT_OUTLINE) ?? 0.2f;
			var fontMaterial = __instance.GetSyncMember(23) as SyncRef<UI_TextUnlitMaterial>;
			if (fontMaterial?.Target != null) {
				fontMaterial.Target.OutlineThickness.Value = textOutline;
				
				// Set custom text outline color
				var outlineColor = Config?.GetValue(KEY_TEXT_OUTLINE_COLOR) ?? new color(0f, 0f, 0f, 1f);
				fontMaterial.Target.OutlineColor.Value = new colorX(outlineColor.r, outlineColor.g, outlineColor.b, outlineColor.a);
			}

			// Configure touch interaction settings
			var canvas = __instance.GetSyncMember(9) as SyncRef<Canvas>;
			if (canvas?.Target != null) {
				bool acceptPhysicalTouch = Config?.GetValue(KEY_ACCEPT_PHYSICAL_TOUCH) ?? false;
				bool acceptExistingTouch = Config?.GetValue(KEY_ACCEPT_EXISTING_TOUCH) ?? true;
				
				canvas.Target.AcceptPhysicalTouch.Value = acceptPhysicalTouch;
				canvas.Target.AcceptExistingTouch.Value = acceptExistingTouch;
				
				Msg($"Touch settings - Physical: {acceptPhysicalTouch}, Existing: {acceptExistingTouch}");
			}
		}
	}

	// === PATCH: OnChanges - Apply visual scale and opacity ===
	[HarmonyPatch(typeof(ContextMenu), "OnChanges")]
	class ContextMenu_OnChanges_Patch {
		static void Postfix(ContextMenu __instance) {
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
				// Clean up the cache if the slot was removed
				_visualSlotCache.Remove(__instance);
			}

			// Apply opacity to color fades if different from default
			if (!MathX.Approximately(menuOpacity, 1.0f)) {
				var fillFade = __instance.GetSyncMember(36) as FieldDrive<colorX>;
				var outlineFade = __instance.GetSyncMember(37) as FieldDrive<colorX>;
				var textFade = __instance.GetSyncMember(38) as FieldDrive<colorX>;
				var iconFade = __instance.GetSyncMember(39) as FieldDrive<colorX>;

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
						// We need to determine if this is open or close speed based on context
						// The close speed appears first in the method, open speed appears later
						if (patchCount == 0) {
							// First occurrence - this is close speed
							codes[i] = new CodeInstruction(OpCodes.Call,
								AccessTools.Method(typeof(ContextMenu_OnCommonUpdate_Patch), nameof(GetCloseSpeed)));
							patchCount++;
						} else if (patchCount == 1) {
							// Second occurrence - this is open speed
							codes[i] = new CodeInstruction(OpCodes.Call,
								AccessTools.Method(typeof(ContextMenu_OnCommonUpdate_Patch), nameof(GetOpenSpeed)));
							patchCount++;
						}
					}
					// Exit distance constants
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
			float multiplier = Config?.GetValue(KEY_EXIT_START) ?? 1.0f;
			return 0.9775f * multiplier;
		}

		static float GetExitEnd() {
			float multiplier = Config?.GetValue(KEY_EXIT_END) ?? 1.0f;
			return 1.4875001f * multiplier;
		}
	}

	// === PATCH: AddItem - THE ULTIMATE CUSTOMIZATION ===
	[HarmonyPatch(typeof(ContextMenu), "AddItem")]
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
			if (__result == null) return;

			try {
				// Get config values
				float textSize = Config?.GetValue(KEY_TEXT_SIZE) ?? 50f;
				float arcOutline = Config?.GetValue(KEY_ARC_OUTLINE_THICKNESS) ?? 3.0f;
				float arcCorner = Config?.GetValue(KEY_ARC_CORNER_RADIUS) ?? 16.0f;
				bool clampIcons = Config?.GetValue(KEY_CLAMP_ICONS) ?? true;
				var textColor = Config?.GetValue(KEY_TEXT_COLOR) ?? new color(1f, 1f, 1f, 1f);
				float lineHeight = Config?.GetValue(KEY_TEXT_LINE_HEIGHT) ?? 0.8f;
				string customFontUrl = Config?.GetValue(KEY_CUSTOM_FONT_URL) ?? "";
				
				// Arc colors
				var arcFillColor = Config?.GetValue(KEY_ARC_FILL_COLOR) ?? new color(0f, 0f, 0f, 0f);
				var arcOutlineColor = Config?.GetValue(KEY_ARC_OUTLINE_COLOR) ?? new color(0f, 0f, 0f, 0f);
				var iconTint = Config?.GetValue(KEY_ICON_TINT_COLOR) ?? new color(1f, 1f, 1f, 1f);
				
				// Gradient settings
				bool enableGradient = Config?.GetValue(KEY_ENABLE_GRADIENT) ?? false;
				var gradientColor = Config?.GetValue(KEY_GRADIENT_COLOR) ?? new color(0.5f, 0.5f, 1f, 1f);

				// Find the text component
				var text = __result.Slot.GetComponentInChildren<Text>();
				if (text != null) {
					// Apply text customizations
					text.Size.Value = textSize;
					text.AutoSizeMax.Value = textSize;
					text.Color.Value = new colorX(textColor.r, textColor.g, textColor.b, textColor.a);
					text.LineHeight.Value = lineHeight;

					// Apply custom font if specified
					if (!string.IsNullOrWhiteSpace(customFontUrl)) {
						try {
							Uri fontUri;
							if (Uri.TryCreate(customFontUrl, UriKind.Absolute, out fontUri)) {
								var world = __result.World;
								world.RunSynchronously(() => {
									var fontSlot = __result.Slot.FindChild("CustomFont");
									if (fontSlot == null) {
										fontSlot = __result.Slot.AddSlot("CustomFont");
									}
									
									var staticFont = fontSlot.GetComponent<StaticFont>();
									if (staticFont == null) {
										staticFont = fontSlot.AttachComponent<StaticFont>();
									}
									
									staticFont.URL.Value = fontUri;
									text.Font.Target = staticFont;
								});
							}
						} catch (Exception e) {
							Warn($"Failed to load custom font from {customFontUrl}: {e.Message}");
						}
					}
				}

				// Find the arc component and apply colors
				var arc = __result.Slot.GetComponentInChildren<OutlinedArc>();
				if (arc != null) {
					arc.OutlineThickness.Value = arcOutline;
					arc.RoundedCornerRadius.Value = arcCorner;
					
					// Get the arc's material (UI_CircleSegment) for color customization
					var arcMaterial = arc.Material.Target as UI_CircleSegment;
					
					// Only apply custom arc colors if:
					// 1. Alpha > 0 (user wants custom colors)
					// 2. The material's tint fields are NOT driven (no ContextMenuItemSource override)
					
					if (arcMaterial != null) {
						// Apply arc fill color if alpha > 0 and not driven
						if (arcFillColor.a > 0f && !arcMaterial.FillTint.IsDriven) {
							arcMaterial.FillTint.Value = new colorX(arcFillColor.r, arcFillColor.g, arcFillColor.b, arcFillColor.a);
						}
						
						// Apply arc outline color if alpha > 0 and not driven
						if (arcOutlineColor.a > 0f && !arcMaterial.OutlineTint.IsDriven) {
							arcMaterial.OutlineTint.Value = new colorX(arcOutlineColor.r, arcOutlineColor.g, arcOutlineColor.b, arcOutlineColor.a);
						}
					}
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

				// Apply gradient effect if enabled and material not driven
				if (enableGradient && arc != null) {
					var arcMaterial = arc.Material.Target as UI_CircleSegment;
					if (arcMaterial != null && !arcMaterial.FillTint.IsDriven) {
						__result.World.RunSynchronously(() => {
							// Create a gradient by adding a gradient overlay
							var gradientSlot = __result.Slot.FindChild("GradientEffect");
							if (gradientSlot == null) {
								gradientSlot = __result.Slot.AddSlot("GradientEffect");
								var gradientImage = gradientSlot.AttachComponent<Image>();
								
								// Position gradient overlay
								var rectTransform = gradientSlot.AttachComponent<RectTransform>();
								rectTransform.AnchorMin.Value = float2.Zero;
								rectTransform.AnchorMax.Value = float2.One;
								rectTransform.OffsetMin.Value = float2.Zero;
								rectTransform.OffsetMax.Value = float2.Zero;
								
								// Create gradient material
								var gradientMat = gradientSlot.AttachComponent<UI_UnlitMaterial>();
								gradientMat.Tint.Value = new colorX(gradientColor.r, gradientColor.g, gradientColor.b, gradientColor.a * 0.3f);
								gradientImage.Material.Target = gradientMat;
								
								gradientMat.BlendMode.Value = BlendMode.Additive;
							}
						});
					}
				}

			} catch (Exception e) {
				Error($"Exception in AddItem patch: {e}");
			}
		}
	}
}
