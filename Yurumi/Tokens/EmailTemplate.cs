using System.Collections.Generic;

namespace Yurumi.Tokens
{
    /// <summary>
    /// Email template.
    /// </summary>
    class EmailTemplate
    {
        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>The images.</value>
        internal IEnumerable<EmailImage> Images { get; set; }

        /// <summary>
        /// The content.
        /// </summary>
        internal readonly string Content;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Yurumi.Tokens.EmailTemplate"/> class.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <param name="images">Images.</param>
        internal EmailTemplate(string content, IEnumerable<EmailImage> images)
        {
            Content = content;
            Images = images;
        }
    }
}
