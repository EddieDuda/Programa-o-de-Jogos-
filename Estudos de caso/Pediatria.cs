using System;
using System.Collections.Generic;

class Consultorio
{
    public string localizacao;
    private List<string> diasAtendimento;
    public Dictionary<DayOfWeek, List<TimeSpan>> horariosDeFuncionamento;

    public Consultorio(string loc, List<string> dias, Dictionary<DayOfWeek, List<TimeSpan>> horarios) 
    {
        localizacao = loc;
        diasAtendimento = dias;
        horariosDeFuncionamento = horarios;
    }
    
    public bool DiaDeAtendimento(DayOfWeek dia)
    {
        return diasAtendimento.Contains(dia.ToString());
    }


    public void ImprimirInformacoes()
    {
        Console.WriteLine($"Localização: {localizacao}");
        Console.WriteLine("Dias de atendimento:");
        foreach (string dia in diasAtendimento)
        {
            Console.WriteLine($"- {dia}");
        }

        Console.WriteLine("Horários de funcionamento:");
        foreach (var horario in horariosDeFuncionamento)
        {
            Console.Write($"- {horario.Key}: ");
            foreach (var intervalo in horario.Value)
            {
                Console.Write($" {intervalo.ToString("hh\\:mm")} -");
            }
            Console.WriteLine();
        }
    }
    
    public void ImprimirFichaCompleta(Paciente paciente)
    {
        Console.WriteLine("Ficha completa do paciente:");
        paciente.ImprimirInformacoes();
        Console.WriteLine();
        
        Console.WriteLine("Informações do consultório:");
        ImprimirInformacoes();
    }

}

    class Paciente
    {
    public string nome;
    public string endereco;
    public List<string> telefones;
    public DateTime dataNascimento;
    public DateTime dataPrimeiraConsulta;
    public string email;
    public bool conveniadoPlanoSaude;


    public Paciente(string n, string end, List<string> tels, DateTime nasc, DateTime primeiraConsulta, string mail, bool conveniado) 
    {
    nome = n;
    endereco = end;
    telefones = tels;
    dataNascimento = nasc;
    dataPrimeiraConsulta = primeiraConsulta;
    email = mail;
    conveniadoPlanoSaude = conveniado;
    }

    public static Paciente NovoPaciente()
    {
    Console.WriteLine("Por favor, informe os dados do paciente:");

    Console.Write("Nome: ");
    string nome = Console.ReadLine();

    Console.Write("Endereço: ");
    string endereco = Console.ReadLine();
    
    List<string> telefones = new List<string>();
    bool continuarAdicionandoTelefones = true;
    while (continuarAdicionandoTelefones)
    {
        Console.Write("Telefone: ");
        string telefone = Console.ReadLine();
        telefones.Add(telefone);

        Console.Write("Deseja adicionar outro telefone? (s/n) ");
        string resposta = Console.ReadLine().ToLower();
        continuarAdicionandoTelefones = (resposta == "s");
    }

    DateTime dataNascimento;
    bool dataNascimentoValida = false;
    do
    {
        Console.Write("Data de nascimento (dd/mm/aaaa): ");
        string dataNascimentoStr = Console.ReadLine();
        dataNascimentoValida = DateTime.TryParseExact(dataNascimentoStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataNascimento);
        if (!dataNascimentoValida)
        {
            Console.WriteLine("Data inválida. Digite a data novamente.");
        }
    } while (!dataNascimentoValida);

    Console.Write("Data da primeira consulta (dd/mm/aaaa): ");
    DateTime dataPrimeiraConsulta = DateTime.Parse(Console.ReadLine());

    Console.Write("E-mail: ");
    string email = Console.ReadLine();

    Console.Write("É conveniado de plano de saúde? (s/n) ");
    bool conveniadoPlanoSaude = (Console.ReadLine().ToLower() == "s");

    return new Paciente(nome, endereco, telefones, dataNascimento, dataPrimeiraConsulta, email, conveniadoPlanoSaude);
    }
    public void AgendarConsulta(Consultorio consultorio)
    {
    // Verifica se o consultório atende no mesmo dia que a primeira consulta
    if (!consultorio.DiaDeAtendimento(dataPrimeiraConsulta.DayOfWeek))
    {
        Console.WriteLine($"O consultório não atende no dia da sua primeira consulta ({dataPrimeiraConsulta.ToString("dddd")}).");
        return;
    }

    // Pergunta qual o dia desejado para a consulta
    Console.WriteLine($"Em que dia você deseja a consulta no consultório {consultorio.localizacao}?");
    DayOfWeek diaConsulta;
    while (true)
    {
        Console.Write("Digite o dia da semana: ");
        string dia = Console.ReadLine().ToLower();
        try
        {
            diaConsulta = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dia);
            break;
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Dia inválido. Por favor, tente novamente.");
        }
    }

    // Verifica se o consultório atende no dia escolhido
    if (!consultorio.DiaDeAtendimento(diaConsulta))
    {
        Console.WriteLine($"O consultório não atende no dia escolhido ({diaConsulta.ToString()}).");
        return;
    }

    // Pergunta qual o horário desejado para a consulta
    Console.WriteLine($"Em que horário você deseja a consulta no dia {diaConsulta.ToString()}?");
    List<TimeSpan> horariosDisponiveis = consultorio.horariosDeFuncionamento[diaConsulta];
    TimeSpan horarioConsulta;
    while (true)
    {
        Console.Write("Digite o horário (formato HH:MM): ");
        string horario = Console.ReadLine();
        try
        {
            horarioConsulta = TimeSpan.Parse(horario);
            if (horariosDisponiveis.Contains(horarioConsulta))
            {
                break;
            }
            else
            {
                Console.WriteLine("Horário inválido. Por favor, tente novamente.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Horário inválido. Por favor, tente novamente.");
        }
    }

    // Confirma a marcação da consulta
    Console.WriteLine($"Sua consulta no consultório {consultorio.localizacao} foi marcada para o dia {diaConsulta.ToString()} às {horarioConsulta.ToString("hh\\:mm")}.");
    }

public void ImprimirInformacoes()
{
    Console.WriteLine($"Nome: {nome}");
    Console.WriteLine($"Endereço: {endereco}");
    Console.WriteLine("Telefones:");
    foreach (string telefone in telefones)
    {
        Console.WriteLine($"- {telefone}");
    }
    Console.WriteLine($"Data de nascimento: {dataNascimento.ToString("dd/MM/yyyy")}");
    Console.WriteLine($"Data da primeira consulta: {dataPrimeiraConsulta.ToString("dd/MM/yyyy")}");
    Console.WriteLine($"E-mail: {email}");
    Console.WriteLine($"Conveniado de plano de saúde: {(conveniadoPlanoSaude ? "Sim" : "Não")}");
}
}

