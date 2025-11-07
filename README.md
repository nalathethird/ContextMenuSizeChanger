# ContextMenuCustomizer

A [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) mod for [Resonite](https://resonite.com/) that provides **COMPLETE VISUAL CONTROL** over context menus!

**The most comprehensive context menu customization mod ever created** - Transform your context menus with colors, gradients, custom fonts, animations, and pixel-perfect control over every visual element!

---

## **v1.0.1 - Better Context Menu Fixes Update**

### **Brand New Features:**
- **Full Color Control** - Customize EVERYTHING! Text, arcs, outlines, icons!
- **Arc Fill & Outline Colors** - Make menus truly yours
- **Menu Opacity** - Semi-transparent menus for that sleek look
- **Custom Fonts** - Use ANY font via resdb:// URLs
- **Perfect Typography** - Line height, outline, and sizing control

---

## **Complete Feature List: 26 Settings!**

### **Visual Layout** (5 settings)
- **MenuScale** - Overall menu size (0.1 to 3.0)
- **ItemSeparation** - Item spacing in degrees (1.0 to 20.0)
- **LabelWidth** - Label width (100 to 800)
- **LabelHeight** - Label height (50 to 300)
- **InnerRadiusRatio** - Center circle size (0.1 to 0.9)

---

### **Color Customization** ? (7 settings!)

#### Text Colors
- **TextColor** - Main text color (RGBA)
- **TextOutlineColor** - Text border color (RGBA)

### **Typography** (4 settings)
- **TextSize** - Font size (20 to 100)
- **TextOutlineThickness** - Border thickness (0.0 to 1.0)
- **TextLineHeight** - Multi-line spacing (0.5 to 2.0)
- **CustomFontURL** - resdb:// font URL (empty for default)

---

### **Icons & Sprites** (1 setting)
- **ClampIcons** - Fix texture tiling (true/false)

---

### **Arc Visuals** (2 settings)
- **ArcOutlineThickness** - Border thickness (0.0 to 10.0)
- **ArcCornerRadius** - Corner roundness (0.0 to 50.0)

---

### **Transparency** (1 setting)
- **MenuOpacity** - Overall menu transparency (0.1 to 1.0)

---

### **Animations** (2 settings)
- **OpenSpeed** - Opening animation speed (0.1 to 5.0)
- **CloseSpeed** - Closing animation speed (0.1 to 5.0)

---

### **Interactions** (2 settings)
- **ExitStartDistance** - When closing starts (0.5 to 2.0)
- **ExitEndDistance** - When fully closed (0.5 to 2.0)

---

### **Touch Input** ? NEW! (2 settings)
- **AcceptPhysicalTouch** - Enable direct touch interaction (true/false)
- **AcceptExistingTouch** - Enable existing touch sources (true/false)

---


---

## **Pro Tips**

### Custom Fonts
1. Save a StaticFont in Resonite
2. Copy its resdb:// URL
3. Paste into `CustomFontURL`
4. Reopen menu to see changes

### Color Gradients
- Enable `EnableGradient`
- Set `GradientColor` to desired secondary color
- Automatically blends with base arc colors!

### Transparency Effects
- `MenuOpacity` affects entire menu
- `ArcFillColor` alpha controls individual item transparency
- Combine for stunning glass effects!

### Arc Colors
- Set alpha > 0 to override item colors
- Leave alpha = 0 to use default game colors
- Mix with gradients for dynamic looks!

### **ContextMenuItemSource Compatibility**
**Important**: If you're using `ContextMenuItemSource` components (for programmatic menu items), their color settings will **take priority** over mod settings!

- Mod colors are only applied if the field is **not driven**
- This means:
  - Normal context menus = Full color control
  - Code-generated menus = Mod colors apply
  - ContextMenuItemSource items = Their colors win
  
This ensures the mod doesn't break existing content that relies on `ContextMenuItemSource.Color` drives!

---

### **Touch Input Controls**
Perfect for **Quest users** and **touchscreen setups**!

#### AcceptPhysicalTouch
- **Default**: false (disabled)
- **When enabled**: Allows direct finger/hand touch on menus
- **Use case**: Quest hand tracking, touchscreens, physical interactions
- May cause accidental activations in VR if not careful!

#### AcceptExistingTouch
- **Default**: true (enabled)
- **What it does**: Accepts touch from existing touch systems
- **Use case**: Laser pointers, UI interaction systems
- Keep this enabled for normal menu interaction!

#### Recommended Touch Presets

**Quest Hand Tracking**:
```json
{
  "AcceptPhysicalTouch": true,
  "AcceptExistingTouch": true
}
```

**VR Laser Only** (default):
```json
{
  "AcceptPhysicalTouch": false,
  "AcceptExistingTouch": true
}
```
---

---

## **Installation**

1. Install [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader)
2. Download [ContextMenuCustomizer.dll](https://github.com/nalathethird/ContextMenuSizeChanger/releases/latest)
3. Place in `rml_mods` folder (default: `C:\Program Files (x86)\Steam\steamapps\common\Resonate\rml_mods`)
4. Launch Resonite
5. Configure and enjoy! ??

---
