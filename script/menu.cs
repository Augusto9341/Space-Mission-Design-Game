using Godot;

public partial class menu : Node
{
	public override void _Ready()
	{
		Button botao = GetNode<Button>("iniciar");
		botao.Pressed += AoClicar;

		Button loja = GetNode<Button>("Loja");
		loja.Pressed += IrasCompras;
	}

	private void AoClicar()
	{
		GD.Print("aaa");
		GetTree().ChangeSceneToFile("res://cenas/gameplay.tscn");
	}

	private void IrasCompras()
	{
		GD.Print("LOJA");
		GetTree().ChangeSceneToFile("res://cenas/Loja.tscn");
	}
}
