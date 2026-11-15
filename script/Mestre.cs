
using Godot;
using System.Collections.Generic;

public partial class Mestre : Node
{
	public bool problem;

	// Moedas da última partida, acessíveis pela tela de Game Over.
	public static int MoedasUltimaPartida { get; private set; }

	public int Moedas { get; private set; } = 0;

	private int estado;
	private float tempo;
	private bool manutencao;
	private bool encerrando = false;

	private TextureRect background;
	private PainelManutencao painel;

	private List<Evento> eventos = new List<Evento>();

	// Botões
	private Button meteoritos;
	private Button fusivel;
	private Button motor;
	private Button oxigenio;

	// Filtros
	private FiltroVermelho filtroVermelho;
	private FiltroEnergia filtroEnergia;
	private FiltroLaranja filtroLaranja;

	// Eventos
	private FalhaOxigenio eventoOxigenio;
	private FalhaRota eventoMotor;
	private FalhaMeteoro eventoMeteoro;
	private FalhaEnergia eventoEnergia;

	// Experimento biológico
	private Experimento experimento;
	private Label vidaExperimento;

	// Áudios
	private AudioStreamPlayer2D alertaMeteoro;
	private AudioStreamPlayer2D laserMeteoro;

	// Painéis dos puzzles
	private PuzzleOxigenio puzzleOxigenio;
	private PuzzleMotor puzzleMotor;

	// Pontuação
	private double pontuacao = 0;
	private int recorde = 0;

	// =========================
	// ATUALIZAÇÃO DOS BOTÕES
	// =========================

	private void AtualizarBotoes()
	{
		meteoritos.Disabled = (estado != 0);
		oxigenio.Disabled = (estado != 2);
		fusivel.Disabled = (estado != 2);
		motor.Disabled = (estado != 3);

		GD.Print("Estado atual: ", estado);
		GD.Print("Oxigênio: ", oxigenio.Disabled);
		GD.Print("Fusível: ", fusivel.Disabled);
		GD.Print("Motor: ", motor.Disabled);
		GD.Print("Meteoritos: ", meteoritos.Disabled);
	}

	// =========================
	// INICIALIZAÇÃO
	// =========================

	public override void _Ready()
	{
		estado = 0;
		problem = false;
		manutencao = false;

		// MOVIMENTAÇÃO

		Button virarDireita = GetNode<Button>("virarDireita");
		virarDireita.Pressed += virarDireitaF;

		Button virarEsquerda = GetNode<Button>("virarEsquerda");
		virarEsquerda.Pressed += virarEsquerdaF;

		// BACKGROUND

		background = GetNode<TextureRect>("Background");

		// PAINEL DE MANUTENÇÃO

		Button action = GetNode<Button>("action");

		painel = GetNode<PainelManutencao>("PainelManutencao");

		vidaExperimento = painel.GetNode<Label>("VidaExperimento");

		action.Pressed += () =>
		{
			manutencao = !manutencao;

			virarDireita.Disabled = manutencao;
			virarEsquerda.Disabled = manutencao;

			painel.AlternarPainel();
		};

		// EVENTO DE ENERGIA

		filtroEnergia = GetNode<FiltroEnergia>("FiltroEnergia");

		experimento = new Experimento(10, GameOver);

		eventoEnergia = new FalhaEnergia(
			filtroEnergia,
			experimento
		);

		eventos.Add(eventoEnergia);

		fusivel = GetNode<Button>("Botoes/fusivel");

		fusivel.Pressed += () =>
		{
			if (eventoEnergia.Ativo)
			{
				eventoEnergia.Resolver();
				GD.Print("Energia restabelecida!");
			}
		};

		// EVENTO DE OXIGÊNIO

		filtroVermelho = GetNode<FiltroVermelho>("FiltroVermelho");

		eventoOxigenio = new FalhaOxigenio(
			filtroVermelho,
			GameOver
		);

		eventos.Add(eventoOxigenio);

		puzzleOxigenio = GetNode<PuzzleOxigenio>(
            "Interface/PuzzleOxigenio"
		);

		puzzleOxigenio.ConfigurarEvento(eventoOxigenio);

		oxigenio = GetNode<Button>("Botoes/O2button");

		oxigenio.Pressed += puzzleOxigenio.AbrirPuzzle;

		// EVENTO DO MOTOR / ROTA

		filtroLaranja = GetNode<FiltroLaranja>("FiltroLaranja");

		eventoMotor = new FalhaRota(
			filtroLaranja,
			GameOver
		);

		eventos.Add(eventoMotor);

		puzzleMotor = GetNode<PuzzleMotor>(
            "Interface/PuzzleMotor"
		);

		puzzleMotor.ConfigurarEvento(eventoMotor);

		motor = GetNode<Button>("Botoes/rota");

		motor.Pressed += puzzleMotor.AbrirPuzzle;

		// EVENTO DE METEORITOS

		alertaMeteoro = GetNode<AudioStreamPlayer2D>(
            "AlertaMeteoro"
		);

		laserMeteoro = GetNode<AudioStreamPlayer2D>(
            "LaserMeteoro"
		);

		eventoMeteoro = new FalhaMeteoro(
			alertaMeteoro,
			laserMeteoro,
			GameOver
		);

		eventos.Add(eventoMeteoro);

		meteoritos = GetNode<Button>("Meteoritos");

		meteoritos.Pressed += eventoMeteoro.DestruirMeteoros;

		// INICIALIZA O AMBIENTE

		CarregarRecorde();

		mudarEstado(0);
	}

