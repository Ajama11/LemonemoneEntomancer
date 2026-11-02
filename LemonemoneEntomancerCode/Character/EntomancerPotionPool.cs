using BaseLib.Abstracts;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using Godot;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Character;

public class EntomancerPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => Entomancer.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}