using System;

namespace NodeBuilder.Views
{
    public class ValueDescription
    {
        public ValueDescription()
        {
        }

        public Enum Value { get; set; }
        public string Description { get; set; }

        public override string ToString() => Description;
        
    }
}