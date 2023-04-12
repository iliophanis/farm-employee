using System.Globalization;
using System.Linq.Dynamic.Core;
using server.Modules.Common.Responses;
using System.Linq.Expressions;
#nullable enable

namespace server.Modules.Common.Services
{
    public class DataTableService
    {
        public async Task<DataTableResponse<List<T>>> GetPagedDataTableAsync<T>(IQueryable<T> model, DataTableRequest request)
        {
            var queryExpression = model.Expression;
            model = SearchQuery(model, request.Columns);

            model = SortQuery(model, request.Columns);
            var totalRecords = await model.CountAsync();
            var data = await PaginateAsync(model, request.Skip, request.Take);

            return new DataTableResponse<List<T>>()
            {
                Data = data,
                Pagination = new Pagination { Skip = request.Skip, Take = request.Take, TotalRecords = totalRecords }
            };
        }

        /// <summary>
        ///     Paginate the data
        /// </summary>
        /// <param name="model">The model to paginate</param>
        /// <param name="skip">The number of records to skip</param>
        /// <param name="pageSize">The number of records to take</param>
        /// <returns></returns>
        private async Task<List<T>> PaginateAsync<T>(IQueryable<T> model, int skip, int pageSize)
        {
            try
            {
                return await model.Skip(skip).Take(pageSize).ToListAsync();
            }
            catch (NullReferenceException)
            {
                return new List<T>();
            }
        }

        /// <summary>
        ///     Sorts the data based on the given columns and directions
        /// </summary>
        /// <param name="model">The model to sort</param>
        /// <param name="columns">List of <see cref="Column" /> objects</param>
        /// <returns>The sorted model</returns>
        private IQueryable<T> SortQuery<T>(IQueryable<T> model, IReadOnlyList<Column> columns)
        {
            var column = "Id";
            var orderedQueryable = model.OrderBy(column + " " + "DESC");
            if (columns.Count() == 0) return orderedQueryable;

            if (columns[0].Orderable)
            {
                column = FormatColumnName(columns[0].Name);
                orderedQueryable = model
                    .OrderBy(column + " " + columns[0].Direction);
            }

            for (var i = 1; i < columns.Count; i++)
            {
                if (!columns[i].Orderable) continue;
                column = FormatColumnName(columns[i].Name);
                orderedQueryable = orderedQueryable.ThenBy(column + " " + columns[i].Direction);
            }

            return orderedQueryable;
        }

        /// <summary>
        ///     Search a queryable
        /// </summary>
        /// <param name="model">Queryable to search</param>
        /// <param name="searchValue">The value to search for</param>
        /// <param name="columns">A list of <see cref="Column" /></param>
        /// <returns>A filtered queryable</returns>
        private IQueryable<T> SearchQuery<T>(IQueryable<T> model, List<Column> columns)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? orExpression = null;
            foreach (var filter in columns)
            {
                if (!filter.Searchable) continue;
                // Check if the filter column is a navigation property
                var property = typeof(T).GetProperty(FormatColumnName(filter.Name));
                // If the column is not a navigation property, use the default filter logic
                var entityProperty = Expression.Property(parameter, FormatColumnName(filter.Name));
                var filterExpression = Expression.Call(entityProperty, "Contains", null, Expression.Constant(filter.SearchValue));
                orExpression = orExpression == null
                    ? filterExpression
                    : Expression.Or(orExpression, filterExpression);

            }
            if (orExpression == null) return model;
            var filterLambda = Expression.Lambda<Func<T, bool>>(orExpression, parameter);
            return model.Where(filterLambda);
        }

        /// <summary>
        ///     Formats a string as a column name.
        /// </summary>
        /// <param name="name">The name to format.</param>
        /// <returns>The formatted name.</returns>
        private string FormatColumnName(string name)
        {
            var col = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            col = col.Replace(" ", "");
            col = col.Split("_")
                .Aggregate((current, next) => current + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(next));

            return col;
        }
    }

    public class DataTableRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<Column> Columns { get; set; } = null!;
    }
    public class Column
    {
        public string Name { get; set; } = null!;
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public string Direction { get; set; } = null!;
        public string SearchValue { get; set; } = "";
    }
}