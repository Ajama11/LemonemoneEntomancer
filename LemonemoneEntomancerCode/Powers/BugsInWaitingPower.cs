using BaseLib.Cards.Variables;
using Godot;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Singletons;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class BugsInWaitingPower() : LemonemoneEntomancerPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    private const int BaseBugStrength = 3;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DisplayVar<BugsInWaitingPower>("Display", power => $"{power.Amount * MyHooks.ModifyBugDamageAdditive(null, BaseBugStrength, power.Owner)}")
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
        if (Owner != dealer || !props.IsPoweredAttack()) return 0;

        int totalBugStrength = MyHooks.ModifyBugDamageAdditive(target, BaseBugStrength, dealer);
        
        Data data = GetInternalData<Data>();
        
        return data.CommandToModify != null &&
               cardSource != null &&
               cardSource != data.CommandToModify.ModelSource ||
               data.CommandToModify != null &&
               data.CommandToModify.Attacker != dealer
            ? 0 : Amount * totalBugStrength;
    }

    public override async Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
    {
        Data data = GetInternalData<Data>();
        
        if (data.CommandToModify != command) return;

        foreach (var list in command.Results)
        {
            foreach (var damageResult in list)
            {
                var room = NCombatRoom.Instance;
                var creatureNode = room?.GetCreatureNode(damageResult.Receiver);
                if (creatureNode == null) continue;
                
                for (int i = 0; i < data.AmountWhenAttackStarted; i++)
                {
                    Vector2 randomRay = new Vector2(-50, 0).Rotated(Rng.Chaotic.NextFloat((float) (Math.PI * 0.75f)));
                    
                    Vector2 startPosition = creatureNode.VfxSpawnPosition + randomRay;
                    Vector2 endPosition = creatureNode.VfxSpawnPosition;
        
                    NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(
                        NShivThrowVfx.Create(startPosition, endPosition, new Color("B59A00")));
                }
            }
        }

        await PowerCmd.ModifyAmount(choiceContext, this,
            -data.AmountWhenAttackStarted, null, null);
        
        await BugsSingleton.SpendBugs(Owner.Player!, data.AmountWhenAttackStarted, command.ModelSource as CardModel);

        data.CommandToModify = null;
    }

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props,
        Creature target, CardModel? cardSource)
    {
        if (dealer != Owner) return;
        if (dealer.Player == null) return;

        await MyHooks.OnBugAttackHit(dealer.Player, dealer, result, props, target, cardSource);
    }

    protected override object InitInternalData() => new Data();
    public class Data
    {
        public AttackCommand? CommandToModify;
        public int AmountWhenAttackStarted;
    }
}