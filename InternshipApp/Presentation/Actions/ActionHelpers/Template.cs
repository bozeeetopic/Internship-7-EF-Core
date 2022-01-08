using Presentation.Enums;
using System;

namespace Presentation.Actions.ActionHelpers
{
    public class Template
    {
        public InputStatus Status{ get; set; }
        public string Name{ get; set; }
        public Action Function { get; set; }
    }
}
