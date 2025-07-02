namespace Sandbox.SQLite
{
    using System.Collections.Generic;

    /// <summary>
    /// The class representing a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public IEnumerable<string> Favorites { get; set; }
    }
}
