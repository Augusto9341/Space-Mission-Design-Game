using Godot;
using System;

public class FalhaOxigenio : Evento
{
	private FiltroVermelho filtro;
	private Action gameOver;

	public FalhaOxigenio(FiltroVermelho filtro, Action gameOver)
		: base(5, 15)
	{
		this.filtro = filtro;
		this.gameOver = gameOver;
	}

	protected override void Executar()
	{
		GD.Print("Falha no oxigênio!");
		tempoResolucao = 15;
	}

	protected override void Aviso()
	{
		filtro.Ativar();
	}

	protected override void EncerrarAviso()
	{
		filtro.Desativar();
	}

	protected override void Expirar()
	{
		GD.Print("GAME OVER");
		gameOver?.Invoke();
	}
}
