using BaseLib.Abstracts;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using Godot;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Character;

public class LemonemoneEntomancerPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => LemonemoneEntomancer.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}