using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class Unnamed59FirstSwarmPower() : LemonemoneEntomancerPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<Swarm>()
    ];

    public override Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card.Owner.Creature != Owner || card is not Swarm)
            return Task.CompletedTask;

        List<CardDrawnEntry> entries = CombatManager.Instance.History.Entries
            .OfType<CardDrawnEntry>()
            .ToList();
        
        int swarmsDrawnThisTurn = entries.Count(IsSwarmThisTurn);
        if (swarmsDrawnThisTurn > Amount) return Task.CompletedTask;
        
        Flash();

        CardModel? swarm = entries
            .Where(IsSwarmThisTurn)
            .ElementAtOrDefault(swarmsDrawnThisTurn - 1)
            ?.Card;

        if (swarm != null) CardCmd.Upgrade(swarm);

        return Task.CompletedTask;
    }
    
    private bool IsSwarmThisTurn(CardDrawnEntry e)
    {
        return e.HappenedThisTurn(CombatState) && 
               e.Actor == Owner &&
               e.Card is Swarm;
    }
}