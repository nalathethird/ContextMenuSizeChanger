using FrooxEngine;
using HarmonyLib;
using ResoniteModLoader;
using Elements.Core;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Linq;

namespace ContextMenuSizeChanger;
public class ContextMenuSizeChanger : ResoniteMod {
	internal const string VERSION_CONSTANT = "1.0.0";
	public override string Name => "ContextMenuSizeChanger";
	public override string Author => "NalaTheThird";
	public override string Version => VERSION_CONSTANT;
	public override string Link => "https://github.com/nalathethird/ContextMenuSizeChanger";

	[AutoRegisterConfigKey]
	private static ModConfigurationKey<float> KEY_MENU_SIZE = new ModConfigurationKey<float>(
		"MenuSize",
		"The size multiplier for the context menu (default is 0.85, range: 0.1 to 2.0)",
		() => 0.85f
	);

	private static ModConfiguration Config;

	public override void OnEngineInit() {
		Config = GetConfiguration();
		Config.OnThisConfigurationChanged += OnConfigurationChanged;

		Harmony harmony = new("com.nalathethird.ContextMenuSizeChanger");
		harmony.PatchAll();
	}
	private void OnConfigurationChanged(ConfigurationChangedEvent configurationChangedEvent) {
		// Clamp the value to a reasonable range
		float value = Config.GetValue(KEY_MENU_SIZE);
		if (value < 0.1f || value > 2.0f) {
			value = MathX.Clamp(value, 0.1f, 2.0f);
			Config.Set(KEY_MENU_SIZE, value);
		}
	}

	// Patch the OnChanges method to replace the hardcoded 0.85f with our configurable value
	[HarmonyPatch(typeof(ContextMenu), "OnChanges")]
	class ContextMenu_OnChanges_Patch {
		static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
			var codes = new List<CodeInstruction>(instructions);

			for (int i = 0; i < codes.Count; i++) {
				// Look for ldc.r4 0.85 (the float constant)
				if (codes[i].opcode == OpCodes.Ldc_R4 && codes[i].operand is float floatValue && floatValue == 0.85f) {
					// Replace with a call to our method that returns the configured value
					codes[i] = new CodeInstruction(OpCodes.Call,
						AccessTools.Method(typeof(ContextMenu_OnChanges_Patch), nameof(GetMenuSize)));
					Msg($"Patched context menu size constant at instruction {i}");
				}
			}

			return codes.AsEnumerable();
		}

		static float GetMenuSize() {
			return Config?.GetValue(KEY_MENU_SIZE) ?? 0.85f;
		}
	}
}
