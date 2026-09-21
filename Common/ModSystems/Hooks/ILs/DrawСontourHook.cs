using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace Synergia.Common.ModSystems.Hooks.ILs {
    public class DrawСontourHook : ILoadable {
        public void Load(Mod mod) {
            IL_ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += DrawСontour;
        }

        static void DrawСontour(ILContext il) {
            ILCursor c = new(il);

            c.GotoNext(MoveType.After, i => i.MatchLdcI4(28));
            for (int i = 0; i < 21; i++) { c.Index++; }
            c.RemoveRange(15);

            c.Emit(OpCodes.Ldloc, 7); // Texture2D
            c.Emit(OpCodes.Ldloc, 8); // Color
            c.Emit(OpCodes.Ldarg, 0); // SpriteBatch
            c.Emit(OpCodes.Ldarg, 1); // Item[]
            c.Emit(OpCodes.Ldarg, 2); // int
            c.Emit(OpCodes.Ldarg, 3); // int
            c.Emit(OpCodes.Ldarg, 4); // Vector;

            c.EmitDelegate<Action<Texture2D, Color, SpriteBatch, Item[], int, int, Vector2>>((texture, color, sb, inv, context, slot, pos) => {
                bool drawOrigSlot = true;
                if (context == 6 || context == 8 || context == 9 || context == 10 || context == 11 || context == 13 || context == 21) {
                    sb.Draw(texture, pos, null, color, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f);
                    return;
                }
                for (int i = 0; i < Manager.Slots.Count; i++) {
                    if (Manager.Slots[i].Active(inv[slot])) {
                        if (Manager.Slots[i].ReplaceFavoriteTexture) {
                            RTextures.SlotImgName.TryGetValue(Manager.Slots[i].Name, out (Texture2D, Texture2D, Texture2D, Texture2D) value);
                            if (inv[slot].favorited) {
                                if (value.Item3 != null) { texture = value.Item3; }
                                if (value.Item4 != null && inv[slot].type == Main.HoverItem.type) { texture = value.Item4; }
                            }
                        }
                        if (Manager.Slots[i].ReplaceItemSlotTexture) { drawOrigSlot = false; }
                    }
                }
                if (drawOrigSlot) { sb.Draw(texture, pos, null, color, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f); }
                for (int i = 0; i < Manager.Slots.Count; i++) {
                    if (Manager.Slots[i].Active(inv[slot])) {
                        RTextures.SlotImgName.TryGetValue(Manager.Slots[i].Name, out (Texture2D, Texture2D, Texture2D, Texture2D) value);
                        if (inv[slot].type != ItemID.None) { Manager.Slots[i].Draw(inv[slot], context, inv[slot].type == Main.HoverItem.type, inv[slot].favorited, sb, pos, Main.inventoryScale, [value.Item1, value.Item2, value.Item3, value.Item4]); }
                    }
                }
            });
        }

        public void Unload() {
            IL_ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color -= DrawСontour;
        }
    }
}
