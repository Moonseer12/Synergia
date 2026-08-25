// Code by SerNik

using Avalon.Common;
using Synergia.UIs;
using System.Reflection;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.IO;

namespace Synergia.Common.GlobalPlayer;

public class BookAbilityPlayer : ModPlayer {
    readonly FieldInfo _getModedAccSlot = typeof(ModAccessorySlotPlayer).GetField("exAccessorySlot", BindingFlags.NonPublic | BindingFlags.Instance);

    internal AbilityUI ui = null;

    public BookAbilitySlot ActiveSpell;

    public bool[] UsesSlot { get; private set; } = [false, false, false, false];
    public bool permomentClose = false;
    public bool close = false;
    bool _looadData = false;

    public int ActiveSlotCount {
        get {
            int count = 0;
            for (int i = 0; i < 4; i++) {
                if (UsesSlot[i] == true) { count++; }
            }
            return count;
        }
    }
    int _index = 0;

    public float alpha = 0f;

    string _dataName = "";

    public Item[] GetModedAccItemInSlot(Player player) => (Item[])_getModedAccSlot.GetValue(player.GetModPlayer<ModAccessorySlotPlayer>());

    public override void Initialize() => GetInstance<Synergia>().AbilityUI.SetState(null);
    public override void LoadData(TagCompound tag) {
        _index = tag.GetInt("Index_Of_Acc_Slot");
        UsesSlot = tag.Get<bool[]>("Active Slot");
        _dataName = tag.GetString("Name");
    }
    public override void SaveData(TagCompound tag) {
        tag["Active Slot"] = UsesSlot;
        tag["Index_Of_Acc_Slot"] = _index;
        tag["Name"] = _dataName;
    }
    public override void ProcessTriggers(TriggersSet triggersSet) {
        if (Synergia.SpellUIKey.JustReleased) {
            if (ui != null) { close = true; }
            for (int i = 0; i < Player.GetModPlayer<ModAccessorySlotPlayer>().SlotCount; i++) {
                if (GetModedAccItemInSlot(Player)[i] != null && GetModedAccItemInSlot(Player)[i].type != 0) {
                    if (GetModedAccItemInSlot(Player)[i].GetGlobalItem<AvalonGlobalItemInstance>().Tome) {
                        if (ui == null) {
                            _index = i;
                            ui = new AbilityUI(GetModedAccItemInSlot(Player)[i], Player, _index);
                            close = false;
                            GetInstance<Synergia>().AbilityUI.SetState(ui);
                        }
                        break;
                    }
                }
            }
        }
    }
    public override void PostUpdate() {
        if (_looadData == false) {
            if (_dataName != "") {
                foreach (BookAbilitySlot spellSlot in BookSpellSlotManager.Slots) {
                    if (_dataName == spellSlot.Name) {
                        ActiveSpell = spellSlot;
                        _looadData = true;
                        break;
                    }
                }
            }
        }
        if (!ActiveSpell.CanActive(GetModedAccItemInSlot(Player)[_index].type)) {
            for (int i = 0; i < 4; i++) { UsesSlot[i] = false; }
        }
        for (int i = 0; i < UsesSlot.Length; i++) {
            if (UsesSlot[i] == true) {
                if (ActiveSlotCount <= 2) {
                    ActiveSpell.SlotStat(i, Player, GetModedAccItemInSlot(Player)[_index]);
                }
            }
        }
        if (ActiveSpell != null) { _dataName = ActiveSpell.Name; }
    }
}