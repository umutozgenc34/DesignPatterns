using Microsoft.AspNetCore.Mvc;

namespace DesignPatterns.Command.Commands;

public interface ITableActionCommand
{
    IActionResult Execute();
}
