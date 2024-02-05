namespace Console.UI;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using ProcessadorTarefas.Entidades;
using ProcessadorTarefas.Repositorios;
using ProcessadorTarefas.Servicos;

internal class Program : IGerenciadorTarefas, IProcessadorTarefas
{
    Repository repository = new Repository();

    static void Main(string[] args)
    {
        Repository repository = new Repository();
        int opcao = 0;
        int subMenu = 0;
        foreach (var tarefa in repository.GetAll().ToList())
        {
            Console.WriteLine("Tarefa ID: " + tarefa.Id + "Estado: " + tarefa.Estado);
        }
        do 
        {
            Console.WriteLine("Bem-vindo ao gerenciador de tarefas");
            Console.WriteLine();
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine();
            Console.WriteLine("1 - Criar tarefa");
            Console.WriteLine("2 - Tarefas ativas");
            Console.WriteLine("3 - Tarefas inativas");
            Console.WriteLine("4 - Iniciar");
            opcao = int.TryParse(Console.ReadLine(), out opcao) ? opcao : 0;
            switch (opcao)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("Criar tarefa");
                Console.WriteLine();
                Tarefa tarefa = new Tarefa();
                repository.Add(tarefa);
                Console.WriteLine("Tarefa criada com sucesso.");
                Console.ReadLine();
                Console.Clear();
                break;
            case 2:
                subMenu = MenuTarefasAtivas();
                switch (subMenu)
                {
                    case 1:
                        Console.WriteLine("Listar tarefas ativas");
                        Console.WriteLine();
                        repository
                            .GetAll()
                            .Where(x => x.Estado == EstadoTarefa.EmExecucao || x.Estado == EstadoTarefa.EmPausa || x.Estado == EstadoTarefa.Criada || x.Estado == EstadoTarefa.Agendada)
                            .ToList()
                            .ForEach(x => Console.WriteLine("Tarefa ID: " + x.Id + "Estado: " + x.Estado));
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Cancelar tarefa");
                        Console.WriteLine();
                        Console.WriteLine("Informe o id da tarefa que deseja cancelar");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            Tarefa t = repository.GetById(id);
                            if (t != null)
                            {
                                t.Estado = EstadoTarefa.Cancelada;
                                repository.Update(t);
                                Console.WriteLine("Tarefa cancelada com sucesso.");
                            }
                            else
                                Console.WriteLine("Tarefa não encontrada.");
                        }
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
                break;
            case 3:
                subMenu = MenuTarefasInativas();
                switch (subMenu)
                {
                    case 1:
                        Console.WriteLine("Listar tarefas concluídas");
                        Console.WriteLine();
                        repository
                            .GetAll()
                            .Where(x => x.Estado == EstadoTarefa.Concluida)
                            .ToList()
                            .ForEach(x => Console.WriteLine("Tarefa ID: " + x.Id + "Estado: " + x.Estado));
                        break;
                    case 2:
                        Console.WriteLine("Listar tarefas canceladas");
                        Console.WriteLine();
                        repository
                            .GetAll()
                            .Where(x => x.Estado == EstadoTarefa.Cancelada)
                            .ToList()
                            .ForEach(x => Console.WriteLine("Tarefa ID: " + x.Id + "Estado: " + x.Estado));
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
                break;
            default:
                Console.WriteLine("Opção inválida");
                break;
        }
        }while (opcao != 0);
    }
    public static int MenuTarefasAtivas()
    {
        Console.Clear();
        Console.WriteLine("1 - Listar tarefas ativas");
        Console.WriteLine("2 - Cancelar tarefa");
        Console.WriteLine("0 - Voltar ao menu principal");
        return int.TryParse(Console.ReadLine(), out int opcao) ? opcao : 0;
    }
    public static int MenuTarefasInativas()
    {
        Console.Clear();
        Console.WriteLine("1 - Listar tarefas concluídas");
        Console.WriteLine("2 - Listar tarefas canceladas");
        Console.WriteLine("0 - Voltar ao menu principal");
        return int.TryParse(Console.ReadLine(), out int opcao) ? opcao : 0;
    }

    public Task<Tarefa> Criar()
    {
        return Task.Run(async () =>
        {
            var novaTarefa = repository.GetAll().FirstOrDefault();
            await Task.Run(async () =>
            {
                foreach (var subtarefa in novaTarefa.SubtarefasPendentes)
                {
                    await Executar(subtarefa);
                    novaTarefa.SubtarefasExecutadas.Append(subtarefa);
                }

                novaTarefa.Estado = EstadoTarefa.Concluida;
            });
            novaTarefa.Estado = EstadoTarefa.Agendada;
            return novaTarefa;
        });
    }

    private async Task Executar(Subtarefa subtarefa)
    {
        await Task.Delay(subtarefa.Duracao);
    }

    public Task<Tarefa> Consultar(int idTarefa)
    {
        throw new NotImplementedException();
    }

    public Task Cancelar(int idTarefa)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Tarefa>> ListarAtivas()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Tarefa>> ListarInativas()
    {
        throw new NotImplementedException();
    }

    public Task Iniciar()
    {
        return Task.Run(() =>
        {
        });
    }

    public Task CancelarTarefa(int idTarefa)
    {
        throw new NotImplementedException();
    }

    public Task Encerrar()
    {
        throw new NotImplementedException();
    }
}


