using Cadastro_de_Eletro;
using System;
using System.Collections.Generic;
using System.IO;

class CadastroEletro {
    static void addEletrodomestico(List<Eletro> listaEletros)
    {

        Eletro novoEletro = new Eletro();
        Console.WriteLine("*** Dados para o novo Eletro ***");
        Console.WriteLine("Nome: ");
        novoEletro.nome = Console.ReadLine();
        Console.WriteLine("Potencia (kw): ");
        novoEletro.potencia = double.Parse(Console.ReadLine());
        Console.WriteLine("Tempo de uso ( média diária): ");
        novoEletro.tempo = double.Parse(Console.ReadLine());
        listaEletros.Add(novoEletro);

    }
    static void mostrarEletrodomesticos(List<Eletro> listaEletros)
    {
        Console.WriteLine("Eletros Cadastrados");
        for (int i = 0; i < listaEletros.Count; i++)
        {
            Console.WriteLine("Eletro " + (i + 1));
            Console.WriteLine($"Nome: {listaEletros[i].nome}");
            Console.WriteLine($"Potencia: {listaEletros[i].potencia}");
            Console.WriteLine($"Tempo: {listaEletros[i].tempo}");
        }
    }
    static bool buscarEletrodomestico(List<Eletro> listaEletros, string nomeBusca)
    {
        bool encontrado = false;
        for (int i = 0; i < listaEletros.Count; i++)
        {
            if (listaEletros[i].nome.ToUpper().Contains(nomeBusca.ToUpper()))
            {
                Console.WriteLine("*** ***");
                Console.WriteLine($"Nome: {listaEletros[i].nome}");
                Console.WriteLine($"Potencia: {listaEletros[i].potencia}");
                Console.WriteLine($"Tempo: {listaEletros[i].tempo}");
                encontrado = true;
            }
        }
        return encontrado;
    }
  
    static bool buscarPorConsumo(List<Eletro> listaEletros, double valorX)
    {
        bool encontrado = false;
        for (int i = 0; i < listaEletros.Count; i++)
        {
            double consumo = listaEletros[i].potencia * listaEletros[i].tempo;
            if (consumo > valorX)
            {
                Console.WriteLine("*** ***");
                Console.WriteLine($"Nome: {listaEletros[i].nome}");
                Console.WriteLine($"Potencia: {listaEletros[i].potencia}");
                Console.WriteLine($"Tempo: {listaEletros[i].tempo}");
                Console.WriteLine($"Consumo: {consumo}");
                encontrado = true;
            }
        }
        return encontrado;
    }

    static void calcularConsumoCasa(List<Eletro> listaEletros)
    {
        Console.Write("Valor do Kw em R$: ");
        double valorKw = double.Parse(Console.ReadLine());
        double totalDia = 0, totalMes = 0;
        foreach (Eletro e in listaEletros)
        {
            totalDia += e.potencia * e.tempo;
        }
        Console.WriteLine($"Total de Kw consumidos por dia: {totalDia:f2}");
        Console.WriteLine($" Gastos por dia: {totalDia*valorKw:f2}");     
        totalMes = totalDia * 30;
        Console.WriteLine($"Total de Kw consumidos por mes: {totalMes:f2}");
        Console.WriteLine($" Gastos por mes: {totalMes * valorKw:f2}");
    }



    static int menu()
    {
        Console.WriteLine("=== CONTROLE DE CONSUMO DE ENERGIA RESIDENCIAL ===");
        Console.WriteLine("1 - Cadastrar Eletrodoméstico");
        Console.WriteLine("2 - Mostrar Eletrodomésticos");
        Console.WriteLine("3 - Buscar Eletrodoméstico pelo Nome");
        Console.WriteLine("4 - Buscar Eletrodomésticos que Gastam mais que X(kWh / dia)");
        Console.WriteLine("5 - Calcular Consumo Diário e Mensal da Casa (kW e R$)");
        Console.WriteLine("6 - Salvar Dados em Arquivo");
        Console.WriteLine("7 - Carregar Dados do Arquivo");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
        return int.Parse(Console.ReadLine());
    }
    static void salvarDados(List<Eletro> listaEletros, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);
        foreach (Eletro eletro in listaEletros)
        {

            writer.WriteLine($"{eletro.nome},{eletro.potencia},{eletro.tempo}");
        }
        Console.WriteLine("Dados salvos com sucesso!");
        writer.Dispose();
    }
    static void carregarDados(List<Eletro> listaEletros, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhasDoArquivo)
            {
                string[] campos = linha.Split(',');
                Eletro eletro = new Eletro();
                eletro.nome = campos[0];
                eletro.potencia = double.Parse(campos[1]);
                eletro.tempo = double.Parse(campos[2]);
                listaEletros.Add(eletro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }

    static void Main()
    {
        List<Eletro> listaEletros = new List<Eletro>();
        carregarDados(listaEletros, "dadosEletros.txt");
        int opcao;
        do
        {
            opcao = menu();
            switch (opcao)
            {
                case 1:
                    addEletrodomestico(listaEletros);
                    break;
                case 2:
                    mostrarEletrodomesticos(listaEletros);
                    break;
                case 3:
                    Console.WriteLine("Nome do Eletro: ");
                    string nomeDeBusca = Console.ReadLine();
                    bool retorno = buscarEletrodomestico(listaEletros, nomeDeBusca);
                    if (retorno == false)
                        Console.WriteLine("Eletro não encontrado");
                    break;
                case 4:
                    Console.Write("Digite o valor mínimo de consumo (kWh/dia): ");
                    double valorX = double.Parse(Console.ReadLine());
                    bool encontrado = buscarPorConsumo(listaEletros, valorX);
                    if (!encontrado)
                        Console.WriteLine("Nenhum eletrodoméstico encontrado com o consumo especificado.");
                    break;
                case 5:
                    calcularConsumoCasa(listaEletros); 
                    break;
                case 6:
                    salvarDados(listaEletros, "dadosEletros.txt");
                    break;

                case 7:
                    listaEletros.Clear();
                    carregarDados(listaEletros, "dadosEletros.txt");
                    break;
                case 0:
                    Console.WriteLine("Saindo e salvando...");
                    salvarDados(listaEletros, "dadosEletros.txt");
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (opcao != 0);
    }
}



            