using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BotRetreat.Client.Exceptions;
using BotRetreat.Client.Serializers;
using RestSharp;

namespace BotRetreat.Client.Base
{
    public class ClientBase
    {
        protected String BaseUri { get; }

        //public ClientBase(String baseUri = "http://localhost/BotRetreat.Web")
        public ClientBase(String baseUri = "http://botretreat.cloudapp.net")
        {
            BaseUri = baseUri;
        }

        public async Task<TResult> Get<TResult>(String uri, Dictionary<String, String> parameters = null)
        {
            var parameterizedUri = Parameterize(uri, parameters);
            var client = new RestClient(parameterizedUri ?? uri);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync<TResult>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return response.Data;
                case HttpStatusCode.NotFound:
                    throw new ClientException("404 NOT FOUND");
                default:
                    throw new ClientException($"StatusCode: {response.StatusCode}, StatusDescription: {response.StatusDescription}, ErrorMessage: {response.ErrorMessage}");
            }
        }

        public async Task<TResult> Post<TResult, TBody>(String uri, TBody body)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            request.AddBody(body);
            var response = await client.ExecutePostTaskAsync<TResult>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return response.Data;
                case HttpStatusCode.NotFound:
                    throw new ClientException("404 NOT FOUND");
                case HttpStatusCode.BadRequest:
                    throw new ClientException(response.Content);
                case HttpStatusCode.InternalServerError:
                    throw new ClientException(response.Content);
                default:
                    throw new ClientException($"StatusCode: {response.StatusCode}, StatusDescription: {response.StatusDescription}, ErrorMessage: {response.ErrorMessage}");
            }
        }

        public async Task<TResult> Put<TResult, TBody>(String uri, TBody body)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.PUT);
            request.AddBody(body);
            var response = await client.ExecuteTaskAsync<TResult>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }
            throw new ClientException($"StatusCode: {response.StatusCode}, StatusDescription: {response.StatusDescription}, ErrorMessage: {response.ErrorMessage}");
        }

        public async Task Delete(String uri, Guid id)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(id)] = $"{id}"
            };
            var parameterizedUri = Parameterize(uri, parameters);
            var client = new RestClient(parameterizedUri ?? uri);
            var request = new RestRequest(Method.DELETE);
            var response = await client.ExecuteTaskAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ClientException($"StatusCode: {response.StatusCode}, StatusDescription: {response.StatusDescription}, ErrorMessage: {response.ErrorMessage}");
            }
        }

        private String Parameterize(String uri, Dictionary<String, String> parameters)
        {
            return parameters?.Aggregate(uri, (current, parameter) => current.Replace($"{{{parameter.Key}}}", parameter.Value));
        }
    }
}