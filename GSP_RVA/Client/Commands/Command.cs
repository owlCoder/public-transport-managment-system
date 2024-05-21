﻿namespace Client.Commands
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void Undo();
        public abstract void Redo();
    }
}
