namespace ProcessadorTarefas.Entidades
{
    public struct Subtarefa
    {
        Random random = new Random();
        public TimeSpan Duracao { get; set; }
        public Subtarefa()
        {
            Duracao = new TimeSpan(0, 0, random.Next(3, 61));
        }
    }

}
