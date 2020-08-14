using System;
using System.Collections.Generic;
using System.Text;

namespace Elastic.Models.Foods
{
    public class Food: BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
