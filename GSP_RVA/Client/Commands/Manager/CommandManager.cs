using System.Collections.Generic;
using System.Linq;

namespace Client.Commands.Manager
{
    public class CommandManager
    {
        private readonly List<Command> commands = new List<Command>();
        private readonly Stack<Command> undoStack = new Stack<Command>();
        private readonly Stack<Command> redoStack = new Stack<Command>();

        public void AddAndExecuteCommand(Command command)
        {
            commands.Add(command);
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear(); // Clear redo stack when a new command is executed
        }

        public void Undo()
        {
            if (undoStack.Any())
            {
                Command command = undoStack.Pop();
                command.Undo();
                redoStack.Push(command);
            }
        }

        public void Redo()
        {
            if (redoStack.Any())
            {
                Command command = redoStack.Pop();
                command.Redo();
                undoStack.Push(command);
            }
        }
    }
}
