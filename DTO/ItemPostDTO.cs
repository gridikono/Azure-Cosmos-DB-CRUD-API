using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.DTO
{
    public partial class ItemPostDTO
    {
       
            public string Name { get; set; }

            public string Description { get; set; }

            public bool Completed { get; set; }

            public String CraetedBy { get; set; }

            public String EditedBy { get; set; }
        
    }
}
