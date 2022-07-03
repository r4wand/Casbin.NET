using System.Threading.Tasks;
using Casbin.Caching;
using Casbin.Effect;
using Casbin.Evaluation;
using Casbin.Model;
using Casbin.Persist;
using Casbin.Rbac;
#if !NET452
using Microsoft.Extensions.Logging;
#endif
namespace Casbin
{
    /// <summary>
    /// IEnforcer is the API interface of Enforcer
    /// </summary>
    public interface IEnforcer
    {
        #region Options
        public bool Enabled { get; set; }
        public bool EnabledCache { get; set; }
        public bool AutoSave { get; set; }
        public bool AutoBuildRoleLinks { get; set; }
        public bool AutoNotifyWatcher { get; set; }
        public bool AutoCleanEnforceCache { get; set; }
        #endregion

        #region Extensions
        public IEffector Effector { get; set; }
        public IModel Model { get; set; }
        public IPolicyManager PolicyManager { get; set; }
        public IReadOnlyAdapter Adapter { get; set; }
        public IWatcher Watcher { get; set; }
        public IRoleManager RoleManager { get; set; }
        public IEnforceCache EnforceCache { get; set; }
#if !NET452
        public ILogger Logger { get; set; }
#endif
        #endregion

        public bool IsSynchronized { get; }
        public string ModelPath { get; }
        public bool IsFiltered { get; }

        /// <summary>
        /// Decides whether a "subject" can access a "object" with the operation
        /// "action", input parameters are usually: (sub, obj, act).
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestValues">The request needs to be mediated, usually an array of strings,
        /// can be class instances if ABAC is used.</param>
        /// <returns>Whether to allow the request.</returns>
        public bool Enforce<TRequest>(EnforceContext context, TRequest requestValues) where TRequest : IRequestValues;

        /// <summary>
        /// Decides whether a "subject" can access a "object" with the operation
        /// "action", input parameters are usually: (sub, obj, act).
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestValues">The request needs to be mediated, usually an array of strings,
        /// can be class instances if ABAC is used.</param>
        /// <returns>Whether to allow the request.</returns>
        public Task<bool> EnforceAsync<TRequest>(EnforceContext context, TRequest requestValues) where TRequest : IRequestValues;
    }
}