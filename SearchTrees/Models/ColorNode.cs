using System;
using SearchTrees.Models.Interfaces;

namespace SearchTrees.Models
{
    public class ColorNode<TKey, TValue> : NodeBase<ColorNode<TKey, TValue>, TKey, TValue> 
    {
        public bool Red { get; internal set; }
        public bool Black
        {
            get => !Red;
            internal set => Red = !value;
        }
    }
}
