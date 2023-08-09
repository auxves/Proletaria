using Microsoft.Xna.Framework;
using Proletaria.Content.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Proletaria.Common.UI;

[Autoload(Side = ModSide.Client)]
public class WorkSystem : ModSystem
{
    private UserInterface UserInterface { get; } = new();
    internal WorkState State { get;  } = new();

    public override void Load()
    {
        State.Activate();
    }

    public void Show(BaseWork work)
    {
        State.UseWork(work);
        UserInterface.SetState(State);
    }

    public void Hide()
    {
        UserInterface.SetState(null);
    }

    public override void UpdateUI(GameTime gameTime)
    {
        if (UserInterface.CurrentState is not null)
        {
            UserInterface.Update(gameTime);
        }
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        if (mouseTextIndex != -1)
        {
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "Proletaria: Work Reader",
                delegate {
                    if (UserInterface.CurrentState is not null)
                    {
                        UserInterface.Draw(Main.spriteBatch, new GameTime());
                    }
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }
}
