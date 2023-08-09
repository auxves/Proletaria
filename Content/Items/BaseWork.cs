using Microsoft.Xna.Framework;
using Proletaria.Common;
using Proletaria.Common.UI;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Proletaria.Content.Items;

public abstract class BaseWork : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;

        Item.maxStack = 1;
        Item.value = 0;
        Item.rare = ItemRarityID.Cyan;
        Item.useStyle = ItemUseStyleID.Swing;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        FP.Let(tooltips.Find(tooltip => tooltip.Name == "Price"), tooltip =>
        {
            tooltip.Text = Language.GetTextValue("Mods.Proletaria.Tooltips.Priceless");
            tooltip.OverrideColor = Color.LightCoral;
        });
    }

    public override bool? UseItem(Player player)
    {
        ModContent.GetInstance<WorkSystem>().Show(this);
        return base.UseItem(player);
    }
}
