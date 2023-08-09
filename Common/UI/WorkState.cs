using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.Audio;
using Terraria.ID;
using Proletaria.Content.Items;
using Terraria.GameContent;
using System;

namespace Proletaria.Common.UI;

public class WorkState : UIState
{
    private UIText? titleText;
    private UIText? contentText;
    private UIElement? contentView;

    private UIScrollbar? scrollbar;

    public override void OnInitialize()
    {
        DraggablePanel panel = new();

        panel.SetPadding(0);
        panel.BackgroundColor = new Color(73, 94, 171, 220);
        SetRectangle(panel, width: Main.screenHeight * 0.5f, height: Main.screenHeight * 0.7f, left: Main.screenWidth * 0.6f, top: Main.screenHeight * 0.2f);
        
        var closeButton = new UIImageButton(ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete"));
        SetRectangle(closeButton, left: panel.Width.Pixels - 32f, top: 10f, width: 22f, height: 22f);
        closeButton.OnLeftClick += new MouseEvent(OnClose);
        panel.Append(closeButton);

        titleText = new(LocalizedText.Empty) { HAlign = 0f };
        SetRectangle(titleText, left: 16f, top: 16f, width: 0f, 0f);
        panel.Append(titleText);

        contentView = new() { OverflowHidden = true };
        SetRectangle(contentView, left: 16f, top: 44f, width: panel.Width.Pixels - 32f, height: panel.Height.Pixels - 60f);

        contentText = new(LocalizedText.Empty) { OverflowHidden = false, Width = contentView.Width };
        contentView.Append(contentText);

        panel.Append(contentView);
        Append(panel);
    }

    public void UseWork(BaseWork work)
    {
        var title = Language.GetTextValue($"Mods.Proletaria.Items.{work.Name}.DisplayName");
        var content = Language.GetTextValue($"Mods.Proletaria.Works.{work.Name}");

        if (contentView is not null && contentText is not null && titleText is not null)
        {
            titleText.SetText(title);
            contentText.SetText(FontAssets.MouseText.Value.CreateWrappedText(content, contentText.Width.Pixels));

            Console.WriteLine($"{contentText.Height.Pixels}, {contentView.Height.Pixels}");

            if (5000f > contentView.Height.Pixels)
            {
                scrollbar = new();
                scrollbar.SetView(contentView.Height.Pixels, 5000f);
            }
        }
    }

    private void OnClose(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(SoundID.MenuClose);
        ModContent.GetInstance<WorkSystem>().Hide();
    }

    private static void SetRectangle(UIElement element, float left, float top, float width, float height)
    {
        element.Left.Set(left, 0f);
        element.Top.Set(top, 0f);
        element.Width.Set(width, 0f);
        element.Height.Set(height, 0f);
    }

    public override void ScrollWheel(UIScrollWheelEvent evt)
    {
        base.ScrollWheel(evt);
        if (scrollbar is not null)
        {
            scrollbar.ViewPosition -= evt.ScrollWheelValue;
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (scrollbar is not null && contentText is not null)
        {
            contentText.Top.Set(0f - scrollbar.GetValue(), 0f);
        }

        Recalculate();
    }
}
