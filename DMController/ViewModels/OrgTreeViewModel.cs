using DMController.Models;
using DMController.Models.OrganizeNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.ViewModels
{
    class OrgTreeViewModel
    {
        private static OrgTreeViewModel self;

        private List<OrgElementViewModel> root;

        //the root of the visual tree
        public List<OrgElementViewModel> Root
        {
            get
            {
                if (root == null)
                {
                    root = new List<OrgElementViewModel>();
                    OrgElementViewModel vmRoot = new OrgElementViewModel(OrganizeNodeSystem.Instance().GetRoot());
                    vmRoot.IsRoot = true;
                    root.Add(vmRoot);
                }
                return root;
            }
        }

        private OrgTreeViewModel() { }

        public static OrgTreeViewModel Instance()
        {
            if (self == null)
                self = new OrgTreeViewModel();
            return self;
        }
    }
}
