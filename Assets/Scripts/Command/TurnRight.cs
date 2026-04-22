using Chapter.Observer;
using Chapter.State;
namespace Chapter.Command
{
    public class TurnRight : Command
    {
        private BikeController _controller;

        public TurnRight(BikeController controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.Turn(Direction.Right);
        }
    }
}