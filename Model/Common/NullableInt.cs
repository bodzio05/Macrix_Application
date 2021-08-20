using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Macrix_Application.Model.Common
{
    public struct NullableInt : IXmlSerializable
    {
        private int value;
        private bool hasValue;

        private NullableInt(int value)
        {
            hasValue = true;
            this.value = value;
        }

        public bool HasValue
        {
            get { return hasValue; }
        }

        public int Value
        {
            get { return value; }
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.GetAttribute("nil") == "true")
            {
                ReadNullValue();
                return;
            }
            ReadNonNullValue(reader);
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            throw new NotSupportedException();
        }

        private void ReadNullValue()
        {
            hasValue = false;
        }

        private void ReadNonNullValue(XmlReader reader)
        {
            reader.ReadStartElement();
            var s = reader.ReadString();
            value = Convert.ToInt32(s);
            reader.ReadEndElement();
            hasValue = true;
        }

        public static implicit operator NullableInt(int value)
        {
            return new NullableInt(value);
        }
    }
}
