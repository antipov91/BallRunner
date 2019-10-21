namespace BallRunner.Views
{
    public interface IEntityLinker<T>
    {
        void Link(Contexts contexts, T entity);
        void Unlink(Contexts contexts, T entity);
    }
}