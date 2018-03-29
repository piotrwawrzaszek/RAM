using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.ViewModel.Base;

namespace RAM.Infrastructure.ViewModel
{
    public interface IOptionsViewModel : IViewModel
    {

    }

    public class OptionsViewModel : BaseViewModel, IOptionsViewModel
    {

    }
}
