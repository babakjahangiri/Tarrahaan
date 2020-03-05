using System;
using System.Data;
using Itanc.DbManager.DbData;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public class CurrencyRepository:ICurrencyRepository
    {

        public Currency Load(byte currencyId)
        {

            var sqlQuery = "SELECT CurrencyId,CurrencySign,ISOCode,Visible FROM Currency WHERE CurrencyId=" + currencyId;
            var dbcom = new DbCommand(sqlQuery);

            var currencyEntity = new Currency();

            if (!dbcom.HasRow()) return currencyEntity;

            dbcom.FillRow();

            currencyEntity.CurrencyId = (byte) dbcom.Row["currencyId"];
            currencyEntity.CurrencySign = (string) dbcom.Row["CurrencySign"];
            currencyEntity.IsoCode = (string) dbcom.Row["IsoCode"];
            currencyEntity.Visible = (bool) dbcom.Row["Visible"];

            return currencyEntity;

        }


        public DataTable ListCurrencies()
        {
            const string sqlQuery = "SELECT Currency.CurrencyId, Currency.CurrencySign, Currency.ISOCode " +
                                    " FROM Currency WHERE (Currency.Visible = 1)";

            var ddt = new DbDataTable("Currency")
            {SqlQuery = sqlQuery};

            try
            {
                return ddt.GetTable();
            }
            catch (Exception ex)
            {
                throw new Exception("CurrencyRepository -> ListCurrencies : " + ex.Message);
            }
        }

        public string GetName(byte currencyId)
        {

            var query = "SELECT CurrencySign FROM Currency WHERE currencyId=" + currencyId;
            var dbcom = new DbCommand(query);

            if (!dbcom.HasRow()) return String.Empty;

            try
            {
                return (string)dbcom.ExecuteReaderSingleResult();
            }
            catch (Exception ex)
            {
                throw new Exception("CurrencyRepository > GetName:" + ex.Message);
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
 
    }
}