namespace BoilerplateService.Infrastructures.Extensions
{
    /// <summary>
    /// Extension methods for Collection class.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Break up a list into batches
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            T[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new T[size];

                bucket[count++] = item;

                if (count != size)
                    continue;

                yield return bucket.Select(x => x);

                bucket = null;
                count = 0;
            }

            // Return the last bucket with all remaining elements
            if (bucket != null && count > 0)
            {
                Array.Resize(ref bucket, count);
                yield return bucket.Select(x => x);
            }
        }

        /// <summary>
        /// Convert a simple collection to a data table with single column
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source, string columnName = "id")
        {
            var table = new DataTable();
            table.Columns.Add(columnName);

            if (source is null)
            {
                return table;
            }

            foreach (var item in source)
            {
                table.Rows.Add(item);
            }

            return table;
        }

        /// <summary>
        /// Convert a simple collection to a data table with single column
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source, IDictionary<string, Type> columns)
        {
            var table = new DataTable();
            table.Columns.AddRange(columns.Select(x => new DataColumn(x.Key, x.Value)).ToArray());

            if (source is null)
            {
                return table;
            }

            foreach (var item in source)
            {
                var row = table.NewRow();

                foreach (DataColumn col in table.Columns)
                {
                    var value = item.GetType().GetProperty(col.ColumnName).GetValue(item);
                    row[col.ColumnName] = value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static string Join<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }
    }
}