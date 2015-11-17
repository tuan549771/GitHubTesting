using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.Models.QueueTesting
{
    class Queue
    {
        private static readonly List<ItemScript> _queueScripts = new List<ItemScript>();
        public IEnumerable<ItemScript> QueueScripts
        {
            get { return new List<ItemScript>(_queueScripts); }
        }

        public Queue(IEnumerable<ItemScript> queueScripts)
        {
            _queueScripts.Clear();
            _queueScripts.AddRange(queueScripts);
        }

        public static List<ItemScript> GetListItemScript()
        {
            List<ItemScript> itemScripts = new List<ItemScript>();
            foreach (ItemScript script in _queueScripts)
            {
                itemScripts.Add(script);
            }
            return itemScripts;
        }
    }
}
