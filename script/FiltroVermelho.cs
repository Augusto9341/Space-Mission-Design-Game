using Godot;

public partial class FiltroVermelho : ColorRect
{
	private Tween animacao;

	public override void _Ready()
	{
		Color = new Color(1, 0, 0, 0);
		Visible = false;
	}

	public void Ativar()
	{
		Visible = true;

		if (animacao != null && animacao.IsRunning())
			animacao.Kill();

		animacao = CreateTween();
		animacao.SetLoops();

		animacao.TweenProperty(
			this,
			"color:a",
			0.35f,
			0.5
		);

		animacao.TweenProperty(
			this,
			"color:a",
			0.05f,
			0.5
		);
	}

	public void Desativar()
	{
		if (animacao != null && animacao.IsRunning())
			animacao.Kill();

		animacao = CreateTween();

		animacao.TweenProperty(
			this,
			"color:a",
			0,
			0.2
		);

		animacao.TweenCallback(
			Callable.From(() => Visible = false)
		);
	}
}
