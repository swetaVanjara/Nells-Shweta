using CommunityToolkit.Maui.Views;
using NellsPay.Send.Models.LoginModels;
using NellsPay.Send.Views.LoginPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.Navigation
{
    public interface IInitializeWithParameter
    {
        void Initialize(object parameter);
    }
}