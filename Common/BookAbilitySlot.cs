// Code by SerNik

using System.Collections.Generic;
using Terraria;

namespace Synergia.Common;

public abstract class BookAbilitySlot : ModType, ILocalizedModType {
    public string LocalizationCategory => "BookAbilitys";

    public override void Register() { ModTypeLookup<BookAbilitySlot>.Register(this); }

    public override void Load() {
        BookSpellSlotManager.Slots.Add(this);
        _ = this.GetLocalization("0");
        _ = this.GetLocalization("1");
        _ = this.GetLocalization("2");
        _ = this.GetLocalization("3");
    }
    public abstract bool CanActive(int target);
    public abstract void SlotStat(int slotNum, Player player, Item item);
    public virtual string SlotDescription(int slotNum) {
        return slotNum switch {
            0 => this.GetLocalization("0").Value,
            1 => this.GetLocalization("1").Value,
            2 => this.GetLocalization("2").Value,
            3 => this.GetLocalization("3").Value,
            _ => string.Empty,
        };
    }
}
public class BookSpellSlotManager {
    public static List<BookAbilitySlot> Slots { get; private set; } = [];
}