using Chapter.Observer;
using Chapter.State;
namespace Chapter.Command
{
    public class TurnLeft : Command
    {
        private BikeController _controller;

        public TurnLeft(BikeController controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.Turn(Direction.Left);
        }
    }
}