using Godot;
using System;

public class Experimento
{
	private const float intervaloPerda = 3f;

	private int perdaPorIntervalo;
	private float tempo;
	private bool deteriorando = false;
	private bool morreu = false;

	private Action gameOver;

	public int Vida { get; private set; } = 100;

	public Experimento(int perda, Action gameOver)
	{
		perdaPorIntervalo = perda;
		this.gameOver = gameOver;
	}

	// Chamado quando o período de tolerância da energia termina
	public void IniciarDeterioracao()
	{
		if (deteriorando || morreu)
			return;

		deteriorando = true;
		tempo = 0;
	}

	public void Atualizar(float delta)
	{
		if (!deteriorando || morreu)
			return;

		tempo += delta;

		while (tempo >= intervaloPerda)
		{
			tempo -= intervaloPerda;

			Vida -= perdaPorIntervalo;

			if (Vida <= 0)
			{
				Vida = 0;
				morreu = true;
				deteriorando = false;

				GD.Print("EXPERIMENTO BIOLÓGICO MORREU!");
				gameOver?.Invoke();

				return;
			}

			GD.Print("Vida do experimento: ", Vida);
		}
	}
}
