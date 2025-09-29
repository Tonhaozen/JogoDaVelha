namespace JogoDaVelha
{
    class Jogo
    {
        public static ConsoleColor BackgroundColor { get; set; }
        static void Main(string[] args)
        {
            JogoDaVelha jogo = new JogoDaVelha();  //instância do jogo
            jogo.MostrarMenu();  //mostra o menu principal para o usuário
        }
    }
    class JogoDaVelha
    {
        private char[] tabuleiroPosicoes = Array.Empty<char>();   //representa o tabuleiro do jogo (9 posições) //iniciando vazio para o compilador não reclamar
        private char jogadorVez;           //indica de quem é a vez de jogar (X ou O)
        private bool fimJogo;             //controla se o jogo terminou ou não
        private int qtdJogadas;          //conta quantas jogadas já foram feitas

        //Ranking do jogo
        private int pontosX = 0;
        private int pontosO = 0;
        private int pontosEmpate = 0;

        //Jogar contra o computador
        private bool jogarContraPc = false; //true se for jogador vs PC
        private bool dificil = false;       //true se for modo difícil

        //Construtor do jogo
        public JogoDaVelha()
        {
            Reiniciar(); //inicializa o tabuleiro e variáveis
        }


        //Menu principal
        public void MostrarMenu()
        {
            Console.WriteLine("DEBUG: Entrando no menu principal.");
            Console.Clear(); // Se aparecer uma mensagem rapida ao inciar o codigo, é porque debugou e corrigiu bugs, (tambem tem DEBUG, NA SAIDA DO JOGO) :)

            while (true) //loop até o usuário escolher sair
            {
                // Cabeçalho
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("#### JOGO DA VELHA ####");
                Console.ResetColor();

                // Opções
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1 - Jogador VS Jogador");
                Console.WriteLine("2 - Jogador VS Computador");
                Console.WriteLine("3 - Sair");
                Console.ResetColor();

                // Ranking
                Console.WriteLine(); // linha em branco de separaçao
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("----RANKING:----");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Vitórias X: {pontosX}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Vitórias O: {pontosO} ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Empates: {pontosEmpate}");
                Console.ResetColor();

                // Escolher uma opção
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Escolha uma opção: ");
                Console.ResetColor();
                string opcao = Console.ReadLine() ?? "";

                if (opcao == "1")  //define a ação conforme a escolha do usuário
                {
                    jogarContraPc = false; //modo jogador vs jogador
                    Jogar();
                }
                else if (opcao == "2")
                {
                    jogarContraPc = true; //modo jogador vs PC
                    EscolherModoPc();    //seleciona dificuldade
                }
                else if (opcao == "3")
                {
                    break; //sai do menu
                }
                else
                {
                    Console.WriteLine("Opção inválida! Aperte ENTER.");  //entrada inválida
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        //Menu para escolher dificuldade do PC
        private void EscolherModoPc()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("#### JOGAR CONTRA O PC ####");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1 - Modo fácil");
                Console.WriteLine("2 - Modo difícil");
                Console.WriteLine("0 - Voltar");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEscolha: ");

                string opcao = Console.ReadLine() ?? ""; // ?? "" para garantir que não seja nulo

                if (opcao == "1")
                {
                    dificil = false; //modo fácil
                    Jogar();
                    break;
                }
                else if (opcao == "2")
                {
                    dificil = true; //modo difícil
                    Jogar();
                    break;
                }
                else if (opcao == "0")
                {
                    Console.Clear();
                    return; //volta ao menu principal
                }
                else
                {
                    Console.WriteLine("Opção inválida! Aperte ENTER.");  //entrada inválida
                    Console.ReadLine();
                }
            }
        }

        //Inicia o jogo
        private void Jogar()
        {
            Reiniciar(); //reinicia o tabuleiro e variáveis

            while (!fimJogo) //while o jogo não acabar
            {
                MostrarTabuleiro(); //desenha o tabuleiro na tela
                FazerJogada();      //jogador faz sua jogada
                MostrarTabuleiro(); //mostra o tabuleiro atualizado
                ConferirFim();      //verifica se alguém ganhou ou deu empate
                TrocarJogador();    //muda a vez
            }

            //Mensagem ao fim do jogo
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.WriteLine("DEBUG: Fim do jogo alcançado. Aguardando tecla para voltar ao menu...");
            Console.ReadKey();
            Console.WriteLine("DEBUG: Limpando a tela antes de mostrar o menu.");
            Console.Clear(); // limpar tela ao fim da partida
        }

        //Reinicia o tabuleiro e variáveis para novo jogo
        private void Reiniciar()
        {
            tabuleiroPosicoes = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            jogadorVez = 'X';    //sempre começa o jogador X
            fimJogo = false;    //jogo não finalizou ainda
            qtdJogadas = 0;     //nenhuma jogada feita
        }

        //Mostra o tabuleiro atual na tela
        private void MostrarTabuleiro()
        {

            Console.Clear();
            // Para a primeira linha
            Console.ForegroundColor = ConsoleColor.Magenta; // cor que quiser para linha 1
            Console.WriteLine($" {tabuleiroPosicoes[0]} | {tabuleiroPosicoes[1]} | {tabuleiroPosicoes[2]} ");
            Console.ResetColor();
            Console.WriteLine("---+---+---");

            // Para a segunda linha
            Console.ForegroundColor = ConsoleColor.Yellow; // cor que quiser para linha 2
            Console.WriteLine($" {tabuleiroPosicoes[3]} | {tabuleiroPosicoes[4]} | {tabuleiroPosicoes[5]} ");
            Console.ResetColor();
            Console.WriteLine("---+---+---");

            // Para a terceira linha
            Console.ForegroundColor = ConsoleColor.Cyan; // cor que quiser para linha 3
            Console.WriteLine($" {tabuleiroPosicoes[6]} | {tabuleiroPosicoes[7]} | {tabuleiroPosicoes[8]} ");
            Console.ResetColor();

        }

        //Lê e processa a jogada do jogador ou do PC
        private void FazerJogada()
        {
            if (jogarContraPc && jogadorVez == 'O') //vez do PC
            {

                if (dificil)
                    JogadaPcDificil();
                else
                    JogadaPcFacil();
                return;
            }

            //Jogador humano
            Console.WriteLine($"Vez do jogador {jogadorVez}. Escolha uma posição (1-9):");
            bool numero = int.TryParse(Console.ReadLine(), out int posicao);

            //Validação da entrada
            while (!numero || !ValidaJogada(posicao))
            {
                Console.WriteLine("Posição inválida, tente de novo:");
                numero = int.TryParse(Console.ReadLine(), out posicao);
            }

            Preencher(posicao); //registra a jogada
        }

        //Jogada do PC modo fácil
        private void JogadaPcFacil()
        {
            Random random = new Random();
            int posicao;
            do
            {
                posicao = random.Next(1, 10); //escolhe uma posição aleatória
            } while (!ValidaJogada(posicao));

            Preencher(posicao); //preenche no tabuleiro
        }

        //Jogada do PC modo difícil
        private void JogadaPcDificil()
        {
            //possíveis combinações de vitória
            int[,] linhas =
            {
                {1,2,3},{4,5,6},{7,8,9},
                {1,4,7},{2,5,8},{3,6,9},
                {1,5,9},{3,5,7}
            };

            //tenta ganhar primeiro
            for (int i = 0; i < linhas.GetLength(0); i++)
            {
                int a = linhas[i, 0], b = linhas[i, 1], c = linhas[i, 2];
                if (tabuleiroPosicoes[a - 1] == 'O' && tabuleiroPosicoes[b - 1] == 'O' && ValidaJogada(c)) { Preencher(c); return; }
                if (tabuleiroPosicoes[a - 1] == 'O' && tabuleiroPosicoes[c - 1] == 'O' && ValidaJogada(b)) { Preencher(b); return; }
                if (tabuleiroPosicoes[b - 1] == 'O' && tabuleiroPosicoes[c - 1] == 'O' && ValidaJogada(a)) { Preencher(a); return; }
            }

            //depois tenta bloquear o adversário
            for (int i = 0; i < linhas.GetLength(0); i++)
            {
                int a = linhas[i, 0], b = linhas[i, 1], c = linhas[i, 2];
                if (tabuleiroPosicoes[a - 1] == 'X' && tabuleiroPosicoes[b - 1] == 'X' && ValidaJogada(c)) { Preencher(c); return; }
                if (tabuleiroPosicoes[a - 1] == 'X' && tabuleiroPosicoes[c - 1] == 'X' && ValidaJogada(b)) { Preencher(b); return; }
                if (tabuleiroPosicoes[b - 1] == 'X' && tabuleiroPosicoes[c - 1] == 'X' && ValidaJogada(a)) { Preencher(a); return; }
            }

            //se nada disso, joga aleatório
            JogadaPcFacil();
        }

        //Valida a posição escolhida
        private bool ValidaJogada(int posicao)
        {
            return posicao >= 1 && posicao <= 9 && tabuleiroPosicoes[posicao - 1] != 'X' && tabuleiroPosicoes[posicao - 1] != 'O';
        }

        //Preenche a posição escolhida
        private void Preencher(int posicao)
        {
            tabuleiroPosicoes[posicao - 1] = jogadorVez;
            qtdJogadas++;
        }

        //Alterna a vez do jogador
        private void TrocarJogador()
        {
            if (!fimJogo)
                jogadorVez = (jogadorVez == 'X') ? 'O' : 'X';
        }

        //Verifica se alguém ganhou ou se deu empate
        private void ConferirFim()
        {
            int[,] linhas =
            {
                {1,2,3},{4,5,6},{7,8,9},
                {1,4,7},{2,5,8},{3,6,9},
                {1,5,9},{3,5,7}
            };

            //verifica vitória
            for (int i = 0; i < linhas.GetLength(0); i++)
            {
                int a = linhas[i, 0], b = linhas[i, 1], c = linhas[i, 2];
                if (tabuleiroPosicoes[a - 1] == jogadorVez && tabuleiroPosicoes[b - 1] == jogadorVez && tabuleiroPosicoes[c - 1] == jogadorVez)
                {
                    Console.WriteLine($"Jogador {jogadorVez} venceu!");
                    fimJogo = true;
                    if (jogadorVez == 'X') pontosX++; else pontosO++;
                    return;
                }
            }

            //verifica empate
            if (qtdJogadas == 9)
            {
                Console.WriteLine("Empate!");
                pontosEmpate++;
                fimJogo = true;
            }
        }
    }
}