using Terraria;

namespace Synergia.Content.Buffs;

public class ReincarnationBuff : ModBuff {
    public override bool RightClick(int buffIndex) => false;
    public override void Update(Player player, ref int buffIndex) => player.GetDamage(DamageClass.Generic) += 0.20f;
};
