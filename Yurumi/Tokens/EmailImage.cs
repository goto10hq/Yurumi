namespace Yurumi.Tokens
{
    /// <summary>
    /// Email image.
    /// </summary>
    class EmailImage
    {
        /// <summary>
        /// The original name with full path.
        /// </summary>
        internal readonly string OriginalNameWithFullPath;

        /// <summary>
        /// The original name with path.
        /// </summary>
        internal readonly string OriginalNameWithPath;

        /// <summary>
        /// The name of the original.
        /// </summary>
        internal readonly string OriginalName;

        /// <summary>
        /// The image name in HTML.
        /// </summary>
        internal readonly string ImageNameInHtml;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Yurumi.Tokens.EmailImage"/> class.
        /// </summary>
        /// <param name="originalNameWithFullPath">Original name with full path.</param>
        /// <param name="originalNameWithPath">Original name with path.</param>
        /// <param name="originalName">Original name.</param>
        /// <param name="imageNameInHtml">Image name in html.</param>
        public EmailImage(string originalNameWithFullPath, string originalNameWithPath, string originalName, string imageNameInHtml)
        {
            OriginalNameWithFullPath = originalNameWithFullPath;
            OriginalNameWithPath = originalNameWithPath;
            OriginalName = originalName;
            ImageNameInHtml = imageNameInHtml;
        }
    }
}
