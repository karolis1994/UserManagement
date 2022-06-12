using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Domain
{
    /// <summary>
    /// Base of an entity
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public virtual long Id { get; set; }
    }
}
