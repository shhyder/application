using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataSet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlTypes;
using System.Data.Common;



namespace DataAccessLayer.Parameter
{
    //----------------------------------------------------------------
    /// Class: Secret_Question
    //----------------------------------------------------------------
    public class Secret_Question : BaseParameter
    {

        System.Int32 _secret_question_ID;

        public Secret_Question(Database db, DbTransaction dbTrans)
        {
            _db = db;
            _transaction = dbTrans;
        }
        public Secret_Question(Database db)
        {
            _db = db;
        }
        public Secret_Question()
        {

        }
        public Secret_Question(System.Int32 secret_question_ID)
        {
            _secret_question_ID = secret_question_ID;
        }

        //----------------------------------------------------------------
        /// Get specific records: Secret_Question
        //----------------------------------------------------------------
        public override IDataReader GetItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetSecret_Question");
            _db.AddInParameter(_dbCommand, _DSParam.Secret_Question.Secret_Question_IDColumn.ToString(), DbType.Int32, _secret_question_ID);
            return _db.ExecuteReader(_dbCommand);
        }


        //----------------------------------------------------------------
        /// Get specific records: All Secret_Question
        //----------------------------------------------------------------
        public override IDataReader GetAllItem()
        {

            _dbCommand = _db.GetStoredProcCommand("GetAllSecret_Question");
            return _db.ExecuteReader(_dbCommand);
        }



       

        //----------------------------------------------------------------
        /// Delete: Secret_Question
        //----------------------------------------------------------------
        public override IDataReader Delete()
        {

            _dbCommand = _db.GetStoredProcCommand("GetSecret_Question");
            _db.AddInParameter(_dbCommand, _DSParam.Secret_Question.Secret_Question_IDColumn.ToString(), DbType.Int32, _secret_question_ID);
            return _db.ExecuteReader(_dbCommand);
        }



        //----------------------------------------------------------------
        /// Insert record: Secret_Question
        //----------------------------------------------------------------
        public override IDataReader Insert( DSParameter   ds)
        {
            _dbCommand = _db.GetStoredProcCommand("InsertSecret_Question");
            _db.AddOutParameter(_dbCommand, ds.Secret_Question.Secret_Question_IDColumn.ToString(), DbType.Int32, 20);
            _db.AddInParameter(_dbCommand, ds.Secret_Question.Secret_QuestionColumn.ToString(), DbType.String, ds.Secret_Question.Rows[0][ds.Secret_Question.Secret_QuestionColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            _ID = System.Int32.Parse(_db.GetParameterValue(_dbCommand, "@Secret_Question_ID").ToString());
            return dr;
        }



        //----------------------------------------------------------------
        /// Update: Secret_Question
        //----------------------------------------------------------------
        public override IDataReader Update(DSParameter ds)
        {
            _dbCommand = _db.GetStoredProcCommand("UpdateSecret_Question");
            _db.AddInParameter(_dbCommand, ds.Secret_Question.Secret_Question_IDColumn.ToString(), DbType.Int32, ds.Secret_Question.Rows[0][ds.Secret_Question.Secret_Question_IDColumn.ToString()]);
            _db.AddInParameter(_dbCommand, ds.Secret_Question.Secret_QuestionColumn.ToString(), DbType.String, ds.Secret_Question.Rows[0][ds.Secret_Question.Secret_QuestionColumn.ToString()]);
            IDataReader dr = _db.ExecuteReader(_dbCommand, _transaction);
            dr.Close();
            return dr;
        }

    }

}
