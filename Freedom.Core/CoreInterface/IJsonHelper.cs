using Freedom.Utility.Models.Retencion.Export;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freedom.Core.CoreInterface
{
    public interface IJsonHelper<T>
    {
        Task<string> SerializeObject(List<T> collection);

        Task<List<T>> DeserializeObject(string JsonString);

        /// <summary>
        /// Import JsonFile With Dialog
        /// </summary>
        /// <returns></returns>
        Task<List<T>> ImportJsonFile();

        /// <summary>
        /// import file in silent mode
        /// </summary>
        Task<List<T>> ImportJsonFile(string path);

        /// <summary>
        /// export jsonfile with Dialog
        /// </summary>
        Task<bool> ExportJsonFile(List<T> collection);

        /// <summary>
        /// Export file in silent mode
        /// </summary>
        Task<bool> ExportJsonFile(List<T> collection, string path);

        /// <summary>
        /// Exportar a Windows la Retencion para luego importar en software Tesaka
        /// </summary>
        Task<bool> ExportRetencionJsonFile(List<Root> collection);
    }
}