using Godot;

public partial class TelaOver : Node2D
{
	public override void _Ready()
	{
		Label moedas = GetNode<Label>("MoedasObtidas");

		Button botaoMenu = GetNode<Button>("BotaoMenu");
		Button botaoJogar = GetNode<Button>("BotaoJogarNovamente");

		moedas.Text = "Moedas obtidas: " + Mestre.MoedasUltimaPartida;

		botaoMenu.Pressed += VoltarMenu;
		botaoJogar.Pressed += JogarNovamente;
	}

	private void VoltarMenu()
	{
		GetTree().ChangeSceneToFile("res://cenas/menu.tscn");
	}

	private void JogarNovamente()
	{
		GetTree().ChangeSceneToFile("res://cenas/gameplay.tscn");
	}
}
