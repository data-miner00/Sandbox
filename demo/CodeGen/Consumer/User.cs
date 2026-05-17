using Generaton;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Consumer
{
    [Serialize]
    internal partial class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
