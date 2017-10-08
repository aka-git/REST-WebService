using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Services
{
    public interface IJSONService
    {
        /// <summary>
        /// Адрес веб сервера
        /// </summary>
        string UrlService { get; set; }

        /// <summary>
        /// GET запрос
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <returns></returns>
        TResponse Get<TResponse>(string url);

        /// <summary>
        /// POST запрос
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <typeparam name="TRequestContent">Тип запроса для сирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <param name="content">Объект запроса</param>
        /// <returns></returns>
        TResponse Post<TResponse, TRequestContent>(string url, TRequestContent content);

        /// <summary>
        /// PUT запрос
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <typeparam name="TRequestContent">Тип запроса для сирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <param name="content">Объект запроса</param>
        /// <returns></returns>
        TResponse Put<TResponse, TRequestContent>(string url, TRequestContent content);

        /// <summary>
        /// GET запрос асинхронный
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string url);

        /// <summary>
        /// POST запрос асинхронный
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <typeparam name="TRequestContent">Тип запроса для сирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <param name="content">Объект запроса</param>
        /// <returns></returns>
        Task<TResponse> PostAsync<TResponse, TRequestContent>(string url, TRequestContent content);

        /// <summary>
        /// PUT запрос асинхронный
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <typeparam name="TRequestContent">Тип запроса для сирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <param name="content">Объект запроса</param>
        /// <returns></returns>
        Task<TResponse> PutAsync<TResponse, TRequestContent>(string url, TRequestContent content);

    }
}
