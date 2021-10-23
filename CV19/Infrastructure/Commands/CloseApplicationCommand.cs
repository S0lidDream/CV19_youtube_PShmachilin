using CV19.Infrastructure.Commands.BaseCommand;
using System.Windows;

namespace CV19.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
        
    }
}
