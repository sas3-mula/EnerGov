using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EnerGov
{
    public class DbHelper
    {
        private IConfigurationSection _appSettings;
        private int _commandTimeOut;
        private SqlConnection _connection;
        public DbHelper()
        {
            this._appSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings");
            this._connection = new SqlConnection(_appSettings["QuizBuilderDatabase"]);
            int.TryParse(_appSettings["CommandTimeOut"], out this._commandTimeOut);
        }
        public DataSet ExecStoredProcedure(string spName, List<SqlParameter> parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection.Open();
                SqlCommand command = new SqlCommand(spName, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = this._commandTimeOut;
                foreach (var item in parameters)
                {
                    command.Parameters.Add(item);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute stored Procedure : " + spName, ex);
            }
            finally
            {
                _connection.Close();
            }

            return ds;
        }

        public DataSet GetAllEmployees()
        {
            var parameters = new List<SqlParameter>();
            return ExecStoredProcedure(_appSettings["GetAllEmployees"], parameters);
        }

        public DataSet GetEmployeesByManager(string @managerId)
        {
            var parameters = new List<SqlParameter>();
            SqlParameter paramManagerId = new SqlParameter("@uName", SqlDbType.VarChar);
            paramManagerId.Value = managerId;
            parameters.Add(paramManagerId);

            return ExecStoredProcedure(_appSettings["GetEmployeesUnderManager"], parameters);
        }

        public DataSet CreateEmployee(string empId, string firstName, string lastName, string roles, string mId)
        {
            var parameters = new List<SqlParameter>();
            SqlParameter paramEmpId = new SqlParameter("@empId", SqlDbType.VarChar);
            paramEmpId.Value = empId;
            SqlParameter paramFirstName = new SqlParameter("@firstName", SqlDbType.VarChar);
            paramFirstName.Value = firstName;
            SqlParameter paramlastName = new SqlParameter("@lastName", SqlDbType.VarChar);
            paramlastName.Value = lastName;
            SqlParameter paramRoles = new SqlParameter("@roles", SqlDbType.VarChar);
            paramRoles.Value = roles;
            SqlParameter paramManagerId = new SqlParameter("@managerId", SqlDbType.VarChar);
            paramManagerId.Value = mId;

            parameters.Add(paramEmpId);
            parameters.Add(paramFirstName);
            parameters.Add(paramlastName);
            parameters.Add(paramRoles);
            parameters.Add(paramManagerId);

            return ExecStoredProcedure(_appSettings["CreateEmployee"], parameters);
        }

        public DataSet CheckUserName(string empId)
        {
            var parameters = new List<SqlParameter>();
            SqlParameter paramEmpId = new SqlParameter("@id", SqlDbType.VarChar);
            paramEmpId.Value = empId;
            parameters.Add(paramEmpId);
            return ExecStoredProcedure(_appSettings["ValidateEmployeeId"], parameters);
        }
    }
}
