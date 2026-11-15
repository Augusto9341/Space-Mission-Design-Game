using Godot;

public partial class FiltroLaranja : ColorRect
{
	private Tween animacao;

	public override void _Ready()
	{
		Color = new Color(1, 0.4f, 0, 0);
		Visible = false;
		MouseFilter = MouseFilterEnum.Ignore;
	}

	public void Ativar()
	{
		Visible = true;

		if (animacao != null && animacao.IsRunning())
			animacao.Kill();

		Color = new Color(1, 0.4f, 0, 0.05f);

		animacao = CreateTween();
		animacao.SetLoops();

		animacao.TweenProperty(this, "color:a", 0.4f, 0.5);
		animacao.TweenProperty(this, "color:a", 0.05f, 0.5);
	}

	public void Desativar()
	{
		if (animacao != null && animacao.IsRunning())
			animacao.Kill();

		Color = new Color(1, 0.4f, 0, 0);
		Visible = false;
	}
}
