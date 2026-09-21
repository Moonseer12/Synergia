using Synergia.Common.GlobalPlayer;
using System;
using Terraria;
using Terraria.DataStructures;

namespace Synergia.Common.PlayerLayers {
    public class SpellChainLayer : PlayerDrawLayer {
        public override Position GetDefaultPosition() => PlayerDrawLayers.BeforeFirstVanillaLayer;
        public override void Draw(ref PlayerDrawSet drawInfo) {
            Player player = drawInfo.drawPlayer;
            BookAbilityPlayer modPlayer = player.GetModPlayer<BookAbilityPlayer>();

            if (drawInfo.shadow != 0f || player.dead || player.whoAmI != Main.myPlayer) { return; }
            if (modPlayer.ui == null && modPlayer.alpha <= 0f) { return; }

            Texture2D chain = RTextures.AbilityBookSlots[4].Value;

            Vector2[] offsets = [new(-50, -30), new(50, -30), new(-50, 30), new(50, 30)];

            for (int i = 0; i < offsets.Length; i++) { DrawChain(ref drawInfo, chain, player.Center - Main.screenPosition, GetSlotPosition(i, offsets[i]) * Main.UIScale, Color.White * modPlayer.alpha); }

            Vector2 GetSlotPosition(int index, Vector2 offset) {
                Vector2 pos = new((player.Center.X - Main.screenPosition.X) / Main.UIScale, (player.Center.Y - Main.screenPosition.Y) / Main.UIScale);
                Vector2 velocity = new((float)Math.Cos(Main.GameUpdateCount * 0.08f * (1f + index * 0.1f) + index) * 2f, (float)Math.Sin(Main.GameUpdateCount * 0.08f * (0.8f + index * 0.15f) + index) * 2f);
                return pos + velocity + offset;
            }

            void DrawChain(ref PlayerDrawSet drawInfo, Texture2D texture, Vector2 start, Vector2 end, Color color) {
                Vector2 direction = end - start;
                float distance = direction.Length();

                if (distance <= 0f) { return; }

                direction /= distance;

                float rotation = direction.ToRotation();
                float segmentLength = texture.Width;
                int count = (int)(distance / segmentLength);

                for (int i = 0; i < count; i++) {
                    Vector2 position = start + direction * (i * segmentLength);
                    drawInfo.DrawDataCache.Add(new DrawData(texture, position, null, color, rotation, texture.Size() / 2f, 1f, SpriteEffects.None, 1));
                }
            }
        }
    }
}
