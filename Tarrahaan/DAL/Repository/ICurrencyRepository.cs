using System;
using System.Data;
using Tarrahaan.DAL.Entities;

namespace Tarrahaan.DAL.Repository
{
    public interface ICurrencyRepository:IDisposable
    {
        DataTable ListCurrencies();
        Currency Load(byte currencyId);
        string GetName(byte currencyId);

    }
}
