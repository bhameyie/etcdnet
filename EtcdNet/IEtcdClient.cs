using System.Threading.Tasks;

namespace EtcdNet
{
    public interface IEtcdClient
    {
        /// <summary>
        /// X-Etcd-Cluster-Id
        /// </summary>
        string ClusterID { get; }

        /// <summary>
        /// Lastest X-Etcd-Index received by this instance
        /// </summary>
        long LastIndex { get; }

        /// <summary>
        /// Get etcd node specified by `key`
        /// </summary>
        /// <param name="key">The path of the node, must start with `/`</param>
        /// <param name="recursive">Represents whether list the children nodes</param>
        /// <param name="sorted">To enumerate the in-order keys as a sorted list, use the "sorted" parameter.</param>
        /// <param name="ignoreKeyNotFoundException">If `true`, `EtcdCommonException.KeyNotFound` exception is ignored and `null` is returned instead.</param>
        /// <returns>represents response; or `null` if not exist</returns>
        Task<EtcdResponse> GetNodeAsync(string key
            , bool ignoreKeyNotFoundException = false
            , bool recursive = false
            , bool sorted = false
            );

        /// <summary>
        /// Simplified version of `GetNodeAsync`.
        /// Get the value of the specific node
        /// </summary>
        /// <param name="key">The path of the node, must start with `/`</param>
        /// <param name="ignoreKeyNotFoundException">If `true`, `EtcdCommonException.KeyNotFound` exception is ignored and `null` is returned instead.</param>
        /// <returns>A string represents a value. It could be `null`</returns>
        Task<string> GetNodeValueAsync(string key, bool ignoreKeyNotFoundException = false);

        /// <summary>
        /// Get etcd node specified by `key`
        /// </summary>
        /// <param name="key">path of the node</param>
        /// <param name="value">value to be set</param>
        /// <param name="ttl">time to live, in seconds</param>
        /// <param name="dir">indicates if this is a directory</param>
        /// <returns>SetNodeResponse</returns>
        Task<EtcdResponse> SetNodeAsync(string key, string value, int? ttl = null, bool? dir = null);

        /// <summary>
        /// delete specific node
        /// </summary>
        /// <param name="key">The path of the node, must start with `/`</param>
        /// <param name="dir">true to delete an empty directory</param>
        /// <param name="ignoreKeyNotFoundException">If `true`, `EtcdCommonException.KeyNotFound` exception is ignored and `null` is returned instead.</param>
        /// <returns>SetNodeResponse instance or `null`</returns>
        Task<EtcdResponse> DeleteNodeAsync(string key, bool ignoreKeyNotFoundException = false, bool? dir = null);

        /// <summary>
        /// Create in-order node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ttl"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        Task<EtcdResponse> CreateInOrderNodeAsync(string key, string value, int? ttl = null, bool? dir = null);

        /// <summary>
        /// Create a new node. If node exists, EtcdCommonException.NodeExist occurs
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ttl"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        Task<EtcdResponse> CreateNodeAsync(string key, string value, int? ttl = null, bool? dir = null);

        /// <summary>
        /// CAS(Compare and Swap) a node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prevValue"></param>
        /// <param name="value"></param>
        /// <param name="ttl"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        Task<EtcdResponse> CompareAndSwapNodeAsync(string key, string prevValue, string value, int? ttl = null, bool? dir = null);

        /// <summary>
        /// CAS(Compare and Swap) a node
        /// </summary>
        /// <param name="key">path of the node</param>
        /// <param name="prevIndex">previous index</param>
        /// <param name="value">value</param>
        /// <param name="ttl">time to live (in seconds)</param>
        /// <param name="dir">is directory</param>
        /// <returns></returns>
        Task<EtcdResponse> CompareAndSwapNodeAsync(string key, long prevIndex, string value, int? ttl = null, bool? dir = null);

        /// <summary>
        /// Compare and delete specific node
        /// </summary>
        /// <param name="key">Path of the node</param>
        /// <param name="prevValue">previous value</param>
        /// <returns>EtcdResponse</returns>
        Task<EtcdResponse> CompareAndDeleteNodeAsync(string key, string prevValue);

        /// <summary>
        /// Compare and delete specific node
        /// </summary>
        /// <param name="key">path of the node</param>
        /// <param name="prevIndex">previous index</param>
        /// <returns>EtcdResponse</returns>
        Task<EtcdResponse> CompareAndDeleteNodeAsync(string key, long prevIndex);

        /// <summary>
        /// Watch changes
        /// </summary>
        /// <param name="key">Path of the node</param>
        /// <param name="recursive">true to monitor descendants</param>
        /// <param name="waitIndex">Etcd Index is continue monitor from</param>
        /// <returns>EtcdResponse</returns>
        Task<EtcdResponse> WatchNodeAsync(string key, bool recursive = false, long? waitIndex = null);
    }
}