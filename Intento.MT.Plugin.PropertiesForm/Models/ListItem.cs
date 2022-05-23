namespace Intento.MT.Plugin.PropertiesForm.Models
{
    /// <summary>
    /// Item of list
    /// </summary>
    internal class ListItem
    {
        /// <summary>
        /// Display name of item
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Value for item
        /// </summary>
        public string Value { get; set; }
        
        /// <inheritdoc/>
        public override string ToString()
        {
            return DisplayName ?? base.ToString();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not ListItem item)
            {
                return false;
            }

            return ReferenceEquals(this, obj) || Equals(item);
        }

        private bool Equals(ListItem other)
        {
            return Value == other.Value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}