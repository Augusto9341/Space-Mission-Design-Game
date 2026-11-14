# Tópicos — o que a gente precisa fazer

Baseado no áudio de 2min41s. A transcrição foi automática, então os pontos duvidosos estão marcados com **(confirmar)** no final.

## O que você descreveu (resumo)

1. A história começa no **lançamento do foguete**.
2. Depois do lançamento aparece a **tela inicial**, onde o usuário monitora tudo.
3. A **Central da Terra** conversa com o usuário e **explica as mecânicas** de manutenção da nave.
4. Vem um **período de teste inicial** (tutorial).
5. Depois começam as **waves**: uma corrida de quests e desafios, com tempo limite, subindo de **nível 1 → 2 → 3**.
6. A Central dá as missões, por exemplo: ver a temperatura do gerador, ver se o ar está circulando, ver se o material biológico está vivo e qual a temperatura correta.
7. A cada fase entram **novas dificuldades**. Na última, vários problemas acontecem **ao mesmo tempo**.
8. Cada quest vale **pontos**. Para a missão dar certo, o jogador precisa **atender todos os objetivos**.
9. Se errar muito, ou deixar tudo parado, o **material biológico** e a **vida do tripulante** são prejudicados. A missão pode ser **perdida**.

---

## 1. História e fluxo do jogo
- [ ] Tela de **lançamento do foguete** (cena de abertura).
- [ ] Transição do lançamento para a **tela inicial de monitoramento** (central de controle).
- [ ] Definir a sequência completa: lançamento → tutorial → wave 1 → wave 2 → wave 3 → resultado.

## 2. Central da Terra (Controle de Missão)
- [ ] Caixa de diálogo / rádio com as mensagens da Terra (arte + texto).
- [ ] Escrever os textos que **explicam cada mecânica** (temperatura, energia, ar/O2, material biológico).
- [ ] A Terra também **dá as missões** durante as waves ("verifique a temperatura do gerador", "confirme o material biológico").
- [ ] Alertas/avisos da Terra quando o jogador demora ou erra.

## 3. Tutorial (período de teste inicial)
- [ ] Tutorial guiado, sem falha fatal, apresentando um painel por vez.
- [ ] Marcar quando o tutorial termina e a primeira wave começa.

## 4. Sistema de waves (3 níveis)
- [ ] Criar o controlador de waves (hoje o jogo tem uma missão única de 5 min).
- [ ] Wave 1, 2 e 3, cada uma com **tempo limite** e lista de quests.
- [ ] A dificuldade sobe a cada wave: mais tarefas, menos tempo, novas mecânicas.
- [ ] Última wave: **todos os problemas simultâneos** (reaproveitar o `EventManager`).

## 5. Quests e puzzles
- [ ] Sistema de **quests** (tarefa, tempo, pontos, estado: pendente/concluída/falhou).
- [ ] Criar as quests citadas:
  - [ ] Checar a **temperatura do gerador**.
  - [ ] Checar o **ar** (circulação/oxigênio).
  - [ ] Checar se o **material biológico está vivo**.
  - [ ] Ajustar a **temperatura correta** para o material.
- [ ] Mais quests e puzzles conforme as waves avançam.

## 6. Pontos e condição de sucesso/falha
- [ ] **Pontuação** por quest concluída.
- [ ] Meta mínima de pontos por wave para a missão dar certo.
- [ ] Erros acumulam **dano**; muitos erros causam **falha da missão**.
- [ ] Tela de resultado por wave e final (já existe uma versão com 4 critérios).

## 7. Consequências de negligência
- [ ] Deixar parado prejudica o **material biológico** (já existe saúde da cultura).
- [ ] Criar a **saúde do tripulante** (hoje só existe o oxigênio, com 8 s até a falha).
- [ ] Ligar as duas: ar ruim, temperatura errada e falta de ação reduzem as duas barras.

## 8. Sistemas monitorados
- [ ] **Temperatura do gerador** (novo, ligado à sala do motor).
- [ ] Ar / oxigênio (já existe).
- [ ] Temperatura e saúde do material biológico (já existe).
- [ ] Energia (já existe).

## 9. Arte e ambientes
Já feitos em pixel art:
- [x] Cabine de controle
- [x] Armário de experimentos
- [x] Sala do motor e refrigeração
- [x] Sala de O2 e caixa de força

Faltam:
- [ ] Cena de lançamento do foguete
- [ ] Rosto/tela da Central da Terra
- [ ] Ícones e HUD em pixel art (pontos, tempo, alertas)
- [ ] (opcional) Comunicação e pressão/válvulas

## 10. Integração no Godot
- [ ] Colocar as 4 imagens no projeto como cenários.
- [ ] Navegação entre salas (clicar na cabine, armário, motor, O2/força).
- [ ] Trocar os painéis de texto pelos elementos das imagens (disjuntores clicáveis, termômetros, gavetas).
- [ ] Alertas piscando na sala certa.

## 11. Som e acabamento
- [ ] Alarmes, cliques, ambiente da nave.
- [ ] Música/efeito do lançamento.
- [ ] Balancear o tempo e os pontos de cada wave.

## 12. Entrega do hackathon
- [ ] Texto de descrição do projeto.
- [ ] Referências científicas da NASA (por exemplo, BioNutrients-2).
- [ ] Vídeo ou demonstração curta.
- [ ] Revisar os requisitos oficiais do desafio na aba *Resources*.

---

## Ordem sugerida
1. Fluxo: lançamento → tela inicial → Central da Terra → tutorial.
2. Sistema de waves + quests + pontos.
3. Saúde do tripulante e temperatura do gerador.
4. Integrar a arte e a navegação entre salas.
5. Som, balanceamento e entrega.

## Pontos para confirmar
- **(confirmar)** São exatamente **3 waves**? Entendi "essas três" como níveis 1, 2 e 3.
- **(confirmar)** Cada quest tem pontos e **todas precisam ser cumpridas** para a missão dar certo? Ou existe uma meta mínima?
- **(confirmar)** A parte do áudio sobre "tudo acontecendo ao mesmo tempo" é a **última wave**?
- **(confirmar)** O planejamento de equipamentos e orçamento (Etapa A do GDD) continua **antes** do lançamento?
- **(confirmar)** Um trecho do meio do áudio ficou ininteligível na transcrição (algo sobre "algumas ... até o ..."). Vale me contar se havia mais uma ideia ali.
