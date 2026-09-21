using System.Collections.Generic;
using Terraria;

namespace Synergia.Common;

public class Quest : SlotCounter {
    public override string TexturePatch => Patch;
    public override bool ReplaceFavoriteTexture => false;
    public override bool ReplaceItemSlotTexture => false;
    public override bool Active(Item item) => item.questItem;
    public override void Draw(Item drawItem, int context, bool hover, bool favorite, SpriteBatch sb, Vector2 pos, float scale, List<Texture2D> textures) => base.Draw(drawItem, context, hover, favorite, sb, pos, scale, textures);
}
