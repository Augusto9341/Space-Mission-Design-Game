using Godot;

public partial class Loja : Control
{
	private Label labelMoedas;

	public override void _Ready()
	{
		labelMoedas = GetNode<Label>("LabelMoedas");

		CarregarMoedas();
	}

	private void CarregarMoedas()
	{
		ConfigFile config = new ConfigFile();

		Error erro = config.Load("user://recorde.cfg");

		int moedas = 0;

		if (erro == Error.Ok)
		{
			moedas = (int)config.GetValue(
				"economia",
				"moedas",
				0
			);
		}

		labelMoedas.Text = moedas.ToString();
	}
}
