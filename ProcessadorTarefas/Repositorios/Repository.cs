using ProcessadorTarefas.Entidades;
using SOLID_Example.Interfaces;

namespace ProcessadorTarefas.Repositorios
{
    public class Repository : IRepository<Tarefa>
    {
        private IEnumerable<Tarefa> repository = new List<Tarefa>();
        public void Add(Tarefa entity)
        {
            repository = repository.Append(entity);
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
            if(entity.Estado == EstadoTarefa.Criada)
            {
                entity.Estado = EstadoTarefa.Agendada;
            }
        }
        private void FillRepository()
        {
            for (int i = 0; i < 100; i++)
            {
                Tarefa tarefa = new Tarefa();
                Add(tarefa);
            }
        }
        public Repository()
        {
            FillRepository();
        }
    }
}
