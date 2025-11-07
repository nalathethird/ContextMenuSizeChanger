# ContextMenuCustomizer

A [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) mod for [Resonite](https://resonite.com/) that provides **COMPLETE VISUAL CONTROL** over context menus!

**The most comprehensive context menu customization mod ever created** - Transform your context menus with colors, gradients, custom fonts, animations, and pixel-perfect control over every visual element!

---

## **v1.0.0 - THE ULTIMATE RELEASE!**

### **Brand New Features:**
- **Full Color Control** - Customize EVERYTHING! Text, arcs, outlines, icons!
- **Gradient Effects** - Beautiful color blending across menu items
- **Arc Fill & Outline Colors** - Make menus truly yours
- **Icon Tinting** - Apply color filters to all icons
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

#### Arc Colors  
- **ArcFillColor** - Menu item background (RGBA, alpha > 0 to enable)
- **ArcOutlineColor** - Menu item borders (RGBA, alpha > 0 to enable)

#### Icon Colors
- **IconTintColor** - Color filter for all icons (RGBA)

#### Gradient System
- **EnableGradient** - Toggle gradient effects (true/false)
- **GradientColor** - Secondary gradient color (RGBA)

---

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

## **Preset Gallery**

### **Cyberpunk Neon**
```json
{
  "TextColor": { "r": 0, "g": 1, "b": 1, "a": 1 },
  "TextOutlineColor": { "r": 1, "g": 0, "b": 1, "a": 1 },
  "ArcFillColor": { "r": 0.1, "g": 0, "b": 0.3, "a": 0.8 },
  "ArcOutlineColor": { "r": 0, "g": 1, "b": 1, "a": 1 },
  "EnableGradient": true,
  "GradientColor": { "r": 1, "g": 0, "b": 1, "a": 1 },
  "TextOutlineThickness": 0.4,
  "ArcCornerRadius": 30.0,
  "MenuScale": 1.2
}
```

### **Fire Glow**
```json
{
  "TextColor": { "r": 1, "g": 1, "b": 0, "a": 1 },
  "TextOutlineColor": { "r": 1, "g": 0.3, "b": 0, "a": 1 },
  "ArcFillColor": { "r": 0.3, "g": 0, "b": 0, "a": 0.7 },
  "ArcOutlineColor": { "r": 1, "g": 0.5, "b": 0, "a": 1 },
  "EnableGradient": true,
  "GradientColor": { "r": 1, "g": 0, "b": 0, "a": 1 },
  "ArcOutlineThickness": 5.0,
  "TextOutlineThickness": 0.3
}
```

### **Crystal Clear**
```json
{
  "MenuOpacity": 0.7,
  "ArcFillColor": { "r": 0.8, "g": 0.9, "b": 1, "a": 0.3 },
  "ArcOutlineColor": { "r": 1, "g": 1, "b": 1, "a": 0.6 },
  "TextColor": { "r": 1, "g": 1, "b": 1, "a": 1 },
  "TextOutlineColor": { "r": 0, "g": 0.5, "b": 1, "a": 1 },
  "ArcCornerRadius": 25.0,
  "TextOutlineThickness": 0.25
}
```

### **Forest Theme**
```json
{
  "TextColor": { "r": 0.9, "g": 1, "b": 0.7, "a": 1 },
  "TextOutlineColor": { "r": 0.2, "g": 0.4, "b": 0.1, "a": 1 },
  "ArcFillColor": { "r": 0.1, "g": 0.3, "b": 0.1, "a": 0.8 },
  "ArcOutlineColor": { "r": 0.3, "g": 0.6, "b": 0.2, "a": 1 },
  "IconTintColor": { "r": 0.7, "g": 1, "b": 0.5, "a": 1 }
}
```

### **Ocean Wave**
```json
{
  "EnableGradient": true,
  "ArcFillColor": { "r": 0, "g": 0.3, "b": 0.6, "a": 0.7 },
  "GradientColor": { "r": 0, "g": 0.8, "b": 1, "a": 1 },
  "TextColor": { "r": 0.8, "g": 1, "b": 1, "a": 1 },
  "ArcOutlineColor": { "r": 0, "g": 0.8, "b": 1, "a": 0.8 },
  "ArcCornerRadius": 20.0
}
```

### **Minimalist Ghost**
```json
{
  "MenuOpacity": 0.5,
  "ArcFillColor": { "r": 0, "g": 0, "b": 0, "a": 0.1 },
  "ArcOutlineThickness": 1.0,
  "ArcCornerRadius": 4.0,
  "TextOutlineThickness": 0.1
}
```

### **Golden Luxury**
```json
{
  "TextColor": { "r": 1, "g": 0.95, "b": 0.6, "a": 1 },
  "TextOutlineColor": { "r": 0.6, "g": 0.4, "b": 0, "a": 1 },
  "ArcFillColor": { "r": 0.3, "g": 0.2, "b": 0, "a": 0.9 },
  "ArcOutlineColor": { "r": 1, "g": 0.84, "b": 0, "a": 1 },
  "TextOutlineThickness": 0.3,
  "ArcOutlineThickness": 4.0,
  "ArcCornerRadius": 15.0
}
```

### **Cherry Blossom**
```json
{
  "TextColor": { "r": 1, "g": 0.7, "b": 0.9, "a": 1 },
  "ArcFillColor": { "r": 1, "g": 0.9, "b": 0.95, "a": 0.6 },
  "ArcOutlineColor": { "r": 1, "g": 0.4, "b": 0.7, "a": 0.8 },
  "EnableGradient": true,
  "GradientColor": { "r": 1, "g": 0.6, "b": 0.8, "a": 1 },
  "IconTintColor": { "r": 1, "g": 0.8, "b": 0.9, "a": 1 }
}
```

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

**Touchscreen Only**:
```json
{
  "AcceptPhysicalTouch": true,
  "AcceptExistingTouch": false
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

## **Color Format Guide**

Colors use RGBA (Red, Green, Blue, Alpha):
- **Values**: 0.0 to 1.0 for each channel
- **Alpha**: 1.0 = opaque, 0.0 = invisible

### Quick Reference
```
Pure Red:    { r: 1,   g: 0,   b: 0,   a: 1 }
Pure Green:  { r: 0,   g: 1,   b: 0,   a: 1 }
Pure Blue:   { r: 0,   g: 0,   b: 1,   a: 1 }
Yellow:      { r: 1,   g: 1,   b: 0,   a: 1 }
Cyan:        { r: 0,   g: 1,   b: 1,   a: 1 }
Magenta:     { r: 1,   g: 0,   b: 1,   a: 1 }
Orange:      { r: 1,   g: 0.5, b: 0,   a: 1 }
Purple:      { r: 0.5, g: 0,   b: 1,   a: 1 }
White:       { r: 1,   g: 1,   b: 1,   a: 1 }
Black:       { r: 0,   g: 0,   b: 0,   a: 1 }
Gray:        { r: 0.5, g: 0.5, b: 0.5, a: 1 }
```

---

## **Installation**

1. Install [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader)
2. Download [ContextMenuCustomizer.dll](https://github.com/nalathethird/ContextMenuSizeChanger/releases/latest)
3. Place in `rml_mods` folder (default: `C:\Program Files (x86)\Steam\steamapps\common\Resonate\rml_mods`)
4. Launch Resonite
5. Configure and enjoy! ??

---

## **Technical Details**

### Harmony Patches
1. **OnAttach**: Initial property setup, material customization
2. **OnChanges**: Visual scaling, opacity application
3. **OnCommonUpdate** (Transpiler): Animation speed & interaction distance
4. **AddItem** (Postfix): Per-item colors, gradients, fonts, icons

### Features
- ? Slot-based scaling (uniform, functional)
- ? Material-level color control
- ? Dynamic gradient generation
- ? Real-time opacity blending
- ? Custom font loading with error handling
- ? Icon texture mode fixing
- ? No mod conflicts
- ? Performance optimized with caching

---

## **Changelog**

### v1.0.0 - THE ULTIMATE RELEASE ??
**Complete Visual Control System:**
- Added 7 color customization settings
- Added gradient system with secondary color
- Added menu opacity control
- Added arc fill & outline color override
- Added icon tinting system
- Added custom font URL support
- Added text line height control
- Fixed icon texture wrapping (Clamp mode)
- Added comprehensive preset gallery
- Total: **24 customization settings**

**Core Features:**
- Visual layout control (5 settings)
- Complete color system (7 settings)
- Typography customization (4 settings)
- Icon & sprite handling (1 setting)
- Arc visual control (2 settings)
- Transparency system (1 setting)
- Animation control (2 settings)
- Interaction tweaking (2 settings)

**Core Features:**
- Visual layout control (5 settings)
- Complete color system (7 settings)
- Typography customization (4 settings)
- Icon & sprite handling (1 setting)
- Arc visual control (2 settings)
- Transparency system (1 setting)
- Animation control (2 settings)
- Interaction tweaking (2 settings)
- **Touch input control (2 settings)** ? NEW!

**Touch Input System:**
- Added AcceptPhysicalTouch - Direct touch interaction
- Added AcceptExistingTouch - Existing touch source control
- Perfect for Quest hand tracking and touchscreens
- Full control over touch interaction behavior

**Compatibility:**
- Smart color override detection (respects ContextMenuItemSource)
- IsDriven checks prevent conflicts with user content
- Backwards compatible with existing menus

**Total: 26 Customization Settings!**
