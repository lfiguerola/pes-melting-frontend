using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MeltingApp.Models;

namespace MeltingApp.Interfaces
{
    public interface IDataBaseService
    {
        /// <summary>
        /// Elimina totes les taules de la bd
        /// </summary>
        void DropTables();

        /// <summary>
        /// Crea totes les taules de la bd
        /// </summary>
        void InitializeTables();

        /// <summary>
        /// Retorna una entitat(un model) sense fill (classes complexes) a partir d'una condició
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get<T>(Expression<Func<T, bool>> predicate) where T : EntityBase, new();

        /// <summary>
        /// Retorna una entitat(un model) amb fills (classes complexes) a partir d'una condició
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T GetWithChildren<T>(Expression<Func<T, bool>> predicate) where T : EntityBase, new();

        /// <summary>
        /// Retorna una llista d'entitats sense fill (classes complexes) a partir d'una condició
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> GetCollection<T>(Expression<Func<T, bool>> predicate = null) where T : EntityBase, new();

        /// <summary>
        /// Retorna una llista d'entitats amb fills (classes complexes) a partir d'una condició
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> GetCollectionWithChildren<T>(Expression<Func<T, bool>> predicate = null) where T : EntityBase, new();

        /// <summary>
        /// Quan hi ha grans quantits de dades, retorna una part d'una llista que compleix una condició
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="skip">quants elements s'han de saltar a l'inici del resultat</param>
        /// <param name="take">quan elements ha de retornar el resultat</param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> FindWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null) where T : EntityBase, new();

        /// <summary>
        /// Quan hi ha grans quantits de dades, retorna una part d'una llista amb fills que compleix una condició
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="skip">quants elements s'han de saltar a l'inici del resultat</param>
        /// <param name="take">quan elements ha de retornar el resultat</param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> FindWithChildrenWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null) where T : EntityBase, new();

        /// <summary>
        /// Inserció d'una entitat sense fills
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert<T>(T entity) where T : EntityBase, new();

        /// <summary>
        /// Inserció d'una entitat i els seus fills
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int InsertWithChildren<T>(T entity) where T : EntityBase, new();

        /// <summary>
        /// Inserció d'una llista d'entitats sense fills
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void InsertCollection<T>(T entities) where T : IEnumerable;

        /// <summary>
        /// Inserció d'una llista d'entitats amb fills
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void InsertCollectionWithChildren<T>(T entities) where T : IEnumerable;

        /// <summary>
        /// Actualitza una entitat existent sense fills i si no existeix la crea
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update<T>(T entity) where T : EntityBase, new();

        /// <summary>
        /// Actualitza una entitat existent i els seus fills i si no existeix la crea i als seus fills tambe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int UpdateWithChildren<T>(T entity) where T : EntityBase, new();

        /// <summary>
        /// Actualitza un llistat d'entitats sense fills i crea les que no existeixi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void UpdateCollection<T>(T entities) where T : IEnumerable;

        /// <summary>
        /// Actualitza un llistat existent i els seus fills i crea les entitats i els seus fills que no existeixin
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void UpdateCollectionWithChildren<T>(T entities) where T : IEnumerable;

        /// <summary>
        /// Elimina en cascada totes les entitats de la taula d'un tipus concret
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Clear<T>() where T : EntityBase, new();

        /// <summary>
        /// Elimina en cascada una entitat d'una taula
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        void Delete<T>(T entity, bool recursive = true) where T : EntityBase, new();

        void DeleteCollection<T>(T entities, bool recursive = true) where T : IEnumerable;
    }
}
