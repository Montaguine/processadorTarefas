using ProcessadorTarefas.Entidades;
using SOLID_Example.Interfaces;

namespace ProcessadorTarefas.Repositorios
{
    internal class Repository : IRepository<Tarefa>
    {
        static IEnumerable<Tarefa> repository = new List<Tarefa>();
        public void Add(Tarefa entity)
        {
            repository.Append(entity);
        }
        public IEnumerable<Tarefa> GetAll()
        {
            return repository;
        }
        public Tarefa? GetById(int id)
        {
            return repository.FirstOrDefault(x => x.Id == id);
        }
        public void Update(Tarefa entity)
        {
            if (int.TryParse(Console.ReadLine(), out int estado))
            {
                entity.Estado = (EstadoTarefa)estado;
            }
        }

    }
}
