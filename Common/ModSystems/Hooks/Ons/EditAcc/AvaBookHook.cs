using Avalon.Items.Tomes.PreHardmode;
using Avalon.Items.Tomes.Superhardmode;
using MonoMod.RuntimeDetour;
using System.Reflection;
using Terraria;

namespace Synergia.Common.ModSystems.Hooks.Ons.EditAcc {
    public class AvaBookHook : ModSystem {
        static Hook origAccBonus;

        delegate void Orig_UpdateAccessory(ModItem item, Player player, bool hideVisual);
        delegate void UpdateAccessoryDetour(Orig_UpdateAccessory orig, ModItem item, Player player, bool hideVisual);

        public override void Load() {
            int[] it = [ItemType<AFlowerlessPlant>(), ItemType<Dominance>()];

            for (int i = 0; i < it.Length; i++) {
                MethodInfo info = GetModItem(it[i]).GetType().GetMethod("UpdateAccessory", BindingFlags.Public | BindingFlags.Instance);
                origAccBonus = new(info, (UpdateAccessoryDetour)HookEditAcc);
            }
        }
        void HookEditAcc(Orig_UpdateAccessory orig, ModItem item, Player player, bool hideVisual) {
            //Main.NewText("A");
        }
        public override void Unload() {
            origAccBonus?.Dispose();
            origAccBonus = null;
        }
    }
}
