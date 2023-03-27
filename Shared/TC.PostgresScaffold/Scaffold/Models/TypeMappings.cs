namespace TC.PostgresScaffold.Scaffold.Models;

public static class TypeMappings
{
    public static Type Map(string dbType)
    {
        return dbType switch
        {
            "int" => typeof(int),
            "integer" => typeof(int),
            "int4" => typeof(int),
            "varchar" => typeof(string),
            "text" => typeof(string),
            "timestamp" => typeof(DateTime),
            "bigint" => typeof(long),
            "bigserial" => typeof(long),
            "int8" => typeof(long),
            "serial8" => typeof(long),
            "bit" => typeof(string),
            "bit varying" => typeof(string),
            "varbit" => typeof(string),
            "boolean" => typeof(bool),
            "bool" => typeof(bool),
            // "box" => typeof(), // rectangular box on a plane
            "bytea" => typeof(byte[]),
            "character" => typeof(string),
            "char" => typeof(string),
            "character varying" => typeof(string),
            "cidr" => typeof(string), // IPv4 or IPv6 network address
            // "circle" => typeof(), // circle on a plane
            "date" => typeof(DateOnly),
            "double precision" => typeof(double),
            "float8" => typeof(double),
            "inet" => typeof(string), // IPv4 or IPv6 host address
            "interval" => typeof(TimeSpan),
            "json" => typeof(string),
            "jsonb" => typeof(object),
            // "line" => typeof(), // infinite line on a plane
            // "lseg" => typeof(), // line segment on a plane
            // "macaddr" => typeof(),
            // "macaddr8" => typeof(),
            "money" => typeof(decimal),
            // "numeric [ (p, s) ]" => typeof(), // exact numeric of selectable precision
            // "decimal [ (p, s) ]" => typeof(),
            // "path" => typeof(), // geometric path on a plane
            // "pg_lsn" => typeof(),
            // "pg_snapshot" => typeof(),
            // "point" => typeof(),
            // "polygon" => typeof(),
            "real" => typeof(float),
            "float4" => typeof(float),
            "smallint" => typeof(Int16),
            "int2" => typeof(Int16),
            "smallserial" => typeof(Int16),
            "serial2" => typeof(Int16),
            "serial" => typeof(int),
            "serial4" => typeof(int),
            "time" => typeof(TimeOnly),
            "time with time zone" => typeof(TimeOnly),
            "timetz" => typeof(TimeOnly),
            "timestamp without time zone" => typeof(DateTime),
            "timestamp with time zone" => typeof(DateTime),
            "timestamptz" => typeof(DateTime),
            "tsquery" => typeof(string),
            "tsvector" => typeof(string),
            // "txid_snapshot" => typeof(),
            "uuid" => typeof(string),
            // "xml" => typeof(),
            _ => null
        };
    }
}