namespace Yurumi.Connections
{
    /// <summary>
    /// Connection interface.
    /// </summary>
    public interface IConnection<T> where T:class
    {
        T Client { get; }
    }
}
