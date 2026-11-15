using Godot;
using System;

public class FalhaMeteoro : Evento
{
	private AudioStreamPlayer2D alerta;
	private AudioStreamPlayer2D laser;
	private Action gameOver;

	public FalhaMeteoro(
		AudioStreamPlayer2D alerta,
		AudioStreamPlayer2D laser,
		Action gameOver
	) : base(30, 50)
	{
		this.alerta = alerta;
		this.laser = laser;
		this.gameOver = gameOver;
	}

	protected override void Executar()
	{
		GD.Print("ALERTA: CHUVA DE METEOROS!");
		tempoResolucao = 15;
	}

	protected override void Aviso()
	{
		alerta.Play();
	}

	protected override void EncerrarAviso()
	{
		alerta.Stop();
	}

	protected override void Expirar()
	{
		GD.Print("GAME OVER: impacto de meteoro!");
		gameOver?.Invoke();
	}

	public void DestruirMeteoros()
	{
		if (!Ativo)
			return;

		laser.Play();
		Resolver();

		GD.Print("Meteoros destruídos!");
	}
}
