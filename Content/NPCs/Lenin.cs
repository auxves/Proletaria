using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Proletaria.Content.NPCs;

[AutoloadHead]
public class Lenin : BaseDweller
{

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        NPC.Happiness
            .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
            .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike)
            .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love)
            .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
            .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike)
            .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate);
    }
}
