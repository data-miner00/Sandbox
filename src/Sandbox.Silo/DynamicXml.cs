namespace Sandbox.Silo
{
    using System.Collections;
    using System.Dynamic;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;

    public class DynamicXml : DynamicObject, IEnumerable
    {
        public static implicit operator string(DynamicXml xml) => xml.xml.Value;

        private dynamic xml;

        public DynamicXml(string fileName)
        {
            this.xml = XDocument.Load(fileName);
        }

        public DynamicXml(dynamic xml)
        {
            this.xml = xml;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in this.xml.Elements())
            {
                yield return new DynamicXml(item);
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            var xml = this.xml.Element(binder.Name);

            if (xml == null)
            {
                result = new DynamicXml(xml);
                return true;
            }

            result = null;
            return false;
        }
    }
}
