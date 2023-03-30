using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCommon.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class JResultIndex : Attribute
    {
        public int Index { get; set; }
        public JResultIndex(int index)
        {
            this.Index = index;
        }

        public override string ToString()
        {
            return "JResultIndex = " + this.Index;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NCacheKeyField : Attribute
    {
        public string Prefix { get; set; }
        public int Index { get; set; }
        public NCacheKeyField(string Prefix = null)
        {
            this.Prefix = Prefix;
        }

        public override string ToString()
        {
            return "NCacheKeyField = " + this.Prefix;
        }
    }
}