using Bismuth.Content.Items.Other;
using MonoMod.RuntimeDetour;
using Synergia.Common.GlobalPlayer;
using System.Reflection;
using Terraria;

namespace Synergia.Common.ModSystems.Hooks.Ons;

public class FaithTreatiseHook : ModSystem {
    delegate bool orig_CanUseItem(FaithTreatise item, Player player);
    delegate bool hook_CanUseItem(orig_CanUseItem orig, FaithTreatise item, Player player);
    delegate bool? orig_UseItem(FaithTreatise item, Player player);
    delegate bool? hook_UseItem(orig_UseItem orig, FaithTreatise item, Player player);

    Hook value1 = null;
    Hook value2 = null;

    public override void Load() {
        MethodInfo info = typeof(FaithTreatise).GetMethod(nameof(FaithTreatise.CanUseItem), BindingFlags.Public | BindingFlags.Instance);
        value1 = new(info, (hook_CanUseItem)NewCanUseItem);
        info = typeof(FaithTreatise).GetMethod(nameof(FaithTreatise.UseItem), BindingFlags.Public | BindingFlags.Instance);
        value2 = new(info, (hook_UseItem)NewUseItem);
    }

    bool NewCanUseItem(orig_CanUseItem orig, FaithTreatise item, Player player) {
        if (!player.GetModPlayer<BookPlayer>().Used) { return true; }
        else { return false; }
    }
    bool? NewUseItem(orig_UseItem orig, FaithTreatise item, Player player) {
        player.GetModPlayer<BookPlayer>().Used = true;
        return true;
    }

    public override void Unload() {
        value1?.Undo();
        value1 = null;
        value2?.Undo();
        value2 = null;
    }

}
