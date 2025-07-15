namespace BankApi.Repositories
{
    using BankApi.Applications.Dto;
    using BankApi.Models;
    using Couchbase;
    using Couchbase.Core.Exceptions.KeyValue;
    using Couchbase.Extensions.DependencyInjection;
    using Couchbase.KeyValue;
    using Couchbase.Query;

    public class CustomerRepositoryCB : ICustomerRepository
    {
        private readonly ICouchbaseCollection _collection;
        private readonly IBucket _bucket;

        public CustomerRepositoryCB(INamedBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucketAsync().Result;
            _collection = _bucket.DefaultCollection();
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var query = "SELECT c.* FROM `bank`.`_default`.`_default` c WHERE c.type = 'customer'";

            var result = await _bucket.Cluster.QueryAsync<Customer>(query, new QueryOptions());

            var customers = new List<Customer>();
            await foreach (var row in result.Rows)
            {
                customers.Add(row);
            }

            return customers;
        }

        public async Task<Customer?> GetAsync(string id)
        {
            try
            {
                var result = await _collection.GetAsync(id);
                return result.ContentAs<Customer>();
            }
            catch (DocumentNotFoundException)
            {
                return null;
            }
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _collection.InsertAsync(customer.Id, customer);
            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _collection.UpsertAsync(customer.Id, customer);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.RemoveAsync(id);
        }
    }
}