class MainClass
{
static void Main()
{
Console.WriteLine("Bem-vindo ao consultório da Dra. Janete !");
    
    Console.WriteLine("A Seguir temos os nossos dias de atendimento, os horarios disponiveis e a localização dos nossos escritórios.");
    
    Paciente paciente1 = Paciente.NovoPaciente();
    Consultorio consultorio1 = new Consultorio(
    "Rua do Consultório 1, 123 - Iguatemi",
    new List<string> { "Monday", "Friday" },
    new Dictionary<DayOfWeek, List<TimeSpan>> {
        { DayOfWeek.Monday, new List<TimeSpan> { new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0) } },
        { DayOfWeek.Friday, new List<TimeSpan> { new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0) } }
    }
    );

    Paciente paciente2 = Paciente.NovoPaciente();
    Consultorio consultorio2 = new Consultorio(
    "Rua do Consultório 2, 456 - Pituba",
    new List<string> { "Tuesday", "Wednesday" },
    new Dictionary<DayOfWeek, List<TimeSpan>> {
        { DayOfWeek.Tuesday, new List<TimeSpan> { new TimeSpan(10, 0, 0), new TimeSpan(18, 0, 0) } },
        { DayOfWeek.Wednesday, new List<TimeSpan> { new TimeSpan(10, 0, 0), new TimeSpan(18, 0, 0) } }
    }
    );

    Paciente paciente3 = Paciente.NovoPaciente();
    Consultorio consultorio3 = new Consultorio(
    "Rua do Consultório 3, 789 - Lauro de Freitas",
    new List<string> { "Thursday" },
    new Dictionary<DayOfWeek, List<TimeSpan>> {
        { DayOfWeek.Thursday, new List<TimeSpan> { new TimeSpan(10, 0, 0), new TimeSpan(18, 0, 0) } }
    }
    );

    
    consultorio1.ImprimirInformacoes();
    consultorio2.ImprimirInformacoes();
    consultorio3.ImprimirInformacoes();
    
    Paciente paciente = Paciente.NovoPaciente();
    paciente.ImprimirInformacoes();

 
}
}
