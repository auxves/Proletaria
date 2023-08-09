using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Utilities;

namespace Proletaria.Content.NPCs;

public abstract class BaseDweller : ModNPC
{

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 25;

        NPCID.Sets.ExtraFramesCount[Type] = 9;
        NPCID.Sets.DangerDetectRange[Type] = 700;
        NPCID.Sets.AttackType[Type] = 0;
        NPCID.Sets.AttackTime[Type] = 25;
        NPCID.Sets.AttackAverageChance[Type] = 30;
        NPCID.Sets.HatOffsetY[Type] = 4;
    }

    public override void SetDefaults()
    {
        NPC.townNPC = true;
        NPC.friendly = true;
        NPC.width = 18;
        NPC.height = 40;
        NPC.aiStyle = 7;
        NPC.damage = 10;
        NPC.defense = 15;
        NPC.lifeMax = 250;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0.5f;

        AnimationType = NPCID.Guide;
    }

    public override string GetChat()
    {
        var chat = new WeightedRandom<string>();

        Console.WriteLine(Name);

        chat.Add(Language.GetTextValue($"Mods.Proletaria.Dialogue.{Name}.Standard1"));
        chat.Add(Language.GetTextValue($"Mods.Proletaria.Dialogue.{Name}.Standard2"));
        chat.Add(Language.GetTextValue($"Mods.Proletaria.Dialogue.{Name}.Standard3"));
        chat.Add(Language.GetTextValue($"Mods.Proletaria.Dialogue.{Name}.Common"), 5.0);
        chat.Add(Language.GetTextValue($"Mods.Proletaria.Dialogue.{Name}.Rare"), 0.1);

        return chat;
    }
}
