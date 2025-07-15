using BankApi.Models;
using Couchbase;
using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.Core.IO.Transcoders;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using Newtonsoft.Json;
using System.Text;

namespace BankApi.Repositories
{
    public class AccountRepositoryCB : IAccountRepository
    {
        private readonly ICouchbaseCollection _collection;

        public AccountRepositoryCB(INamedBucketProvider bucketProvider)
        {
            var bucket = bucketProvider.GetBucketAsync().Result;
            _collection = bucket.DefaultCollection();
        }

        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };

        public async Task CreateAsync(Account account)
        {
            var json = JsonConvert.SerializeObject(account, _settings);

            await _collection.UpsertAsync(
                account.Id,
                json,
                options => options.Transcoder(new RawStringTranscoder())
            );
        }

        public async Task<Account?> GetAsync(string id)
        {
            try
            {
                var result = await _collection.GetAsync(
                    id,
                    options => options.Transcoder(new RawStringTranscoder())
                );

                var json = result.ContentAs<string>();
                var obj = JsonConvert.DeserializeObject<Account>(json, _settings);
                return obj;
            }
            catch (DocumentNotFoundException)
            {
                return null;
            }
        }

        public async Task UpdateAsync(Account account)
        {
            var json = JsonConvert.SerializeObject(account, _settings);

            await _collection.UpsertAsync(
                account.Id,
                json,
                options => options.Transcoder(new RawStringTranscoder())
            );
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.RemoveAsync(id);
        }
    }
}
