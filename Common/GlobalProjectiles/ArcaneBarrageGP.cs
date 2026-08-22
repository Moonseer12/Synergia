using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Synergia.Common.GlobalProjectiles
{
    public class ArcaneBarrageGP : GlobalProjectile
    {
        private float timer;
        private Vector2[] trailPositions = new Vector2[12];
        private float[] trailRotations = new float[12];
        private int trailIndex = 0;

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Projectile projectile, bool lateInstatiation) => projectile.ModProjectile != null && projectile.ModProjectile.Mod.Name == "ValhallaMod" && projectile.ModProjectile.Name == "ConiferousCan";

        public override void PostAI(Projectile projectile)
        {
            timer += 0.05f;

            trailPositions[trailIndex] = projectile.Center;
            trailRotations[trailIndex] = projectile.rotation;
            trailIndex = (trailIndex + 1) % trailPositions.Length;
        }

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            SpriteBatch sb = Main.spriteBatch;
            Texture2D tex = ModContent.Request<Texture2D>(projectile.ModProjectile.Texture).Value;
            Vector2 drawPos = projectile.Center - Main.screenPosition;
            float rot = projectile.rotation;
            float scale = projectile.scale;
            Vector2 origin = tex.Size() / 2f;

            float pulse = (float)Math.Sin(timer * 2f) * 0.3f + 0.7f;

            Color outlineColor = new Color(180, 80, 255);
            Color glowColor = new Color(220, 150, 255); 
            Color trailColor = new Color(200, 100, 255);

            sb.End();

            sb.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            for (int i = 0; i < trailPositions.Length; i++)
            {
                if (trailPositions[i] == Vector2.Zero) continue;

                float trailProgress = (float)i / trailPositions.Length;
                float trailAlpha = 0.15f * (1f - trailProgress) * pulse;
                float trailScale = scale * (0.3f + 0.7f * (1f - trailProgress));

                Vector2 trailPos = trailPositions[i] - Main.screenPosition;
                sb.Draw(tex, trailPos, null, trailColor * trailAlpha,
                    trailRotations[i], origin, trailScale, SpriteEffects.None, 0f);
            }

            // === OUTLINE DRAWING (фиолетовый аутлайн) ===
            for (int i = 0; i < 12; i++) // Увеличил количество слоев для красоты
            {
                float offset = 2f + i * 0.6f + pulse * 1.2f;
                float alpha = (0.8f - i * 0.065f) * pulse;
                float rotationOffset = i * 0.25f + timer * 0.4f;

                for (int k = 0; k < 8; k++)
                {
                    Vector2 offsetVec = new Vector2(offset, 0).RotatedBy(k * (MathHelper.Pi / 4f) + rotationOffset);

                    // Внешний слой - яркий фиолетовый
                    sb.Draw(tex, drawPos + offsetVec, null, outlineColor * alpha,
                        rot, origin, scale * 1.15f, SpriteEffects.None, 0f);
                }
            }

            // Внутреннее свечение (фиолетовое)
            for (int i = 0; i < 8; i++)
            {
                float offset = 4f + i * 0.8f + pulse * 1.8f;
                float alpha = (0.5f - i * 0.055f) * pulse * 0.6f;
                float rotationOffset = i * 0.3f + timer * 0.35f;

                for (int k = 0; k < 6; k++)
                {
                    Vector2 offsetVec = new Vector2(offset, 0).RotatedBy(k * (MathHelper.Pi / 3f) + rotationOffset);
                    sb.Draw(tex, drawPos + offsetVec, null, glowColor * alpha,
                        rot, origin, scale * 1.2f, SpriteEffects.None, 0f);
                }
            }

            sb.End();

            // === MAIN DRAWING ===
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            // Основной спрайт
            sb.Draw(tex, drawPos, null, lightColor, rot, origin, scale, SpriteEffects.None, 0f);

            // Ядро с фиолетовым оттенком
            Color coreGlow = new Color(220, 150, 255) * (0.5f + pulse * 0.3f);
            sb.Draw(tex, drawPos, null, coreGlow, rot, origin, scale * 0.8f, SpriteEffects.None, 0f);

            sb.End();

            // Перезапускаем стандартный рендер (мы его отключаем return false)
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            return false;
        }
    }
}