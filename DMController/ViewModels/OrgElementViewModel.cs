using DMController.Common;
using DMController.Models;
using DMController.Models.OrganizeNode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.ViewModels
{
    class OrgElementViewModel : ViewModelBase
    {
        private int _id;
        private string _name;
        private bool _isRoot = false;  //if the node is the root
        private eTypeNode _typeNode;
        private ObservableCollection<OrgElementViewModel> _children;

        public bool IsRoot
        {
            get { return _isRoot; }
            set { _isRoot = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public eTypeNode TypeNode
        {
            get { return _typeNode; }
            set { _typeNode = value; }
        } 

        public ObservableCollection<OrgElementViewModel> Children
        {
            get
            {
                if (_children == null) //not yet initialized
                    return GetChildren();
                return _children;
            }
            set
            {
                _children = value;
                NotifyPropertyChanged("Children");
            }
        }

        internal OrgElementViewModel(NodeSystem iNode)
        {
            this.ID = iNode.Id;
            this.Name = iNode.Name;
            this.TypeNode = iNode.TypeNode;
        }

        private ObservableCollection<OrgElementViewModel> GetChildren()
        {
            _children = new ObservableCollection<OrgElementViewModel>();
            //get the list of children from Model
            foreach (NodeSystem node in OrganizeNodeSystem.Instance().GetChildren(this.ID))
            {
                _children.Add(new OrgElementViewModel(node));
            }
            return _children;
        }
    }
}
