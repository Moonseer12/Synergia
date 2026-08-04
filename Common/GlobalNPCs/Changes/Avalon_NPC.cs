using Avalon.NPCs.Bosses.Hardmode.Phantasm;
using Avalon.NPCs.Bosses.PreHardmode.BacteriumPrime;
using Avalon.NPCs.Bosses.PreHardmode.DesertBeak;
using Avalon.NPCs.Hell;
using Synergia.Common.GlobalNPCs.Changes;
using Terraria;

namespace Synergia.Common.ModSystems.RecipeSystem.ChangesRecipe.AvalonsChanges {
    public partial class Avalons {
        public class Avalon_NPC : BaseNPC {
            public override void EditNPC(NPC npc) {
                EditNPC(npc, NPCType<BacteriumPrime>(), 2, 750);
                EditNPC(npc, NPCType<Blaze>(), 5, 8050);
                EditNPC(npc, NPCType<DesertBeak>(), 5, 950);
                EditNPC(npc, NPCType<Phantasm>(), 18, 180000);
            }
        }
    }
}