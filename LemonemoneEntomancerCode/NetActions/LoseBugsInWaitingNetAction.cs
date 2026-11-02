using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Multiplayer.Serialization;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.NetActions;

public class LoseBugsInWaitingNetAction : INetAction
{
    public int Amount;
    
    public GameAction ToGameAction(Player player) => new LoseBugsInWaitingAction(player, Amount);

    public void Serialize(PacketWriter writer)
    {
        writer.WriteInt(Amount);
    }

    public void Deserialize(PacketReader reader)
    {
        Amount = reader.ReadInt();
    }

    public override string ToString() => nameof(LoseBugsInWaitingNetAction);
}