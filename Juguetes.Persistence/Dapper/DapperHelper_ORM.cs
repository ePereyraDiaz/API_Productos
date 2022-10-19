using Dapper;
using System.Data;
using System.Text;

namespace Juguetes.Persistence.Dapper
{
    public sealed class DapperHelper_ORM
    {
        private IDapper_ORM Dapper_ { get; }

        public DapperHelper_ORM(IDapper_ORM Dapper_ORM)
        {
            Dapper_ = Dapper_ORM;
        }

        public async Task<TResult> GetAsync<TResult, TRequest>(string name, TRequest model)
        {
            try
            {
                var sp = $"{name}";
                var dynamicParameters = CreateDynamicParameters(name, model);
                var Response = await Task.FromResult(Dapper_.Get<TResult>(sp, dynamicParameters.Parameters));
                return Response;
            }
            catch (Exception ex)
            {
                return await Task.FromException<TResult>(ex);
            }
        }

        public async Task<IList<TResult>> GetAllAsync<TResult, TRequest>(string name, TRequest model)
        {
            try
            {
                var sp = $"{name}";
                var dynamicParameters = CreateDynamicParameters(name, model);

                return await Task.FromResult(Dapper_.GetAll<TResult>(sp, dynamicParameters.Parameters));
            }
            catch (Exception ex)
            {
                return await Task.FromException<IList<TResult>>(ex);
            }
        }

        private DynamicParametersPair CreateDynamicParameters(string name, object obj)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            var linebreak = string.Empty;
            var space = " ";
            var tab = string.Empty;

            builder.Append($"{linebreak}EXEC").Append(' ').Append(name).Append(space);

            if (obj != null)
            {
                foreach (var prop in obj.GetType().GetProperties())
                {
                    var property = prop.Name;
                    var pair = AnalyzeProperty(prop.PropertyType, prop.GetValue(obj));

                    parameters.Add(property, pair.Item1, pair.Item2);

                    builder.Append($"{tab}@").Append(property).Append(" = ").Append(Beautify(pair.Item1)).Append(',').Append(space);
                }

                var end = builder.Length;
                if (obj.GetType().GetProperties().Length > 0)
                {
                    builder.Remove(end - 2, 2);
                }

            }

            return new DynamicParametersPair(parameters, builder.ToString());
        }

        private object Beautify(object value)
        {
            var type = value.GetType();

            return type.FullName switch
            {
                "System.String" => $"'{value}'",
                "System.DateTime" => $"'{(DateTime)value:yyyy-MM-dd}'",
                _ => value,
            };
        }

        private static Tuple<object, DbType> AnalyzeProperty(Type type, object value)
        {
            var dbType = DbType.Object;

            switch (type.FullName)
            {
                case "System.Boolean":

                    dbType = DbType.Boolean;

                    break;

                case "System.Byte":

                    dbType = DbType.Byte;

                    break;

                case "System.Byte[]":

                    dbType = DbType.Binary;

                    break;

                case "System.Int32":

                    dbType = DbType.Int32;

                    break;

                case "System.String":

                    dbType = DbType.String;
                    value ??= string.Empty;

                    break;

                case "System.DateTime":

                    dbType = DbType.DateTime;
                    value ??= new DateTime(1900, 1, 1);

                    break;

                case "System.Decimal":

                    dbType = DbType.Decimal;

                    break;

                case "System.Single":

                    dbType = DbType.Double;

                    break;

                case "System.Int64":

                    dbType = DbType.Int64;

                    break;
            }

            return new Tuple<object, DbType>(value, dbType);
        }

        private sealed class DynamicParametersPair
        {
            public DynamicParameters Parameters { get; }

            public string ExcecutableString { get; }

            public DynamicParametersPair(DynamicParameters parameters, string excecutableString)
            {
                Parameters = parameters;
                ExcecutableString = excecutableString;
            }
        }
    }
}