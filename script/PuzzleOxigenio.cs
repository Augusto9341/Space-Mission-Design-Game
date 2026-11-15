
using Godot;
using System;
using System.Collections.Generic;

public partial class PuzzleOxigenio : Control
{
	private Label enunciado;
	private LineEdit resposta;
	private Button confirmar;
	private Button fechar;

	private List<(string pergunta, string resposta)> questoes = new();

	private string respostaCorreta;

	// Referência ao evento de oxigênio
	private Evento eventoOxigenio;

	public override void _Ready()
	{
		enunciado = GetNode<Label>("Enunciado");
		resposta = GetNode<LineEdit>("Resposta");
		confirmar = GetNode<Button>("Confirmar");
		fechar = GetNode<Button>("Fechar");

		confirmar.Pressed += VerificarResposta;
		fechar.Pressed += () => Visible = false;

		CarregarQuestoes();

		Visible = false;
	}

	public void ConfigurarEvento(Evento evento)
	{
		eventoOxigenio = evento;
	}

	private void CarregarQuestoes()
	{
		using var arquivo = FileAccess.Open(
			"res://dados/oxigenio.txt",
			FileAccess.ModeFlags.Read
		);

		if (arquivo == null)
		{
			GD.PrintErr("Não foi possível carregar as questões.");
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
			string respostaCorreta = partes[1].Trim();

			questoes.Add((pergunta, respostaCorreta));
		}
	}

	public void AbrirPuzzle()
	{
		if (questoes.Count == 0)
			return;

		int indice = (int)GD.RandRange(0, questoes.Count - 1);

		enunciado.Text = questoes[indice].pergunta;
		respostaCorreta = questoes[indice].resposta;

		resposta.Text = "";
		Visible = true;
	}

	private void VerificarResposta()
	{
		if (resposta.Text.Trim() == respostaCorreta)
		{
			GD.Print("Oxigênio restaurado!");

			eventoOxigenio?.Resolver();

			Visible = false;
		}
		else
		{
			GD.Print("Resposta incorreta!");
		}
	}
}
