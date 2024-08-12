using System;
using System.Collections.Generic;
using System.IO;

namespace ApontamentoVs2
{
    public class Rule
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Field { get; set; }
        public string Operator { get; set; }
        public string CompareTo { get; set; }
        public int Duration { get; set; }
        public string ErrorMessage { get; set; }
    }


}
