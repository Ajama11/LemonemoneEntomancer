using LemonemoneEntomancer.LemonemoneEntomancerCode.Systems;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.ConsoleCommands;

public class PrepareBugsConsoleCmd : AbstractConsoleCmd
{
    public override string CmdName => "lemonemoneentomancer-preparebugs";
    public override string Args => "<amount:int>";
    public override string Description => "Prepare an amount of Bugs to spend on an Attack.";
    public override bool IsNetworked => true;
    
    public override CmdResult Process(Player? issuingPlayer, string[] args)
    {
        if (!RunManager.Instance.IsInProgress)
        {
            return new CmdResult(success: false, "A run is currently not in progress!");
        }

        if (args.Length == 0)
        {
            return new CmdResult(success: false, "No amount specified.");
        }

        if (!int.TryParse(args[0], out var amount))
        {
            return new CmdResult(success: false, "Arg 0 must be the amount (int), got '" + args[0] + "'.");
        }

        string gainedOrLost = amount >= 0 ? "Prepared" : "Unprepared";
        string pluralBugs = Math.Abs(amount) == 1 ? "Bug" : "Bugs";

        Task task = PrepareOrUnprepareBugs(issuingPlayer!, amount);
        return new CmdResult(task, success: true, $"{gainedOrLost} {amount} {pluralBugs}");
    }

    private static async Task PrepareOrUnprepareBugs(Player player, int amount)
    {
        if (amount < 0)
        {
            await Bugs.LoseBugsInWaiting(player, -amount);
        }
        else
        {
            await Bugs.GainBugsInWaiting(player, amount);
        }
    }
}