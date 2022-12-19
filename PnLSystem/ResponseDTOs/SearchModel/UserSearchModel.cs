using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnLSystem.ResponseDTOs.SearchModel
{

    public class UserSearchModel
    {
        public string SearchTerm { get; set; }
        public bool? isActive { get; set; }
    }
}
