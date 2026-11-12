using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class Unnamed64NoFlyDrawPower() : LemonemoneEntomancerPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(MyEnums.Fly)
    ];

    public override async Task AfterCardDiscarded(PlayerChoiceContext choiceContext, CardModel card)
    {
        if (card.Keywords.Contains(MyEnums.Fly)) return;

        await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, 
            Owner, Amount, 
            Owner, null);
    }
}