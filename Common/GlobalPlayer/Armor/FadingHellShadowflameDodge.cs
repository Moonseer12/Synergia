using Avalon.Particles;
using Synergia.Common.ModSystems.Netcode;
using Synergia.Common.ModSystems.Netcode.Packets;
using Synergia.Helpers;
using Terraria;
using Terraria.Graphics.Renderers;
using Terraria.ID;

namespace Synergia.Common.GlobalPlayer.Armor
{
    public class FadingHellShadowflameDodge : ModPlayer
    {
        public bool IsActive = false;
        public int PartsCount = 0;
        internal int PrevPartsCount = 0;
        public override void Initialize()
        {
            IsActive = false;
            PartsCount = 0;
            PrevPartsCount = 0;
        }
        public override void ResetEffects()
        {
            IsActive = false;
        }
        public override void UpdateDead()
        {
            PartsCount = 0;
            PrevPartsCount = 0;
        }
        public override void PostUpdateEquips()
        {
            if (!IsActive) return;

            if (PrevPartsCount == PartsCount) return;
            PrevPartsCount = PartsCount;
            if (PartsCount != 3) return;

            PartsCount = 0;
            PrevPartsCount = 0;
            Vector2 velocity;
            for(int i = 0; i < 30; i++)
            {
                velocity = Vector2.UnitX.RotatedBy(MathHelper.ToRadians(i * 12)) * 8f;
                Main.ParticleSystem_World_OverPlayers.Add(
                    new ShadowflameParticle(velocity, Player.Center, 1f));
            }
        }
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (!IsActive) return false;
            if (!Main.rand.NextBool(3)) return false;

            DodgeEffect();
            Vector2 velocity;
            for(int i = 0; i < 3; i++)
            {
                velocity.Y = (float)Main.rand.Next(-40, -10) * 0.01f;
                velocity.X = (float)Main.rand.Next(-20, 21) * 0.01f + (0.2f * info.HitDirection);
                Projectile.NewProjectile(
                    Player.GetSource_FromAI(),
                    Player.Center,
                    velocity,
                    ModContent.ProjectileType<ShadowPlayerGore>(),
                    0,
                    0,
                    Main.myPlayer,
                    i
                );
            }
            return true;
        }
        internal void DodgeEffect()
        {
            Player.SetImmuneTimeForAllTypes(60);

            if (Main.myPlayer == Player.whoAmI && Main.netMode != NetmodeID.SinglePlayer)
                MultiplayerSystem.SendPacket(new ShadowflameDodgePacket(Player), ignoreClient: Main.myPlayer);
        }
    }
    public class ShadowPlayerGore : ModProjectile
    {
        internal const int ExtraUpdates = 10;
        internal readonly float[] DustOffsetY = { -12f, 0f, 12f };
        public ref float ArmorType => ref Projectile.ai[0];
        public ref float X => ref Projectile.ai[1];
        public ref float Y => ref Projectile.ai[2];
        public override string Texture => "Synergia/Assets/Textures/Blank";
        public override void SetDefaults()
        {
            Projectile.height = 8;
            Projectile.width = 8;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = ExtraUpdates;
            Projectile.timeLeft = 95 * ExtraUpdates;
            Projectile.alpha = 0;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player == null || player.dead)
                Projectile.Kill();
            Projectile.rotation += Projectile.velocity.X * 0.01f;
            if (Projectile.timeLeft > 35 * ExtraUpdates)
            {
                Projectile.velocity.X *= 0.9995f;
                Projectile.velocity.Y += 0.001f;
                return;
            }
            else if(Projectile.timeLeft > 20 * ExtraUpdates)
            {
                Projectile.velocity *= 0.95f;
                Projectile.velocity *= 0.95f;
                Projectile.Opacity = (Projectile.timeLeft / ExtraUpdates - 20) / 15f;
                X = Projectile.Center.X;
                Y = Projectile.Center.Y;
                return;
            }

            if(Projectile.timeLeft % 5 == 0)
                Main.ParticleSystem_World_OverPlayers.Add(
                    new ShadowflameParticle(Main.rand.NextVector2Unit() * 2f,
                    Projectile.Center + new Vector2(0f, DustOffsetY[(int)ArmorType]).RotatedBy(Projectile.rotation),
                    Main.rand.NextFloat(0.8f, 1.2f))
                );
            float progress = 1f - Projectile.timeLeft / 20f / ExtraUpdates;
            Projectile.Center = Vector2.Lerp(new Vector2(X, Y), player.Center, EaseFunctions.EaseInCubic(progress));
        }
        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.GetModPlayer<FadingHellShadowflameDodge>().PartsCount++;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ArmorType switch
            {
                1 => ModContent.Request<Texture2D>("Synergia/Content/Items/Armor/Magic/FadingHell/FadingHellChestplate_Body2").Value,
                2 => ModContent.Request<Texture2D>("Synergia/Content/Items/Armor/Magic/FadingHell/FadingHellPants_LegsCursed").Value,
                _ => ModContent.Request<Texture2D>("Synergia/Content/Items/Armor/Magic/FadingHell/FadingHellHat_HeadCursed").Value,
            };
            Vector2 position = Projectile.Center - Main.screenPosition;
            int frameHeight = texture.Height / 20;
            if (ArmorType == 1)
                frameHeight = texture.Height;
            Rectangle frame = new(0, 0, texture.Width, frameHeight);
            Vector2 origin = frame.Size() / 2f;
            Color color = Color.Lerp(lightColor, Color.Black, 1f - Projectile.Opacity);
            Main.EntitySpriteDraw(
                texture,
                position,
                frame,
                color,
                Projectile.rotation,
                origin,
                1f,
                SpriteEffects.None,
                0
            );
            return false;
        }
    }
    public class ShadowflameParticle : BaseParticle
    {
        internal const int MaxLifetime = 20;
        internal const float Size = 0.1f;
		float ai1;
		Vector2 Velocity;

		public ShadowflameParticle(Vector2 velocity, Vector2 position, float ai1)
		{
			Velocity = velocity;
			Position = position;
			this.ai1 = ai1;
		}

        public override void Update(ref ParticleRendererSettings settings)
        {
			base.Update(ref settings);
            if (TimeInWorld > MaxLifetime)
                Active = false;

            Position += Velocity;
            Velocity *= 0.99f;
        }
        public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spriteBatch)
        {
            Texture2D texture2D = (Texture2D)ModContent.Request<Texture2D>("Synergia/Assets/Textures/Glow");
            Rectangle rectangle = texture2D.Frame();
            Vector2 origin = rectangle.Size() / 2f;
            Vector2 position = Position - Main.screenPosition;
            spriteBatch.Draw(
                texture2D,
                position,
                rectangle,
                Color.Black,
                0f,
                origin,
                (1f - EaseFunctions.EaseOutCubic((float)TimeInWorld / MaxLifetime)) * Size * ai1,
                SpriteEffects.None,
                0f);
        }
    }
}
