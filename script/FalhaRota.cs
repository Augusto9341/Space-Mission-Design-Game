using Godot;
using System;

public class FalhaRota : Evento
{
	private FiltroLaranja filtro;
	private Action gameOver;

	public FalhaRota(FiltroLaranja filtro, Action gameOver)
		: base(30, 60)
	{
		this.filtro = filtro;
		this.gameOver = gameOver;
	}

	protected override void Executar()
	{
		GD.Print("ERRO: ROTA INCORRETA!");
		tempoResolucao = 90;
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
		GD.Print("GAME OVER: rota incorreta não corrigida!");
		gameOver?.Invoke();
	}
}
