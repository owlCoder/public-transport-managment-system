using Client.ViewModel;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Commands.Manager
{
    public class CommandManager
    {
        private static readonly List<Command> commands = new List<Command>();
        private static readonly Stack<Command> undoStack = new Stack<Command>();
        private static readonly Stack<Command> redoStack = new Stack<Command>();

        public void AddAndExecuteCommand(Command command)
        {
            try
            {
                commands.Add(command);
                command.Execute();
                undoStack.Push(command);
                redoStack.Clear(); // Clear redo stack when a new command is executed
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Error executing command: {ex.Message}");
            }
        }

        public void Undo()
        {
            if (undoStack.Any())
            {
                Command command = undoStack.Pop();
                try
                {
                    command.Undo();
                    redoStack.Push(command);
                }
                catch (Exception ex)
                {
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Error undoing command: {ex.Message}");
                }
            }
        }

        public void Redo()
        {
            if (redoStack.Any())
            {
                Command command = redoStack.Pop();
                try
                {
                    command.Redo();
                    undoStack.Push(command);
                }
                catch (Exception ex)
                {
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Error redoing command: {ex.Message}");
                }
            }
        }
    }
}
