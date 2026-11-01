using Godot;
using HarmonyLib;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Nodes;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Patches;

[HarmonyPatch]
public class BugWidgetPatches
{
    [HarmonyPatch(typeof(NCombatUi), nameof(NCombatUi.Activate))]
    [HarmonyPostfix]
    public static void CombatUiActivatePostfix(NCombatUi __instance, CombatState state)
    {
        var widget = PreloadManager.Cache.GetScene("res://LemonemoneEntomancer/scenes/bug_widget.tscn")
            .Instantiate<Control>();
        if (widget is not NBugWidget bugWidget) return;
        
        bugWidget.Initialize(LocalContext.GetMe(state)!);
        
        __instance._energyCounter.AddChildSafely(bugWidget);
        __instance._energyCounter.MoveChildSafely(bugWidget, 0);
        bugWidget.Position = new Vector2(__instance._energyCounter.Size.X / 2, -61);
    }
}