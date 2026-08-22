// Code by SerNik
using Synergia.Common.GlobalPlayer;
using System;
using Terraria;
using Terraria.ID;

namespace Synergia.Content.Projectiles;

public class ReincarnationAura : ModProjectile {
    public override void SetDefaults() {
        Projectile.width = 170;
        Projectile.height = 170;
        Projectile.alpha = 150;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 2;
        Projectile.tileCollide = false;
    }
    public override void AI() {
        if (Main.player[Projectile.owner].GetModPlayer<BookPlayer>().SecondToDeath != 0 || Main.player[Projectile.owner].GetModPlayer<BookPlayer>().onlyVisual) {
            Projectile.Center = Main.player[Projectile.owner].Center;
            Projectile.timeLeft = 2;
            for (int i = 0; i < 30; i++) {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new((float)Math.Sin(angle) * 85, (float)Math.Cos(angle) * 85);
                Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset - Vector2.One * 4, 0, 0, DustID.PortalBoltTrail, 0, 0, 100)];
                dust.noGravity = true;
            }
        }
        else { Projectile.Kill(); }
    }
}