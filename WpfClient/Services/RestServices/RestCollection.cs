using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class RestService
    {
        readonly HttpClient client;

        public RestService(string baseurl, string pingableEndpoint = "swagger")
        {
            bool isOk = false;
            do
            {
                isOk = Ping(baseurl + pingableEndpoint);
            } while (isOk == false);
            client = new HttpClient();
            Init(baseurl);
        }

        private static bool Ping(string url)
        {
            try
            {
                HttpClient hc = new();
                hc.GetAsync(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Init(string baseurl)
        {

            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
                ("application/json"));
            try
            {
                client.GetAsync("").GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException("Endpoint is not available!");
            }

        }

        public List<T> Get<T>(string endpoint)
        {
            List<T> items;
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return items;
        }

        public List<T> Get<T>(int id, string endpoint)
        {
            List<T> items;
            HttpResponseMessage response = client.GetAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return items;
        }

        public T GetSingle<T>(string endpoint)
        {
            T item;
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return item;
        }

        public T GetSingle<T>(int id, string endpoint)
        {
            T item;
            HttpResponseMessage response = client.GetAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return item;
        }
        public List<T> GetWithItem<T>(string endpoint, T item)
        {
            List<T> items;
            HttpResponseMessage response = client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return items;
        }
        public List<T> GetBulk<T>(string endpoint, int[] ids)
        {
            List<T> items;
            HttpResponseMessage response = client.PostAsJsonAsync(endpoint, ids).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return items;
        }

        public Result Post<T>(T item, string endpoint)
        {
            HttpResponseMessage response = client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            return CheckResponse(response);
        }

        public Result Delete(int id, string endpoint)
        {
            HttpResponseMessage response = client.DeleteAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();

            return CheckResponse(response);
        }

        public Result Put<T>(T item, string endpoint)
        {
            HttpResponseMessage response = client.PutAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            return CheckResponse(response);
        }

        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            List<T> items;
            HttpResponseMessage response = await client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<T>>();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return items;
        }

        public async Task<List<T>> GetAsync<T>(int id, string endpoint)
        {
            List<T> items;
            HttpResponseMessage response = await client.GetAsync(endpoint + "/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<T>>();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return items;
        }

        public async Task<T> GetSingleAsync<T>(string endpoint)
        {
            T item;
            HttpResponseMessage response = await client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<T>();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return item;
        }

        public async Task<T> GetSingleAsync<T>(int id, string endpoint)
        {
            T item;
            HttpResponseMessage response = await client.GetAsync(endpoint + "/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<T>();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return item;
        }

        public async Task<List<T>> GetWithItemAsync<T>(string endpoint, T item)
        {
            List<T> items;
            HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, item);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<T>>();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return items;
        }

        public async Task<List<T>> GetBulkAsync<T>(string endpoint, int[] ids)
        {
            List<T> items;
            HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, ids);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<T>>();
            }
            else
            {
                throw new ArgumentException(CheckResponse(response).Errors[0]);
            }
            return items;
        }

        public async Task<Result> PostAsync<T>(T item, string endpoint)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, item);

            return CheckResponse(response);
        }

        public async Task<Result> DeleteAsync(int id, string endpoint)
        {
            HttpResponseMessage response = await client.DeleteAsync(endpoint + "/" + id.ToString());

            return CheckResponse(response);
        }


        public async Task<Result> PutAsync<T>(T item, string endpoint)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(endpoint, item);

            return CheckResponse(response);
        }

        public static Result CheckResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return new Result(success: true);
            }
            else if (response.Content.Headers.ContentType?.MediaType == "application/problem+json")
            {
                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                var deserialized = JsonSerializer.Deserialize<RestExceptionInfo1>(json, jsonOptions);
                if (deserialized != null && deserialized.Errors != null)
                {
                    var errors = deserialized.Errors;
                    return new Result(success: false, errors.Select(e => e.Value[0]).ToArray());
                }
                return new Result(success: false);

            }
            else
            {
                var error = response.Content.ReadAsAsync<RestExceptionInfo2>().GetAwaiter().GetResult();
                if (error != null)
                {
                    return new Result(success: false, new string[] { error.Msg });
                }
                return new Result(success: false);
            }
        }
    }
    class Result
    {
        public bool Success { get; }
        public string[] Errors { get; }
        public Result(bool success)
        {
            Success = success;
            Errors = Array.Empty<string>();
        }
        public Result(bool success, string[] errors)
        {
            Success = success;
            Errors = errors;
        }
    }
    class RestExceptionInfo1
    {

        public RestExceptionInfo1()
        {
            Type = "";
            Title = "";
            Status = 0;
            TraceId = "";
            Errors = new();
        }
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }

    }
    class RestExceptionInfo2
    {
        public string Msg { get; set; }
        public RestExceptionInfo2()
        {
            Msg = "";
        }
    }
    class NotifyService
    {
        private readonly HubConnection conn;

        public NotifyService(string url)
        {
            conn = new HubConnectionBuilder().WithUrl(url).Build();

            conn.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await conn.StartAsync();
            };
        }

        public void AddHandler<T>(string methodname, Action<T> value)
        {
            conn.On(methodname, value);
        }

        public async void Init()
        {
            await conn.StartAsync();
        }
    }

    class RestCollection<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private readonly string endpoint;
        private readonly bool hasSignalR;

        private readonly NotifyService? notify;
        private readonly RestService rest;
        private readonly Type type;

        private List<T> items;

        public RestCollection(string baseurl, string endpoint, string? hub = null)
        {
            this.endpoint = endpoint;
            type = typeof(T);
            items = new();
            hasSignalR = hub != null;
            rest = new RestService(baseurl, endpoint);
            if (hub != null)
            {
                notify = new NotifyService(baseurl + hub);

                notify.AddHandler(type.Name + "Created", async (int id) =>
                {
                    T newItem = await rest.GetSingleAsync<T>(id, endpoint);
                    items.Add(newItem);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    });
                });

                notify.AddHandler(type.Name + "sCreated", async (int[] ids) =>
                {
                    List<T> newItems = await rest.GetBulkAsync<T>(endpoint, ids);
                    items.AddRange(newItems);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    });
                });

                notify.AddHandler(type.Name + "Deleted", (T item) =>
                {
                    var element = items.FirstOrDefault(t => t.Equals(item));
                    if (element != null)
                    {
                        items.Remove(item);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    }
                    else
                    {
                        _ = Init();
                    }
                });

                notify.AddHandler(type.Name + "Updated", (T item) =>
                {
                    _ = Init();
                });

                notify.AddHandler(type.Name + "sUpdated", (T[] items) =>
                {
                    _ = Init();
                });

                notify.Init();
            }
            _ = Init();
        }

        private async Task Init()
        {
            items = await rest.GetAsync<T>(endpoint);
            Application.Current.Dispatcher.Invoke(() =>
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            });
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (items != null)
            {
                return items.GetEnumerator();
            }
            else return new List<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (items != null)
            {
                return items.GetEnumerator();
            }
            else return new List<T>().GetEnumerator();
        }

        public async Task<Result> Add(T item)
        {
            if (hasSignalR)
            {
                return await rest.PostAsync(item, endpoint);
            }
            else
            {
                var res = rest.PostAsync(item, endpoint);
                await res.ContinueWith((t) =>
                {
                    Init().ContinueWith(z =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    });
                });
                return await res;
            }
        }

        public async Task<Result> Update(T item)
        {
            if (hasSignalR)
            {
                return await rest.PutAsync(item, endpoint);
            }
            else
            {
                var res = rest.PutAsync(item, endpoint);
                await res.ContinueWith((t) =>
                {
                    Init().ContinueWith(z =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    });
                });

                return res.GetAwaiter().GetResult();
            }
        }

        public async Task<Result> Delete(int id)
        {
            if (hasSignalR)
            {
                return await rest.DeleteAsync(id, endpoint);
            }
            else
            {
                var res = rest.DeleteAsync(id, endpoint);
                await res.ContinueWith((t) =>
                {
                    Init().ContinueWith(z =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    });
                });

                return await res;
            }
        }
    }

    class RestSingle<T> : INotifyPropertyChanged where T : TableModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly string endpoint;
        private readonly bool hasSignalR;
        private int? id;

        private readonly NotifyService? notify;
        private readonly RestService rest;
        private readonly Type type;

        public T? Item { get; private set; }

        public RestSingle(string baseurl, string endpoint, string? hub = null)
        {
            this.endpoint = endpoint;
            type = typeof(T);
            hasSignalR = hub != null;
            rest = new RestService(baseurl, endpoint);
            if (hub != null)
            {
                notify = new NotifyService(baseurl + hub);
                notify.AddHandler(type.Name + "Deleted", (T item) =>
                {
                    if (Item != null && Item.Id == item.Id)
                    {
                        Item = null;
                        id = null;
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Item)));
                        });
                    }
                });
                notify.AddHandler(type.Name + "Updated", (T item) =>
                {
                    if (Item != null && Item.Id == item.Id)
                    {
                        _ = Init();
                    }
                });

                notify.AddHandler(type.Name + "sUpdated", (List<T> items) =>
                {
                    if (Item != null && items.Select(i => i.Id).Contains(Item.Id))
                    {
                        _ = Init();
                    }
                });

                notify.Init();
            }
        }

        private async Task Init()
        {
            if (id != null)
            {
                Item = await rest.GetSingleAsync<T>((int)id, endpoint);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Item)));
                });
            }
        }
        public void Setup(int id)
        {
            this.id = id;
            _ = Init();
        }

        public async Task<Result?> Update(T item)
        {
            if (id != null)
            {
                if (hasSignalR)
                {
                    return await rest.PutAsync(item, endpoint);
                }
                else
                {
                    var res = rest.PutAsync(item, endpoint);
                    await res.ContinueWith((t) =>
                    {
                        Init().ContinueWith(z =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Item)));
                            });
                        });
                    });
                    return await res;
                }
            }
            return null;
        }

        public async Task<Result?> Delete()
        {
            if (id != null)
            {
                if (hasSignalR)
                {
                    return await rest.DeleteAsync((int)id, endpoint);
                }
                else
                {
                    var res = rest.DeleteAsync((int)id, endpoint);
                    await res.ContinueWith((t) =>
                    {
                        Init().ContinueWith(z =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Item)));
                            });
                        });
                    });
                    return await res;
                }
            }
            return null;
        }
    }
}