namespace BallRunner.Commands
{
    public class DelayCommand : Command
    {
        private readonly Contexts contexts;

        private float delay;

        public DelayCommand(Contexts contexts, float delay)
        {
            this.contexts = contexts;
            this.delay = delay;
        }
        
        public override void Execute()
        {
            delay -= contexts.time.deltaTime.value;
            
            if (delay < 0)
                IsComplete = true;
        }
    }
}