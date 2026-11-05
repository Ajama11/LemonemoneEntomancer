using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class BuzzedPower() : LemonemoneEntomancerPower
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    private const string ShouldDisplay = "ShouldDisplay";
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DisplayVar<BuzzedPower>("Display", power => $"{power.GetInternalData<Data>().AmountToLose}"),
        new BoolVar(ShouldDisplay, false)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<Swarm>()
    ];

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power != this || applier?.Player == null) return Task.CompletedTask;

        var data = GetInternalData<Data>();
        
        if (!data.Appliers.Contains(applier.Player))
            data.Appliers.Add(applier.Player);
        
        return Task.CompletedTask;
    }

    public override Task BeforeAttack(AttackCommand command)
    {
        if (command.Attacker != Owner || !command.DamageProps.IsPoweredAttack())
            return Task.CompletedTask;
        
        Data data = GetInternalData<Data>();
        
        if (data.CommandToModify != null ||
            command.ModelSource != null &&
            command.ModelSource is not CardModel ||
            !command.DamageProps.IsPoweredAttack())
        {
            return Task.CompletedTask;
        }
        
        data.CommandToModify = command;
        
        return Task.CompletedTask;
    }
    
    public override decimal ModifyDamageAdditive(Creature? target, decimal amount,
        ValueProp props, Creature? dealer, CardModel? cardSource, CardPlay? cardPlay)
    {
        if (Owner != dealer || !props.IsPoweredAttack()) return 0;
        
        Data data = GetInternalData<Data>();

        data.AmountToLose = Math.Max(0, Math.Min(amount, Amount));
        DynamicVars[ShouldDisplay].BoolVal = data.AmountToLose > 0;
        
        return data.CommandToModify != null &&
               cardSource != null &&
               cardSource != data.CommandToModify.ModelSource ||
               data.CommandToModify != null &&
               data.CommandToModify.Attacker != dealer
            ? 0 : -Amount;
    }

    public override async Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
    {
        Data data = GetInternalData<Data>();
        
        if (data.CommandToModify != command) return;

        await PowerCmd.ModifyAmount(choiceContext, this,
            -data.AmountToLose, null, null);

        if (data.AmountToLose > 0)
        {
            foreach (var player in data.Appliers)
            {
                await MyActions.CreateSwarm(1, player, CombatState);
            }
        }

        data.CommandToModify = null;
        data.AmountToLose = 0;
        DynamicVars[ShouldDisplay].BoolVal = false;
    }

    public override Task AfterEnergyReset(Player player)
    {
        if (Owner.Monster == null) return Task.CompletedTask;
        DynamicVars[ShouldDisplay].BoolVal = Owner.Monster.IntendsToAttack;
        
        return Task.CompletedTask;
    }

    protected override object InitInternalData() => new Data();
    public class Data
    {
        public AttackCommand? CommandToModify;
        public decimal AmountToLose;
        public readonly List<Player> Appliers = [];
    }
}