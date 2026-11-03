using BaseLib.Audio;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.GodotExtensions;
using MegaCrit.Sts2.Core.Random;
using Timer = Godot.Timer;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;

public static class MySounds
{
    #region Core

    public static void PlayRandom(ModSound[] sounds,
        float volumeAdd = 0F, float volumeMult = 1F,
        float pitchVariation = 0F, float basePitch = 1F)
    {
        Rng.Chaotic.NextItem(sounds)!.Play(volumeAdd, volumeMult, pitchVariation, basePitch);
    }
    
    private static Timer? PlayWithCooldown(Timer? timer,
        ModSound? singleSfx = null, ModSound[]? multipleSfx = null,
        float volumeAdd = 0F, float volumeMult = 1F,
        float pitchVariation = 0F, float basePitch = 1F)
    {
        if ((timer == null || !timer.IsValid()) && NRun.Instance != null)
        {
            timer = new Timer();
            timer.WaitTime = 0.05;
            timer.OneShot = true;
            
            NRun.Instance.GlobalUi.AddChildSafely(timer);
        }

        if (timer == null || !timer.IsValid() || !timer.IsStopped()) return timer;

        if (singleSfx != null)
        {
            singleSfx.Play(volumeAdd, volumeMult, pitchVariation, basePitch);
        }
        else if (multipleSfx != null)
        {
            PlayRandom(multipleSfx, volumeAdd, volumeMult, pitchVariation, basePitch);
        }
        
        timer.Start();

        return timer;
    }

    #endregion
    
    // private static readonly ModSound GainBug1 = new("res://LemonemoneEntomancer/audio/bugs/gain1.ogg");
    // private static readonly ModSound PhobiaGainBug1 = new("res://LemonemoneEntomancer/audio/bugs/gain_phobia1.ogg");
}