using BaseLib.Cards.Variables;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Systems;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class BugsInWaitingPower() : LemonemoneEntomancerPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    private const int BugStrengthPlaceholder = 3;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DisplayVar<BugsInWaitingPower>("Display", power => $"{power.Amount * BugStrengthPlaceholder}")
    ];

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
        data.AmountWhenAttackStarted = Amount;
        
        return Task.CompletedTask;
    }
    
    public override decimal ModifyDamageAdditive(Creature? target, decimal amount,
        ValueProp props, Creature? dealer, CardModel? cardSource, CardPlay? cardPlay)
    {
        if (Owner != dealer || !props.IsPoweredAttack()) return 0M;
        
        Data data = GetInternalData<Data>();
        
        return data.CommandToModify != null &&
               cardSource != null &&
               cardSource != data.CommandToModify.ModelSource ||
               data.CommandToModify != null &&
               data.CommandToModify.Attacker != dealer
            ? 0M : Amount * BugStrengthPlaceholder;
    }

    public override async Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
    {
        Data data = GetInternalData<Data>();
        
        if (data.CommandToModify != command) return;

        await PowerCmd.ModifyAmount(choiceContext, this,
            -data.AmountWhenAttackStarted, null, null);

        await Bugs.SpendBugs(Owner.Player!, data.AmountWhenAttackStarted);

        data.CommandToModify = null;
    }

    protected override object InitInternalData() => new Data();
    public class Data
    {
        public AttackCommand? CommandToModify;
        public int AmountWhenAttackStarted;
    }
}