using Bismuth.Content.Projectiles;
using Synergia.Content.Buffs;
using Synergia.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Synergia.Common.GlobalPlayer;

public class BookPlayer : ModPlayer {
    public int BuffTime { get; private set; } = 0;
    public int SecondToDeath { get; private set; } = 0;

    public bool Active { get; private set; } = false;
    public bool Used = false;
    public bool onlyVisual = false;

    public override void LoadData(TagCompound tag) {
        BuffTime = tag.GetInt("Buff_Time");
        SecondToDeath = tag.GetInt("Second_To_Death");

        Active = tag.GetBool("Active_Aura");
        Used = tag.GetBool("Used_Book");
    }
    public override void SaveData(TagCompound tag) {
        tag["Buff_Time"] = BuffTime;
        tag["Second_To_Death"] = SecondToDeath;

        tag["Active_Aura"] = Active;
        tag["Used_Book"] = Used;
    }
    public override void ResetEffects() => onlyVisual = false;
    public override void PostUpdate() {
        if (Main.time == 1 && Used) {
            Used = false;
            Active = false;
        }
        if (Used && !Active) {
            SecondToDeath = 300;
            Active = true;
        }
        if (SecondToDeath != 0 || onlyVisual) {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<ReincarnationAura>()] <= 0) {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<ReincarnationAura>(), 0, 0, Player.whoAmI);
            }
        }
        if (SecondToDeath > 0) { SecondToDeath--; }
        if (BuffTime != 0) {
            if (!Player.HasBuff(ModContent.BuffType<ReincarnationBuff>())) {
                Player.AddBuff(ModContent.BuffType<ReincarnationBuff>(), 300);
            }
        }
        if (BuffTime > 0) { BuffTime--; }
    }
    public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource) {
        if (SecondToDeath == 0) { return true; }
        else {
            Player.statLife = 1;
            BuffTime = 300;
            SecondToDeath = 0;
            playSound = false;
            genDust = false;
            Projectile.NewProjectile(Player.GetSource_FromThis(), new(Player.Center.X, Player.Center.Y - 60), Vector2.Zero, ProjectileType<RevivingEagleP>(), 0, 0, Player.whoAmI);
            return false;
        }
    }
}