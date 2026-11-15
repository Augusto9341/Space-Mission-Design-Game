
using Godot;
using System;
using System.Collections.Generic;

public partial class PuzzleMotor : Control
{
	private Label enunciado;
	private LineEdit respostaSoma;
	private LineEdit respostaProduto;

	private Button confirmar;
	private Button fechar;

	private List<(string pergunta, string soma, string produto)> questoes = new();

	private string somaCorreta;
	private string produtoCorreto;

	private Evento eventoMotor;

	public override void _Ready()
	{
		enunciado = GetNode<Label>("Enunciado");

		respostaSoma = GetNode<LineEdit>("RespostaSoma");
		respostaProduto = GetNode<LineEdit>("RespostaProduto");

		confirmar = GetNode<Button>("Confirmar");
		fechar = GetNode<Button>("Fechar");

		confirmar.Pressed += VerificarResposta;
		fechar.Pressed += () => Visible = false;

		CarregarQuestoes();

		Visible = false;
	}

	public void ConfigurarEvento(Evento evento)
	{
		eventoMotor = evento;
	}

	private void CarregarQuestoes()
	{
		using var arquivo = FileAccess.Open(
			"res://dados/motor.txt",
			FileAccess.ModeFlags.Read
		);

		if (arquivo == null)
		{
			GD.PrintErr("Não foi possível carregar motor.txt");
			return;
		}

		string conteudo = arquivo.GetAsText();

		string[] blocos = conteudo.Split(
			"[PERGUNTA]",
			StringSplitOptions.RemoveEmptyEntries
		);

		foreach (string bloco in blocos)
		{
			string[] partes = bloco.Split(
				"[RESPOSTA]",
				StringSplitOptions.RemoveEmptyEntries
			);

			if (partes.Length != 2)
				continue;

			string pergunta = partes[0].Trim();

			string[] respostas = partes[1]
				.Split(
					new[] { '\r', '\n' },
					StringSplitOptions.RemoveEmptyEntries
				);

			if (respostas.Length != 2)
			{
				GD.PrintErr("Questão do motor com quantidade incorreta de respostas.");
				continue;
			}

			questoes.Add((
				pergunta,
				respostas[0].Trim(),
				respostas[1].Trim()
			));
		}

		GD.Print("Questões do motor carregadas: ", questoes.Count);
	}

	public void AbrirPuzzle()
	{
		if (questoes.Count == 0)
		{
			GD.PrintErr("Não existem questões do motor disponíveis.");
			return;
		}

		int indice = (int)GD.RandRange(0, questoes.Count - 1);

		enunciado.Text = questoes[indice].pergunta;

		somaCorreta = questoes[indice].soma;
		produtoCorreto = questoes[indice].produto;

		respostaSoma.Text = "";
		respostaProduto.Text = "";

		Visible = true;
	}

	private void VerificarResposta()
	{
		bool somaCerta =
			respostaSoma.Text.Trim() == somaCorreta;

		bool produtoCerto =
			respostaProduto.Text.Trim() == produtoCorreto;

		if (somaCerta && produtoCerto)
		{
			GD.Print("Motor restaurado!");

			eventoMotor?.Resolver();

			Visible = false;
		}
		else
		{
			GD.Print("Respostas incorretas!");
		}
	}
}
