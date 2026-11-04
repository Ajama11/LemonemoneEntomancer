using MegaCrit.Sts2.Core.Entities.Creatures;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IModifyBugDamageAdditive
{
    public int ModifyBugDamageAdditive(Creature? target, int amount, Creature dealer);
}