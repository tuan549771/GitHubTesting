using ErrorHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.Models.OrganizeNode
{
    class OrganizeNodeSystem
    {
        private const string FOLDER_NAME_CONTAIN_SCRIPTS = "Scripts";
        private const string SLASH_STRING = "\\";
        private const char SLASH_CHAR = '\\';
        private const string EXTENSION_OF_SCRIPT_FILE = ".srt";
        private int IdNodeAssign = 1;
        private static OrganizeNodeSystem self;
        private static Dictionary<int, NodeSystem> list = new Dictionary<int, NodeSystem>();

        /// <summary>
        /// Constructor add first node with node name is FOLDER_NAME_CONTAIN_SCRIPTS
        /// Then create a list node via method OrganizeNodeSystemToDictionary
        /// </summary>
        private OrganizeNodeSystem()
        {
            // Add all nodes system to list
            list.Add(IdNodeAssign, new NodeSystem { Id = IdNodeAssign, Name = FOLDER_NAME_CONTAIN_SCRIPTS, ParentId = 1 });
            OrganizeNodeSystemToDictionary(IdNodeAssign);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdNode"></param>
        private void OrganizeNodeSystemToDictionary(int IdNode)
        {
            string pathScripts = FOLDER_NAME_CONTAIN_SCRIPTS + SLASH_STRING + GetRalativePath(IdNode);
            try
            {
                if (pathScripts[FOLDER_NAME_CONTAIN_SCRIPTS.Length - 1] == SLASH_CHAR)
                    FOLDER_NAME_CONTAIN_SCRIPTS.Remove(FOLDER_NAME_CONTAIN_SCRIPTS.Length - 1);
                string[] pathFolders = Directory.GetDirectories(pathScripts);
                string[] pathiles = Directory.GetFiles(pathScripts);

                List<int> IdFolders = new List<int>();

                foreach (string folder in pathFolders)
                {
                    string friendlyName = Path.GetFileName(folder);
                    ++IdNodeAssign;
                    list.Add(IdNodeAssign, new NodeSystem { Id = IdNodeAssign, Name = friendlyName, ParentId = IdNode, TypeNode = eTypeNode.IsFolder });
                    IdFolders.Add(IdNodeAssign);
                }
                foreach (string pathile in pathiles)
                {
                    string friendlyName = Path.GetFileName(pathile);
                    if (Path.GetExtension(pathile) == EXTENSION_OF_SCRIPT_FILE)
                    {
                        ++IdNodeAssign;
                        list.Add(IdNodeAssign, new NodeSystem { Id = IdNodeAssign, Name = friendlyName, ParentId = IdNode, TypeNode = eTypeNode.IsFileExtSrt });
                    }
                }
                foreach (int i in IdFolders)
                {
                    OrganizeNodeSystemToDictionary(i);
                }
            }
            catch (Exception ex)
            {
                string logger = LogTextInformation.CreateErrorMessage(ex);
                LogTextInformation.LogFileWrite(logger);
            }
        }

        /// <summary>
        /// Instance of OrganizeNodeSystem
        /// </summary>
        /// <returns></returns>
        internal static OrganizeNodeSystem Instance()
        {
            if (self == null)
                self = new OrganizeNodeSystem();
            return self;
        }

        /// <summary>
        /// Get the node root
        /// This will be return the top root node
        /// </summary>
        /// <returns></returns>
        internal NodeSystem GetRoot()
        {
            return list[1];
        }

        /// <summary>
        /// Return the children of a node via Id of parent node
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        internal IEnumerable<NodeSystem> GetChildren(int parentId)
        {
            return from a in list
                   where a.Value.ParentId == parentId
                        && a.Value.Id != parentId   // Don't include the root, which has the same Id and ParentId
                   select a.Value;
        }

        /// <summary>
        /// Return Ralative Path Of Node via Id of Node
        /// </summary>
        /// <param name="NodeId"></param>
        /// <returns></returns>
        internal static string GetRalativePath(int NodeId)
        {
            if (1 == NodeId)
                return string.Empty;
            string path = list[NodeId].Name;
            while (1 != list[NodeId].ParentId)
            {
                NodeId = list[NodeId].ParentId;
                path = list[NodeId].Name + "\\" + path;
            }
            return path;
        }

        /// <summary>
        /// Return Name Of Folder Contain Scripts
        /// </summary>
        /// <returns></returns>
        internal static string GetFolderContainScripts()
        {
            return FOLDER_NAME_CONTAIN_SCRIPTS;
        }
    }
}
