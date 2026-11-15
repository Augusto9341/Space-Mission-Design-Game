using Godot;

public partial class PainelManutencao : Node2D
{
	private bool aberto = false;
	private Tween animacao;
	private Sprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		sprite.Centered = true;

		float largura = GetViewportRect().Size.X;
		float altura = GetViewportRect().Size.Y;

		// Começa abaixo da tela.
		Position = new Vector2(
			largura / 2,
			altura + sprite.Texture.GetHeight() / 2
		);
	}

	public void AlternarPainel()
	{
		if (animacao != null && animacao.IsRunning())
			return;

		aberto = !aberto;

		float largura = GetViewportRect().Size.X;
		float altura = GetViewportRect().Size.Y;

		Vector2 destino;

		if (aberto)
		{
			destino = new Vector2(largura / 2, altura / 2);
		}
		else
		{
			destino = new Vector2(
				largura / 2,
				altura + sprite.Texture.GetHeight() / 2
			);
		}

		// Cria e executa a animação.
		animacao = CreateTween();

		animacao.SetTrans(Tween.TransitionType.Cubic);
		animacao.SetEase(Tween.EaseType.Out);

		animacao.TweenProperty(
			this,
			"position",
			destino,
			0.5
		);
	}
}
