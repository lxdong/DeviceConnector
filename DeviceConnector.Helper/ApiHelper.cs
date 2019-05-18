/*-----------------------------------------------
// Copyright (C) 2018 南京戎光软件科技有限公司  版权所有。
// 文件名称：    ApiHelper
// 功能描述：    
// 创建标识：    panshuai 2018-07-07 10:56:10
// 修改标识：    panshuai 2018-07-07 10:56:10
// 修改描述:     
-----------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace DeviceConnector.Helper
{
    public class ApiHelper
    {
        //BIMAPI地址
        public static readonly string BaseUri = ConfigurationManager.AppSettings["ApiUri"];

        #region GET请求--异步方法
        /// <summary>
        /// GET请求--异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static async Task<T> TryGetAsync<T>(string action, Dictionary<string, string> param)
        {
            using (HttpClient client = new HttpClient())
            {
                //基地址/域名
                client.BaseAddress = new Uri(BaseUri);
                //设定传输格式为json
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("Accept-Charset", "GB2312,utf-8;q=0.7,*;q=0.7");

                StringBuilder strUri = new StringBuilder();
                //方法
                strUri.AppendFormat("{0}?", action);
                //参数
                if (param.Count > 0)
                {
                    foreach (KeyValuePair<string, string> pair in param)
                    {
                        strUri.AppendFormat("{0}={1}&&", pair.Key, pair.Value);
                    }
                    strUri.Remove(strUri.Length - 2, 2);//去掉'&&'
                }
                else
                {
                    strUri.Remove(strUri.Length - 1, 1);//去掉'？'
                }
                HttpResponseMessage response = await client.GetAsync(strUri.ToString());
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    return default(T);
                }
            }
        }
        #endregion

        #region 无参数GET请求--异步方法
        /// <summary>
        /// 无参数GET请求--异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<T> TryGetAsync<T>(string action)
        {
            using (HttpClient client = new HttpClient())
            {
                //基地址/域名
                client.BaseAddress = new Uri(BaseUri);
                //设定传输格式为json
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(action);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    return default(T);
                }
            }
        }
        #endregion

        #region POST请求--异步方法
        /// <summary>
        /// POST请求--异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static async Task<T> TryPostAsync<T>(string action, object param)
        {
            using (HttpClient client = new HttpClient())
            {
                //基地址/域名
                client.BaseAddress = new Uri(BaseUri);
                //设定传输格式为json
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(action, param, new JsonMediaTypeFormatter());
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    return default(T);
                }
            }
        }
        #endregion

        #region GET请求--同步方法
        /// <summary>
        /// GET请求--同步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static T TryGet<T>(string action, Dictionary<string, string> param)
        {
            using (HttpClient client = new HttpClient())
            {
                //基地址/域名
                client.BaseAddress = new Uri(BaseUri);
                //设定传输格式为json
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                StringBuilder strUri = new StringBuilder();
                //方法
                strUri.AppendFormat("{0}?", action);
                //参数
                if (param.Count > 0)
                {
                    foreach (KeyValuePair<string, string> pair in param)
                    {
                        strUri.AppendFormat("{0}={1}&&", pair.Key, pair.Value);
                    }
                    strUri.Remove(strUri.Length - 2, 2);//去掉'&&'
                }
                else
                {
                    strUri.Remove(strUri.Length - 1, 1);//去掉'？'
                }
                HttpResponseMessage response = client.GetAsync(strUri.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
                else
                {
                    return default(T);
                }
            }
        }
        #endregion

        #region 无参数GET请求-同步方法
        /// <summary>
        /// 无参数GET请求-同步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T TryGet<T>(string action)
        {
            using (HttpClient client = new HttpClient())
            {
                //基地址/域名
                client.BaseAddress = new Uri(BaseUri);
                //设定传输格式为json
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(action).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
                else
                {
                    return default(T);
                }
            }
        }
        #endregion

        #region POST请求--同步方法
        /// <summary>
        /// POST请求--同步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static T TryPost<T>(string action, object param)
        {
            using (HttpClient client = new HttpClient())
            {
                //基地址/域名
                client.BaseAddress = new Uri(BaseUri);
                //设定传输格式为json
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(action, param, new JsonMediaTypeFormatter()).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
                else
                {
                    return default(T);
                }
            }
        }
        #endregion

        #region 批量文件上传--不含参数/不改文件名
        /// <summary>
        /// 批量文件上传--不含参数/不改文件名
        /// </summary>
        /// <param name="action">接口路径</param>
        /// <param name="filePaths">文件路径</param>
        /// <returns></returns>
        public static async Task<string> TryUploadAsync(string action, List<string> filePaths)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    //基地址/域名
                    client.BaseAddress = new Uri(BaseUri);

                    foreach (string filePath in filePaths)
                    {
                        var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                        string fileName = System.IO.Path.GetFileName(filePath);
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = fileName };
                        content.Add(fileContent);
                    }
                    HttpResponseMessage response = await client.PostAsync(action, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return await Task.FromResult("");
                    }
                }

            }
        }
        #endregion

        #region 批量文件上传--不含参数/改变文件名
        /// <summary>
        /// 批量文件上传--不含参数/改变文件名
        /// </summary>
        /// <param name="action">接口路径</param>
        /// <param name="filePaths">文件路径,文件名</param>
        /// <returns></returns>
        public static async Task<string> TryUploadAsync(string action, Dictionary<string, string> filePaths)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    //基地址/域名
                    client.BaseAddress = new Uri(BaseUri);
                    foreach (var filePath in filePaths)
                    {
                        var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath.Key));
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = filePath.Value };
                        content.Add(fileContent);
                    }
                    HttpResponseMessage response = await client.PostAsync(action, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return await Task.FromResult("");
                    }
                }

            }
        }
        #endregion

        #region 批量文件上传--含参数--异步方法
        /// <summary>
        /// 批量文件上传--含参数--异步方法
        /// </summary>
        /// <param name="action"></param>
        /// <param name="filePaths"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> TryUploadAsync(string action, Dictionary<string, string> filePaths, Dictionary<string, string> param)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    //基地址/域名
                    client.BaseAddress = new Uri(BaseUri);
                    foreach (var filePath in filePaths)
                    {
                        var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath.Key));
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = filePath.Value };
                        content.Add(fileContent);
                    }
                    foreach (var pair in param)
                    {
                        var dataContent = new ByteArrayContent(Encoding.UTF8.GetBytes(pair.Value));
                        dataContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { Name = pair.Key };
                        content.Add(dataContent);
                    }
                    HttpResponseMessage response = await client.PostAsync(action, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return await Task.FromResult("");
                    }
                }

            }
        }
        #endregion

        #region 批量文件上传--含参数--同步方法
        /// <summary>
        /// 批量文件上传--含参数--同步方法
        /// </summary>
        /// <param name="action"></param>
        /// <param name="filePaths"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string TryUpload(string action, Dictionary<string, string> filePaths, Dictionary<string, string> param)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    //基地址/域名
                    client.BaseAddress = new Uri(BaseUri);
                    foreach (var filePath in filePaths)
                    {
                        var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath.Key));
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = filePath.Value };
                        content.Add(fileContent);
                    }
                    foreach (var pair in param)
                    {
                        var dataContent = new ByteArrayContent(Encoding.UTF8.GetBytes(pair.Value));
                        dataContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { Name = pair.Key };
                        content.Add(dataContent);
                    }
                    HttpResponseMessage response = client.PostAsync(action, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return "";
                    }
                }

            }
        }
        #endregion

        #region 批量下载--同步方法
        /// <summary>
        /// 批量下载--同步方法
        /// </summary>
        /// <param name="action">接口路径</param>
        /// <param name="savePath">文件保存路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static List<string> TryDownload(string action, string savePath, List<string> fileName)
        {
            using (HttpClient client = new HttpClient())
            {
                //基地址/域名
                client.BaseAddress = new Uri(BaseUri);
                //设定传输格式为json
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                List<string> results = new List<string>();
                HttpResponseMessage response = client.PostAsync(action, fileName, new JsonMediaTypeFormatter()).Result;
                if (response.IsSuccessStatusCode)
                {
                    MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(savePath);
                    response.Content.ReadAsMultipartAsync(provider).ContinueWith(t =>
                    {
                        foreach (MultipartFileData file in provider.FileData)
                        {
                            string tmpName = file.Headers.ContentDisposition.FileName;
                            FileInfo fileInfo = new FileInfo(file.LocalFileName);
                            fileInfo.CopyTo(Path.Combine(savePath, tmpName), true);
                            fileInfo.Delete();
                            results.Add(Path.Combine(savePath, tmpName));
                        }
                        return results;
                    });

                }
                return results;
            }
        }
        #endregion

        #region 获取HttpResponseMessage文件信息
        /// <summary>
        /// 获取HttpResponseMessage文件信息
        /// </summary>
        /// <param name="action">接口路径</param>    
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> TryAsyncResponse(string action, List<string> fileName)
        {
            using (HttpClient client = new HttpClient())
            {
                //基地址/域名
                client.BaseAddress = new Uri(BaseUri);
                //设定传输格式为json
                List<string> results = new List<string>();
                HttpResponseMessage response = client.PostAsync(action, fileName, new JsonMediaTypeFormatter()).Result;
                return Task.FromResult(response);
            }
        }
        #endregion

    }
}
