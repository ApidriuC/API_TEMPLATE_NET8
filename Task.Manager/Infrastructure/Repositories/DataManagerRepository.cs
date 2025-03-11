using TaskProject.Manager.Domain.DataAccess;
using TaskProject.Manager.Domain.Interfaces;

namespace TaskProject.Manager.Infrastructure.Repositories;

/// <summary>
///  Repositorio de comunicación con la base de datos SQL. 
/// </summary>
/// <param name="dataBase">Capa de conexiones del proyecto.</param>
internal class DataManagerRepository(IDataBaseManager dataBase) : QueryManager(dataBase.DbTasksSQL), IDataManager
{

}
