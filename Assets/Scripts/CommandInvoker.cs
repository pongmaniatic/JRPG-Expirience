using System.Collections.Generic;

public class CommandInvoker
{
    static Queue<Command> commands = new Queue<Command>();
    public static void AddCommand(Command command)
    {
        commands.Enqueue(command);
    }
    public static void ExecuteCommands()
    {
        foreach (Command c in commands){c.Execute();}
        commands.Clear();
    }
}