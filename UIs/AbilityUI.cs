// Code by SerNik

using Synergia.Common;
using Synergia.Common.GlobalPlayer;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace Synergia.UIs;

public class AbilityUI(Item item, Player player, int index) : UIState {
    BookAbilitySlot _activeSpell;

    bool[] _hoverSlot = [false, false, false, false];
    bool _close = false;

    public override void OnInitialize() {
        for (int i = 0; i < BookSpellSlotManager.Slots.Count; i++) {
            if (BookSpellSlotManager.Slots[i].CanActive(item.type)) {
                _activeSpell = BookSpellSlotManager.Slots[i];
                break;
            }
        }
        if (_activeSpell == null) {
            player.GetModPlayer<BookAbilityPlayer>().ui = null;
            GetInstance<Synergia>().AbilityUI.SetState(null);
        }
    }
    public override void Update(GameTime gameTime) {
        BookAbilityPlayer modPlayer = player.GetModPlayer<BookAbilityPlayer>();
        if (modPlayer.GetModedAccItemInSlot(player)[index].type != item.type) {
            for (int i = 0; i < 4; i++) { modPlayer.UsesSlot[index] = false; }
            if (modPlayer.alpha == 0) {
                modPlayer.permomentClose = true;
                modPlayer.ui = null;
                GetInstance<Synergia>().AbilityUI.SetState(null);
            }
            modPlayer.alpha = MathHelper.Clamp(modPlayer.alpha - 0.05f, 0f, 1f);
            _close = true;
        }
        if (modPlayer.close) {
            modPlayer.alpha = MathHelper.Clamp(modPlayer.alpha - 0.05f, 0f, 1f);
            if (modPlayer.alpha == 0) {
                modPlayer.ui = null;
                GetInstance<Synergia>().AbilityUI.SetState(null);
            }
            _close = true;
        }
    }
    public override void DrawSelf(SpriteBatch spriteBatch) {
        base.DrawSelf(spriteBatch);
        BookAbilityPlayer modPlayer = player.GetModPlayer<BookAbilityPlayer>();
        if (!_close) { modPlayer.alpha = MathHelper.Clamp(modPlayer.alpha + 0.02f, 0f, 1f); }

        DrawSlot(spriteBatch, new Vector2(-50, -30), 0, true);
        DrawSlot(spriteBatch, new Vector2(50, -30), 1);
        DrawSlot(spriteBatch, new Vector2(-50, 30), 2, right: true);
        DrawSlot(spriteBatch, new Vector2(+50, 30), 3);

        modPlayer.ActiveSpell = _activeSpell;
    }
    void DrawSlot(SpriteBatch sb, Vector2 pos, int index, bool left = false, bool right = false) {
        BookAbilityPlayer modPlayer = player.GetModPlayer<BookAbilityPlayer>();

        Texture2D slot = RTextures.AbilityBookSlots[0].Value;
        Texture2D slotGlow = RTextures.AbilityBookSlots[1].Value;
        Texture2D look = RTextures.AbilityBookSlots[2].Value;
        Texture2D lookGlow = RTextures.AbilityBookSlots[3].Value;

        Vector2 pos2 = new((player.Center.X - Main.screenPosition.X) / Main.UIScale, (player.Center.Y - Main.screenPosition.Y) / Main.UIScale);
        Vector2 velocity = new((float)Math.Cos(Main.GameUpdateCount * 0.08f * (1f + index * 0.1f) + index) * 2f, (float)Math.Sin(Main.GameUpdateCount * 0.08f * (0.8f + index * 0.15f) + index) * 2f);

        pos2 += velocity;

        if (UIUtils.Hover(new Vector2(pos2.X + pos.X, pos2.Y + pos.Y), slot, 1)) {
            if (!_hoverSlot[index]) {
                SoundEngine.PlaySound(SoundID.MenuTick);
                _hoverSlot[index] = true;
            }

            player.mouseInterface = true;

            string text = "";
            if (modPlayer.UsesSlot[index]) { text = "активно"; }
            else { text = "Можно активировать"; }

            if (modPlayer.ActiveSlotCount == 2 && !modPlayer.UsesSlot[index]) {
                text = "нету пустых слотов";
            }

            Main.instance.MouseText(_activeSpell.SlotDescription(index) + "\n" + text);
            if (UIUtils.LeftClick()) {
                if (modPlayer.ActiveSlotCount < 2) {
                    modPlayer.UsesSlot[index] = !modPlayer.UsesSlot[index];
                }
                else {
                    modPlayer.UsesSlot[index] = false;
                }
            }
        }
        else { _hoverSlot[index] = false; };

        sb.Draw(slot, new Vector2(pos2.X + pos.X, pos2.Y + pos.Y), null, Color.White * modPlayer.alpha, 0f, slot.Size() / 2f, 1, SpriteEffects.None, 1);
        if (_hoverSlot[index]) {
            sb.Draw(slotGlow, new Vector2(pos2.X + pos.X, pos2.Y + pos.Y), null, Color.Gold * modPlayer.alpha, 0f, slotGlow.Size() / 2f, 1, SpriteEffects.None, 1);
        }
        if (!modPlayer.UsesSlot[index] && modPlayer.ActiveSlotCount == 2) {
            if (_hoverSlot[index]) {
                sb.Draw(lookGlow, new Vector2(pos2.X + pos.X, (pos2.Y + pos.Y) - 11), null, Color.White * modPlayer.alpha, 0f, lookGlow.Size() / 2f, 1, SpriteEffects.None, 1);
            }
            else { sb.Draw(look, new Vector2(pos2.X + pos.X, (pos2.Y + pos.Y) - 10), null, Color.White * modPlayer.alpha, 0f, look.Size() / 2f, 1, SpriteEffects.None, 1); }
        }
    }
}