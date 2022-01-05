using Presentation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Actions.ActionHelpers
{
    public class Template
    {
        public InputStatus Status{ get; set; }
        public string Name{ get; set; }
        public Action Function { get; set; }
    }
}
