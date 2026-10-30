using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;

public static class SfxCmdExtensions
{
    extension(SfxCmd)
    {
        public static void PlayTmp(string sfx, float volume = 1F, PitchVariance variance = PitchVariance.None)
        {
            NDebugAudioManager.Instance?.Play(sfx, volume, variance);
        }
    }
}