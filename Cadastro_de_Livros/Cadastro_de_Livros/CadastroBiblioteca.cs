using Cadastro_de_Livros;
using System;
using System.Collections.Generic;
using System.IO;

class CadastroBiblioteca
{
    static void addLivro(List<Livro> listaLivros)
    {
        Livro novoLivro = new Livro();
        Console.WriteLine("*** Dados do Novo Livro ***");
        Console.Write("Título: ");
        novoLivro.titulo = Console.ReadLine() ?? "";
        Console.Write("Autor: ");
        novoLivro.autor = Console.ReadLine() ?? "";
        Console.Write("Ano: ");
        novoLivro.ano = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Prateleira: ");
        novoLivro.prateleira = Console.ReadLine() ?? "";
        listaLivros.Add(novoLivro);
    }

    static void mostrarLivros(List<Livro> listaLivros)
    {
        Console.WriteLine("*** Livros Cadastrados ***");
        if (listaLivros.Count == 0)
        {
            Console.WriteLine("Nenhum livro cadastrado.");
            return;
        }

        for (int i = 0; i < listaLivros.Count; i++)
        {
            Console.WriteLine($"*** Livro {i + 1} ***");
            Console.WriteLine($"Título: {listaLivros[i].titulo}");
            Console.WriteLine($"Autor: {listaLivros[i].autor}");
            Console.WriteLine($"Ano: {listaLivros[i].ano}");
            Console.WriteLine($"Prateleira: {listaLivros[i].prateleira}\n");
        }
    }

    static bool buscarPorTitulo(List<Livro> listaLivros, string tituloBusca)
    {
        bool encontrado = false;
        for (int i = 0; i < listaLivros.Count; i++)
        {
            if (listaLivros[i].titulo.ToUpper().Contains(tituloBusca.ToUpper()))
            {
                Console.WriteLine($"Título: {listaLivros[i].titulo} | Prateleira: {listaLivros[i].prateleira}");
                encontrado = true;
            }
        }
        return encontrado;
    }

    static bool buscarMaisNovosQueAno(List<Livro> listaLivros, int anoReferencia)
    {
        bool encontrado = false;
        for (int i = 0; i < listaLivros.Count; i++)
        {
            if (listaLivros[i].ano > anoReferencia)
            {
                Console.WriteLine("*** Livro Encontrado ***");
                Console.WriteLine($"Título: {listaLivros[i].titulo}");
                Console.WriteLine($"Autor: {listaLivros[i].autor}");
                Console.WriteLine($"Ano: {listaLivros[i].ano}");
                Console.WriteLine($"Prateleira: {listaLivros[i].prateleira}\n");
                encontrado = true;
            }
        }
        return encontrado;
    }

    static void salvarDados(List<Livro> listaLivros, string nomeArquivo)
    {
        using StreamWriter writer = new StreamWriter(nomeArquivo);
        foreach (Livro livro in listaLivros)
        {
            writer.WriteLine($"{livro.titulo};{livro.autor};{livro.ano};{livro.prateleira}");
        }
        Console.WriteLine("Dados salvos com sucesso!");
    }

    static void carregarDados(List<Livro> listaLivros, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhasDoArquivo)
            {
                if (string.IsNullOrWhiteSpace(linha)) continue;

                string[] campos = linha.Split(';');
                Livro livro = new Livro();
                livro.titulo = campos[0];
                livro.autor = campos[1];
                livro.ano = int.Parse(campos[2]);
                livro.prateleira = campos[3];
                listaLivros.Add(livro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }

    static int menu()
    {
        Console.WriteLine("=== SISTEMA DE BIBLIOTECA ===");
        Console.WriteLine("1 - Cadastrar Livro");
        Console.WriteLine("2 - Buscar Livro por Título");
        Console.WriteLine("3 - Mostrar Todos os Livros");
        Console.WriteLine("4 - Buscar Livros Mais Novos que um Ano");
        Console.WriteLine("5 - Salvar Dados em Arquivo");
        Console.WriteLine("6 - Carregar Dados do Arquivo");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
        return int.Parse(Console.ReadLine() ?? "0");
    }

    static void Main()
    {
        List<Livro> listaLivros = new List<Livro>();
        carregarDados(listaLivros, "livros.txt");

        int opcao;
        do
        {
            opcao = menu();
            switch (opcao)
            {
                case 1:
                    addLivro(listaLivros);
                    break;
                case 2:
                    Console.Write("Digite o título do livro: ");
                    string tituloBusca = Console.ReadLine() ?? "";
                    if (!buscarPorTitulo(listaLivros, tituloBusca))
                        Console.WriteLine("Livro não encontrado.");
                    break;
                case 3:
                    mostrarLivros(listaLivros);
                    break;
                case 4:
                    Console.Write("Digite o ano de referência: ");
                    int anoRef = int.Parse(Console.ReadLine() ?? "0");
                    if (!buscarMaisNovosQueAno(listaLivros, anoRef))
                        Console.WriteLine($"Nenhum livro encontrado mais novo que {anoRef}.");
                    break;
                case 5:
                    salvarDados(listaLivros, "livros.txt");
                    break;
                case 6:
                    listaLivros.Clear();
                    carregarDados(listaLivros, "livros.txt");
                    break;
                case 0:
                    Console.WriteLine("Saindo e salvando...");
                    salvarDados(listaLivros, "livros.txt");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        } while (opcao != 0);
    }
}
