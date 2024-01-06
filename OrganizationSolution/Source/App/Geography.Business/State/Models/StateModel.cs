using Framework.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography.Business.State.Models
{
    public class StateModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
    }
}
