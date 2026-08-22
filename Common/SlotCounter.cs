using System.Collections.Generic;
using Terraria;

namespace Synergia.Common;

public abstract class SlotCounter : ILoadable {
    internal string Patch => "Synergia/Assets/UIs/" + Name;

    public virtual string TexturePatch => (GetType().Namespace + "." + Name).Replace('.', '/');

    public virtual string Name => GetType().Name;
    public virtual string Hower => "_Hower";
    public virtual string Favorite => "_Favorite";

    public virtual bool ReplaceFavoriteTexture => false;
    public virtual bool ReplaceItemSlotTexture => false;

    public virtual bool Active(Item item) => false;
    public virtual void Draw(Item drawItem, int context, bool hover, bool favorite, SpriteBatch sb, Vector2 pos, float scale, List<Texture2D> textures) {
        if (!favorite) {
            sb.Draw(textures[0], pos, null, Color.White, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f);
            if (drawItem.type == Main.HoverItem.type && textures[1] != null) { sb.Draw(textures[1], pos, null, Color.White, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f); }
        }
        if (!ReplaceFavoriteTexture && drawItem.favorited && textures[2] != null) {
            Texture2D hoverTexture;
            if (textures[3] != null && drawItem.type == Main.HoverItem.type) { hoverTexture = textures[3]; }
            else { hoverTexture = textures[2]; }
            sb.Draw(hoverTexture, pos, null, Color.White, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f);
        }
    } 

    public void Load(Mod mod) => Manager.Slots.Add(this);
    public void Unload() { }
}
public class Manager {
    public static List<SlotCounter> Slots = [];
}