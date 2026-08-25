using Avalon.Items.Tomes.PreHardmode;
using Synergia.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Synergia.Content.BookAbilitys {
    internal class Test : BookAbilitySlot {
        public override bool CanActive(int target) => target == ModContent.ItemType<AFlowerlessPlant>();
        public override void SlotStat(int slotNum, Player player, Item item) {
            switch (slotNum) {
                case 0: {
                    player.AddBuff(1, 1);
                    player.GetDamage(DamageClass.Generic) += 0.4f;
                    break;
                }
                case 1: {
                    player.AddBuff(2, 1);
                    break;
                }
                case 2: {
                    player.AddBuff(3, 1);
                    break;
                }
                case 3: {
                    player.AddBuff(4, 1);
                    break;
                }
            }
        }
    }
}
