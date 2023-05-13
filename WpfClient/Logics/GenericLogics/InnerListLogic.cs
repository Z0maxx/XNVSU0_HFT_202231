using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics
{
    abstract class InnerListLogic<T, S> : IInnerListLogic<T, S> where T : TableModel where S : TableModel
    {
        protected readonly ILiveRestService<T> liveRestService;
        private readonly ISingleService<S> singleService1;
        protected readonly PropertyInfo?[] propInfos;
        public ObservableCollection<S>? List1 { get; private set; }
        public InnerListLogic(ILiveRestService<T> liveRestService, ISingleService<S> singleService1, string[] propNames)
        {
            this.liveRestService = liveRestService;
            this.singleService1 = singleService1;

            liveRestService.Single.PropertyChanged += (_, _) => SetInnerList();
            propInfos = new PropertyInfo[propNames.Length];
            List1 = new ObservableCollection<S>();
            for (int i = 0; i < propNames.Length; i++)
            {
                propInfos[i] = typeof(T).GetProperty(propNames[i]);
            }
        }
        public void Setup(T item)
        {
            if (item.Id != null)
            {
                liveRestService.Single.Setup((int)item.Id);
            }
        }

        public void Details(S item)
        {
            singleService1.Single(item);
        }
        public virtual void SetInnerList()
        {
            T? item = liveRestService.Single.Item;
            if (item != null && propInfos.Length > 0)
            {
                if (propInfos[^1]?.GetValue(item) is IEnumerable<S> list)
                {
                    List1?.Clear();
                    foreach (S s in list)
                    {
                        List1?.Add(s);
                    }
                }
            }
        }
    }

    abstract class InnerListLogic<T, S, U> : InnerListLogic<T, U>, IInnerListLogic<T, S, U> where T : TableModel where S : TableModel where U : TableModel
    {
        private readonly ISingleService<S> singleService2;
        public ObservableCollection<S>? List2 { get; private set; }
        public InnerListLogic(ILiveRestService<T> liveRestService, ISingleService<S> singleService2, ISingleService<U> singleService1, string[] propNames) :
            base(liveRestService, singleService1, propNames)
        {
            this.singleService2 = singleService2;
            List2 = new();
        }

        public void Details(S item)
        {
            singleService2.Single(item);
        }
        public override void SetInnerList()
        {
            T? item = liveRestService.Single.Item;
            if (item != null && propInfos.Length > 1)
            {
                if (propInfos[^2] != null && propInfos[^2]?.GetValue(item) is IEnumerable<S> list)
                {
                    List2?.Clear();
                    foreach (S s in list)
                    {
                        List2?.Add(s);
                    }
                }
            }
            base.SetInnerList();
        }
    }
}
