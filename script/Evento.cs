using Godot;

public abstract class Evento
{
	protected float tempoOcorrencia;
	protected float tempoResolucao;

	protected float intervaloMinimo;
	protected float intervaloMaximo;

	public bool Ativo { get; protected set; }

	protected Evento(float minimo, float maximo)
	{
		intervaloMinimo = minimo;
		intervaloMaximo = maximo;

		SortearIntervalo();
	}

	protected void SortearIntervalo()
	{
		tempoOcorrencia = (float)GD.RandRange(
			intervaloMinimo,
			intervaloMaximo
		);
	}

	public void Atualizar(float delta)
	{
		if (Ativo)
		{
			tempoResolucao -= delta;

			if (tempoResolucao <= 0)
			{
				Ativo = false;
				Expirar();
				EncerrarAviso();
				SortearIntervalo();
			}
		}
		else
		{
			tempoOcorrencia -= delta;

			if (tempoOcorrencia <= 0)
				Iniciar();
		}
	}

	protected void Iniciar()
	{
		Ativo = true;
		Executar();
		Aviso();
	}

	public void Resolver()
	{
		if (!Ativo)
			return;

		Ativo = false;
		EncerrarAviso();
		SortearIntervalo();
	}

	protected abstract void Executar();
	protected abstract void Aviso();
	protected abstract void Expirar();
	protected abstract void EncerrarAviso();
}
