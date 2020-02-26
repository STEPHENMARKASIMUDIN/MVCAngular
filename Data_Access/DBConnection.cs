using Dapper;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace MVCAngular
{
    public class DBConnection
    {
        private readonly MySqlConnection MyConnection = null;
        private MySqlTransaction MyTransaction = null;
        private readonly object connLock = new object();
        public DBConnection()
        {
            lock (connLock)
            {
                if (MyConnection == null)
                {
                    MyConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString);
                }
            }
        }
        private readonly ILog _Logger = LogManager.GetLogger(typeof(DBConnection));

        public List<Signatory> GetSignatories()
        {
            try
            {
                MyConnection.Open();
                List<Signatory> signatory = MyConnection.Query<Signatory>(StoredProcedures.Get_Signatories, null, null, false, 60, CommandType.StoredProcedure).ToList();
                MyConnection.Dispose();
                return signatory;
            }
            catch (MySqlException mex)
            {
                _Logger.Fatal(mex.ToString());
                MyConnection.Dispose();
                return null;
            }
        }
        public int AddSignatory(Signatory signatory)
        {
            if (signatory is null)
            {
                throw new ArgumentNullException(nameof(signatory));
            }
            try
            {
                MyConnection.Open();
                using (MyTransaction = MyConnection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        int success = MyConnection.Execute(StoredProcedures.Add_Signatories,
                            new
                            {
                                email = signatory.Email,
                                first_name = signatory.FirstName,
                                middle_name = signatory.MiddleName,
                                last_name = signatory.LastName,
                                division = signatory.Division,
                                _level = signatory.Level
                            },
                            MyTransaction, 60, CommandType.StoredProcedure);
                        if (success < 1)
                        {
                            MyTransaction.Rollback();
                        }
                        else
                        {
                            MyTransaction.Commit();
                        }
                        return success;
                    }
                    catch (Exception ex)
                    {
                        _Logger.Fatal(ex.ToString());
                        MyTransaction.Rollback();
                        MyConnection.Dispose();
                        return 0;
                    }
                }

            }
            catch (MySqlException mex)
            {
                _Logger.Fatal(mex.ToString());
                MyConnection.Dispose();
                return 0;
            }
        }

    }
}