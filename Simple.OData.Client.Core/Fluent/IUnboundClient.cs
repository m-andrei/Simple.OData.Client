﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Simple.OData.Client
{
    /// <summary>
    /// Provides access to unbound OData operations in a fluent style.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IUnboundClient<T> : IFluentClient<T> 
        where T : class
    {
        /// <summary>
        /// Sets the container for data not covered by the entity properties. Typically used with OData open types.
        /// </summary>
        /// <param name="expression">The filter expression.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> WithProperties(Expression<Func<T, IDictionary<string, object>>> expression);

        /// <summary>
        /// Casts the collection of base entities as the collection of derived ones.
        /// </summary>
        /// <param name="derivedCollectionName">Name of the derived collection.</param>
        /// <returns>Self.</returns>
        IUnboundClient<IDictionary<string, object>> As(string derivedCollectionName);
        /// <summary>
        /// Casts the collection of base entities as the collection of derived ones.
        /// </summary>
        /// <param name="derivedCollectionName">Name of the derived collection.</param>
        /// <returns>Self.</returns>
        IUnboundClient<U> As<U>(string derivedCollectionName = null) where U : class;
        /// <summary>
        /// Casts the collection of base entities as the collection of derived ones.
        /// </summary>
        /// <param name="expression">The expression for the derived collection.</param>
        /// <returns>Self.</returns>
        IUnboundClient<ODataEntry> As(ODataExpression expression);

        IUnboundClient<T> WithCount();
        /// <summary>
        /// Sets the specified OData filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Filter(string filter);
        /// <summary>
        /// Sets the specified OData filter.
        /// </summary>
        /// <param name="expression">The filter expression.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Filter(ODataExpression expression);
        /// <summary>
        /// Sets the specified OData filter.
        /// </summary>
        /// <param name="expression">The filter expression.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Filter(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Sets the OData function name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Function(string functionName);
        /// <summary>
        /// Sets the OData action name.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Action(string actionName);

        /// <summary>
        /// Sets the specified entry value for update.
        /// </summary>
        /// <param name="value">The value to update the entry with.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Set(object value);
        /// <summary>
        /// Sets the specified entry value for update.
        /// </summary>
        /// <param name="value">The value to update the entry with.</param>
        /// <returns></returns>
        IUnboundClient<T> Set(IDictionary<string, object> value);
        /// <summary>
        /// Sets the specified entry value for update.
        /// </summary>
        /// <param name="value">The value to update the entry with.</param>
        /// <returns></returns>
        IUnboundClient<T> Set(params ODataExpression[] value);

        /// <summary>
        /// Get responce
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetResponse(CancellationToken cancellationToken = default(CancellationToken));

        //TODO: we can use this method if will resolve confilcts with latest version of system.net.formatting.extentions .
        /// <summary>
        /// Get responce
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        //Task<T> GetResponse<T>(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Skips the specified number of entries from the result.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Skip(int count);

        /// <summary>
        /// Limits the number of results with the specified value.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Top(int count);

        /// <summary>
        /// Expands top level of all associations.
        /// </summary>
        /// <param name="expandOptions">The <see cref="ODataExpandOptions"/>.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Expand(ODataExpandOptions expandOptions);
        /// <summary>
        /// Expands the specified associations.
        /// </summary>
        /// <param name="associations">The associations to expand.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Expand(IEnumerable<string> associations);
        /// <summary>
        /// Expands the number of levels of the specified associations.
        /// </summary>
        /// <param name="expandOptions">The <see cref="ODataExpandOptions"/>.</param>
        /// <param name="associations">The associations to expand.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Expand(ODataExpandOptions expandOptions, IEnumerable<string> associations);
        /// <summary>
        /// Expands the specified associations.
        /// </summary>
        /// <param name="associations">The associations to expand.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Expand(params string[] associations);
        /// <summary>
        /// Expands the number of levels of the specified associations.
        /// </summary>
        /// <param name="expandOptions">The <see cref="ODataExpandOptions"/>.</param>
        /// <param name="associations">The associations to expand.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Expand(ODataExpandOptions expandOptions, params string[] associations);
        /// <summary>
        /// Expands the specified associations.
        /// </summary>
        /// <param name="associations">The associations to expand.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Expand(params ODataExpression[] associations);
        /// <summary>
        /// Expands the number of levels of the specified associations.
        /// </summary>
        /// <param name="expandOptions">The <see cref="ODataExpandOptions"/>.</param>
        /// <param name="associations">The associations to expand.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Expand(ODataExpandOptions expandOptions, params ODataExpression[] associations);
        /// <summary>
        /// Expands the specified expression.
        /// </summary>
        /// <param name="expression">The expression for associations to expand.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Expand(Expression<Func<T, object>> expression);
        /// <summary>
        /// Expands the number of levels of the specified associations.
        /// </summary>
        /// <param name="expandOptions">The <see cref="ODataExpandOptions"/>.</param>
        /// <param name="expression">The expression for associations to expand.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Expand(ODataExpandOptions expandOptions, Expression<Func<T, object>> expression);

        /// <summary>
        /// Selects the specified result columns.
        /// </summary>
        /// <param name="columns">The selected columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Select(IEnumerable<string> columns);
        /// <summary>
        /// Selects the specified result columns.
        /// </summary>
        /// <param name="columns">The selected columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Select(params string[] columns);
        /// <summary>
        /// Selects the specified result columns.
        /// </summary>
        /// <param name="columns">The selected columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Select(params ODataExpression[] columns);
        /// <summary>
        /// Selects the specified result columns.
        /// </summary>
        /// <param name="expression">The expression for the selected columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> Select(Expression<Func<T, object>> expression);

        /// <summary>
        /// Sorts the result by the specified columns in the specified order.
        /// </summary>
        /// <param name="columns">The sort columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> OrderBy(IEnumerable<KeyValuePair<string, bool>> columns);
        /// <summary>
        /// Sorts the result by the specified columns in ascending order.
        /// </summary>
        /// <param name="columns">The sort columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> OrderBy(params string[] columns);
        /// <summary>
        /// Sorts the result by the specified columns in ascending order.
        /// </summary>
        /// <param name="columns">The sort columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> OrderBy(params ODataExpression[] columns);
        /// <summary>
        /// Sorts the result by the specified columns in ascending order.
        /// </summary>
        /// <param name="expression">The expression for the sort columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> OrderBy(Expression<Func<T, object>> expression);
        /// <summary>
        /// Sorts the result by the specified columns in ascending order.
        /// </summary>
        /// <param name="expression">The expression for the sort columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> ThenBy(Expression<Func<T, object>> expression);
        /// <summary>
        /// Sorts the result by the specified columns in descending order.
        /// </summary>
        /// <param name="columns">The sort columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> OrderByDescending(params string[] columns);
        /// <summary>
        /// Sorts the result by the specified columns in descending order.
        /// </summary>
        /// <param name="columns">The sort columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> OrderByDescending(params ODataExpression[] columns);
        /// <summary>
        /// Sorts the result by the specified columns in descending order.
        /// </summary>
        /// <param name="expression">The expression for the sort columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> OrderByDescending(Expression<Func<T, object>> expression);
        /// <summary>
        /// Sorts the result by the specified columns in descending order.
        /// </summary>
        /// <param name="expression">The expression for the sort columns.</param>
        /// <returns>Self.</returns>
        IUnboundClient<T> ThenByDescending(Expression<Func<T, object>> expression);

        /// <summary>
        /// Requests the number of results.
        /// </summary>
        /// <returns>Self.</returns>
        IUnboundClient<T> Count();
    }
}