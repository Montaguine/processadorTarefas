using ProcessadorTarefas.Repositorios;
using static ProcessadorTarefas.Entidades.Tarefa;

namespace ProcessadorTarefas.Entidades
{
    public interface ITarefa
    {
        int Id { get; }
        EstadoTarefa Estado { get; }
        DateTime IniciadaEm { get; }
        DateTime EncerradaEm { get; }
        IEnumerable<Subtarefa> SubtarefasPendentes { get; }
        IEnumerable<Subtarefa> SubtarefasExecutadas { get; }
    }

    public class Tarefa : ITarefa
    {
        private static int quantidadeTarefas = 0;
        Random random = new Random();
        public Tarefa()
        {
            Id = ++quantidadeTarefas;
            Estado = EstadoTarefa.Criada;
            SubtarefasPendentes = new List<Subtarefa>();
            int quantidade = random.Next(10, 101);
            for (int i = 0; i < quantidade; i++)
            {
                SubtarefasPendentes.Append(new Subtarefa());
            }
            SubtarefasExecutadas = new List<Subtarefa>();
        }
        public int Id { get; set; }
        public EstadoTarefa Estado { get; set; }
        public DateTime IniciadaEm { get; set; }
        public DateTime EncerradaEm { get; set; }
        public IEnumerable<Subtarefa> SubtarefasPendentes { get; set; }
        public IEnumerable<Subtarefa> SubtarefasExecutadas { get; set; }
    }

}
