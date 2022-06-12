using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Models
{
    /// <summary>
    /// User's view
    /// </summary>
    public class UserView
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(120)]
        public string Email { get; set; }
    }
}
