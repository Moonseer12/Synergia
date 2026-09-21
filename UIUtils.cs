using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace Synergia;

public static class UIUtils {
    public static bool LeftClick() => Main.mouseLeft && Main.mouseLeftRelease;
    public static bool RightClick() => Main.mouseRight && Main.mouseRightRelease;
    public static Vector2 GetMousePos() => new(Main.mouseX, Main.mouseY);
    public static bool Hover(Vector2 pos, Texture2D texture, float drawScale = 1f) {
        Vector2 size = texture.Size() * drawScale;
        Rectangle rect = new((int)(pos.X - size.X / 2f), (int)(pos.Y - size.Y / 2f), texture.Width, texture.Height);
        return rect.Contains(Main.mouseX, Main.mouseY);
    }
    public static bool Hover(Vector2 pos, Rectangle texture, float drawScale = 1f) {
        Vector2 size = texture.Size() * drawScale;
        Rectangle rect = new((int)(pos.X - size.X / 2f), (int)(pos.Y - size.Y / 2f), texture.Width, texture.Height);
        return rect.Contains(Main.mouseX, Main.mouseY);
    }
    public static bool HoverText(Vector2 pos, string text, float scale = 0.9f) => new Vector2(Main.mouseX, Main.mouseY).Between(pos, pos + ChatManager.GetStringSize(FontAssets.MouseText.Value, text, new Vector2(scale)) * new Vector2(scale) * new Vector2(1f).X);
    public static void DrawText(SpriteBatch sB, string text, Vector2 pos, DynamicSpriteFont font = null, Color? color = null, Vector2? origin = null, Vector2? scale = null) {
        font ??= FontAssets.MouseText.Value;
        color ??= Color.White;
        origin ??= Vector2.Zero;
        scale ??= Vector2.One;
        ChatManager.DrawColorCodedStringWithShadow(sB, font, text, pos, (Color)color, 0f, (Vector2)origin, (Vector2)scale);
    }
};