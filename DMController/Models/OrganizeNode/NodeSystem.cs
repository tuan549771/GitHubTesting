using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.Models.OrganizeNode
{
    public enum eTypeNode { IsFolder = 0, IsFileExtSrt }

    class NodeSystem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public eTypeNode TypeNode { get; set; }
    }
}