	// =========================
	// MOVIMENTAÇÃO
	// =========================

	private void mudarEstado(int novoEstado)
	{
		estado = novoEstado;

		switch (estado)
		{
			case 0:
				background.Texture = GD.Load<Texture2D>(
                    "res://sprites/backgrounds/cabine_pixel.png"
				);
				break;

			case 1:
				background.Texture = GD.Load<Texture2D>(
                    "res://sprites/backgrounds/armario_pixel.png"
				);
				break;

			case 2:
				background.Texture = GD.Load<Texture2D>(
                    "res://sprites/backgrounds/sala_o2_forca_pixel.png"
				);
				break;

			case 3:
				background.Texture = GD.Load<Texture2D>(
                    "res://sprites/backgrounds/sala_motor_pixel.png"
				);
				break;
		}

		AtualizarBotoes();
	}

	private void virarDireitaF()
	{
		if (estado < 3)
			mudarEstado(estado + 1);
	}

	private void virarEsquerdaF()
	{
		if (estado > 0)
			mudarEstado(estado - 1);
	}

	// =========================
	// ATUALIZAÇÃO DOS EVENTOS
	// =========================

	public override void _Process(double delta)
	{
		if (encerrando)
			return;

		pontuacao += delta;

		foreach (Evento evento in eventos)
			evento.Atualizar((float)delta);

		experimento.Atualizar((float)delta);

		vidaExperimento.Text =
			$"VIDA DO EXPERIMENTO: {experimento.Vida}%";
	}

	// =========================
	// SISTEMA DE MOEDAS
	// =========================

	private void SalvarMoedas()
	{
		ConfigFile config = new ConfigFile();

		config.Load("user://recorde.cfg");

		config.SetValue("economia", "moedas", Moedas);

		Error erro = config.Save("user://recorde.cfg");

		if (erro != Error.Ok)
			GD.PrintErr("Erro ao salvar as moedas: ", erro);
	}

	// =========================
	// GAME OVER
	// =========================

	private void GameOver()
	{
		if (encerrando)
			return;

		encerrando = true;

		int moedasGanhas = (int)(pontuacao / 2);

		MoedasUltimaPartida = moedasGanhas;

		Moedas += moedasGanhas;

		GD.Print("Pontuação da partida: ", (int)pontuacao);
		GD.Print("Moedas recebidas: ", moedasGanhas);
		GD.Print("Saldo total: ", Moedas);

		SalvarMoedas();
		SalvarPontuacao();

		CallDeferred(nameof(TrocarCena));
	}

	private void TrocarCena()
	{
		Error resultado = GetTree().ChangeSceneToFile(
            "res://cenas/GameOver.tscn"
		);

		if (resultado != Error.Ok)
			GD.PrintErr("Erro ao abrir Game Over: ", resultado);
	}

	// =========================
	// SISTEMA DE PONTUAÇÃO
	// =========================

	private void CarregarRecorde()
	{
		ConfigFile config = new ConfigFile();

		Error erro = config.Load("user://recorde.cfg");

		if (erro == Error.Ok)
		{
			recorde = (int)config.GetValue(
				"pontuacao",
				"recorde",
				0
			);

			Moedas = (int)config.GetValue(
				"economia",
				"moedas",
				0
			);
		}

		GD.Print("Recorde atual: ", recorde);
		GD.Print("Moedas disponíveis: ", Moedas);
	}

	private void SalvarPontuacao()
	{
		int pontuacaoFinal = (int)pontuacao;

		if (pontuacaoFinal > recorde)
		{
			recorde = pontuacaoFinal;

			// Carrega o arquivo existente para preservar as moedas.
			ConfigFile arquivo = new ConfigFile();
			arquivo.Load("user://recorde.cfg");

			arquivo.SetValue(
				"pontuacao",
				"recorde",
				recorde
			);

			Error erro = arquivo.Save("user://recorde.cfg");

			if (erro != Error.Ok)
				GD.PrintErr("Erro ao salvar o recorde: ", erro);
			else
				GD.Print("NOVO RECORDE: ", recorde);
		}

		GD.Print("Pontuação final: ", pontuacaoFinal);
	}
}
