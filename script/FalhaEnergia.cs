using Godot;

public class FalhaEnergia : Evento
{
	private FiltroEnergia filtro;
	private Experimento experimento;

	public FalhaEnergia(FiltroEnergia filtro, Experimento experimento)
		: base(20, 50)
	{
		this.filtro = filtro;
		this.experimento = experimento;
	}

	protected override void Executar()
	{
		GD.Print("Falha na energia!");
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
		GD.Print("CONSEQUÊNCIA: experimentos biológicos em risco!");
		experimento.IniciarDeterioracao();
	}
}
