using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class JobLogic : IJobLogic
    {
        readonly IRepository<Job> repository;
        public JobLogic(IRepository<Job> repository)
        {
            this.repository = repository;
        }
        public void Create(Job item)
        {
            if (repository.Read(item.Id) != null) throw new ArgumentException("Job by this id already exists: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "Name" };
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Create(item);
        }

        public void Delete(int id)
        {
            Read(id);
            repository.Delete(id);
        }
        public Job Read(int id)
        {
            var result = repository.Read(id);
            if (result == null) throw new ArgumentException("Job not found by this id: " + id);
            return result;
        }

        public IQueryable<Job> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Job item)
        {
            if (item.Id == null) throw new ArgumentException("Id is required");
            if (repository.Read(item.Id) == null) throw new ArgumentException("Job not found by this id: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "Name" };
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Update(item);
        }
        public OrdersCount OrdersCount(int jobId)
        {
            var job = Read(jobId);
            var ordersCounts = new OrdersCount()
            {
                FixedWageOrderCount = job.FixedWageEmployees.Sum(e => e.Orders.Count),
                HourlyWageOrderCount = job.HourlyWageEmployees.Sum(e => e.Orders.Count)
            };
            return ordersCounts;
        }
        public Overview OverallOverview()
        {
            var overview = new Overview()
            {
                OrdersCount = repository.ReadAll().Sum(j => j.FixedWageEmployees.Sum(e => e.Orders.Count) + j.HourlyWageEmployees.Sum(e => e.Orders.Count)),
                Income = repository.ReadAll().Sum(j => j.FixedWageEmployees.Sum(e => e.Orders.Count * e.Hours * e.Wage) + j.HourlyWageEmployees.Sum(e => e.Orders.Sum(o => o.Hours) * e.Wage)),
                Hours = repository.ReadAll().Sum(j => j.FixedWageEmployees.Sum(e => e.Hours * e.Orders.Count) + j.HourlyWageEmployees.Sum(e => e.Orders.Sum(o => o.Hours)))
            };
            return overview;
        }
    }
}
