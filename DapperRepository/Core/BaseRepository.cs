using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Reflection;

namespace DapperRepository.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        IConfiguration _conf;
        string _table;
        List<String> _types = new List<string>();

        public BaseRepository(IConfiguration conf )
        {
            new SqlConnection(_conf.GetConnectionString("Baglanti"));
            _conf = conf;
            _table = typeof(T).Name;
            
            _types.Add("Int32");
            _types.Add("String");
            _types.Add("Int64");

        }


        public SqlConnection GetConnection()
        {

            return new SqlConnection(_conf.GetConnectionString("Baglanti"));
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> List()
        {
            var con = GetConnection();
            var x = _table;
            return con.Query<T>($"select * from {_table} ").ToList();
        }
        public dynamic List(string tablename)
        {
            var con = GetConnection();
            return con.Query<T>($"select * from {tablename} ").ToList();
        }
        public void Insert(T t, string tablename)
        {
            var con = GetConnection();
            var props = GetProperties();
            props.RemoveAt(0);
            string cols = GetInsertColumns(props);
            string val = GetVal(props);
            string qry = $"insert into {tablename} {cols} {val}  ";
         //   string ad = "Şamil";
            con.Execute(qry, t);

        }

        public int SaveRange(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        public void Update(T t, string tablename, int id)
        {
            var con = GetConnection();
            var props = GetProperties();
            string key = props[0].Name;

            //  props.RemoveAt(0);
            string val = GetUpdateColumns(props);
            string where = "where";
            string qry = $"update {tablename} {val} {where}   {key} = {id} ";
         //   string ad = "Şamil";

            con.Execute(qry, t);
        }
        public List<PropertyInfo> GetProperties()
        {
            var props = typeof(T).GetProperties().ToList();
            return props;
        }
        private string GetInsertColumns(List<PropertyInfo> props)
        {
            string col = "(";
            foreach (var item in props)
            {
                //  var type  = item.PropertyType.Name;
                var type = _types.Find(x => x.Contains(item.PropertyType.Name));
                if (!string.IsNullOrEmpty(type))
                {
                    col += item.Name + ",";
                }
            }
            col = col.Remove(col.Length - 1, 1);
            col += ")";
            return col;
        }
        private string GetVal(List<PropertyInfo> props)
        {
            string val = "values (";
            foreach (var item in props)
            {

                val += "@" + item.Name + ",";
            }
            val = val.Remove(val.Length - 1, 1);
            val += ")";

            return val;
        }
        private string GetUpdateColumns(List<PropertyInfo> props)
        {
            string val = "set ";
            foreach (var item in props)
            {
                val += item.Name + " " + "=" + "@" + item.Name + ",";
            }
            val = val.Remove(val.Length - 1, 1);
            return val;
        }
    }
}
