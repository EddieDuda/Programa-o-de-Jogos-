using System;

class Jogo
{
    public int[] numerosEscolhidos;
    public int[] numerosSorteados;
    public decimal premio;

    public Jogo(int[] numerosEscolhidos, decimal premio)
    {
        this.numerosEscolhidos = numerosEscolhidos;
        this.premio = premio;
    }

    public int NumerosAcertados()
    {
        int acertos = 0;
        foreach (int numero in numerosEscolhidos)
        {
            if (Array.IndexOf(numerosSorteados, numero) >= 0)
            {
                acertos++;
            }
        }
        return acertos;
    }

    public decimal CalcularPremio(int acertos)
    {
        if (acertos == numerosSorteados.Length)
        {
            return premio;
        }
        else
        {
            return 0;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Jogo quina = new Jogo(new int[5], 1000000);
        Jogo megasena = new Jogo(new int[6], 5000000);
        Jogo lotomania = new Jogo(new int[7], 100000);

        Console.WriteLine("Digite os números escolhidos para o jogo de Quina (5 números):");
        quina.numerosEscolhidos = LerNumerosEscolhidos(5);

        Console.WriteLine("Digite os números escolhidos para o jogo de Megasena (6 números):");
        megasena.numerosEscolhidos = LerNumerosEscolhidos(6);

        Console.WriteLine("Digite os números escolhidos para o jogo de Lotomania (6 números):");
        lotomania.numerosEscolhidos = LerNumerosEscolhidos(7);

        quina.numerosSorteados = SortearNumeros(5);    //Sabemso que Lotomania pode-se marcar 50 numeros,
        megasena.numerosSorteados = SortearNumeros(6); //Contudo diminui para 7, com o objetivo de simplificar.
        lotomania.numerosSorteados = SortearNumeros(7); 
        
        Console.WriteLine("Resultado do jogo de Quina: " + string.Join(", ", quina.numerosSorteados));
        Console.WriteLine("Acertos no jogo de Quina: " + quina.NumerosAcertados());
        Console.WriteLine("Prêmio do jogo de Quina: " + quina.CalcularPremio(quina.NumerosAcertados()).ToString("C"));

        Console.WriteLine("Resultado do jogo de Megasena: " + string.Join(", ", megasena.numerosSorteados));
        Console.WriteLine("Acertos no jogo de Megasena: " + megasena.NumerosAcertados());
        Console.WriteLine("Prêmio do jogo de Megasena: " + megasena.CalcularPremio(megasena.NumerosAcertados()).ToString("C"));

        Console.WriteLine("Resultado do jogo de Lotomania: " + string.Join(", ", lotomania.numerosSorteados));
        Console.WriteLine("Acertos no jogo de Lotomania: " + lotomania.NumerosAcertados());
        Console.WriteLine("Prêmio do jogo de Lotomania: " + lotomania.CalcularPremio(lotomania.NumerosAcertados()).ToString("C"));
    }

static int[] LerNumerosEscolhidos(int quantidadeNumeros)
{
    int[] numeros = new int[quantidadeNumeros];
    for (int i = 0; i < quantidadeNumeros; i++)
    {
        Console.Write($"Digite o {i + 1}º número: ");
        numeros[i] = int.Parse(Console.ReadLine());
    }
    return numeros;
}

static int[] SortearNumeros(int quantidadeNumeros)
{
    int[] numerosSorteados = new int[quantidadeNumeros];
    Random random = new Random();
    for (int i = 0; i < quantidadeNumeros; i++)
    {
        int numeroSorteado;
        
        if(quantidadeNumeros == 5 )
        {
            do
            {
                numeroSorteado = random.Next(1, 80);
            } while (Array.IndexOf(numerosSorteados, numeroSorteado) >= 0);
            numerosSorteados[i] = numeroSorteado;
        }
        else if(quantidadeNumeros == 6) 
        {
            do
            {
                numeroSorteado = random.Next(1, 60);
            } while (Array.IndexOf(numerosSorteados, numeroSorteado) >= 0);
            numerosSorteados[i] = numeroSorteado;
        }
        else if(quantidadeNumeros == 7)
        {
            do
            {
                numeroSorteado = random.Next(0, 99);
            } while (Array.IndexOf(numerosSorteados, numeroSorteado) >= 0);
            numerosSorteados[i] = numeroSorteado;
        }
    }
    Array.Sort(numerosSorteados);
    return numerosSorteados;
 }
}
