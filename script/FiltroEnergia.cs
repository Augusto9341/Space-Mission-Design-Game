using Godot;

public partial class FiltroEnergia : ColorRect
{
	public override void _Ready()
	{
		Color = new Color(0, 0, 0, 0);
		Visible = false;
		MouseFilter = MouseFilterEnum.Ignore;
	}

	public void Ativar()
	{
		Visible = true;
		Color = new Color(0, 0, 0, 0.65f);
	}

	public void Desativar()
	{
		Color = new Color(0, 0, 0, 0);
		Visible = false;
	}
}
