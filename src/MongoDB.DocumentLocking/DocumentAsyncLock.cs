using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace MongoDB.DocumentLocking {

	public class DocumentAsyncLock<TDocument> : DocumentLockBase<TDocument> where TDocument : class, ILockableDocument {

		public DocumentAsyncLock(IMongoCollection<TDocument> dataStore) : base(dataStore) { }

		public async Task<Boolean> Lock(FilterDefinition<TDocument> filter, Int32 attempts, Int32 delay) {
			Boolean done = false;
			Int32 count = 0;
			// Are we done yet??
			while (!done && count < attempts) {
				// Lock the document, get the (unlocked) document.
				this.lockedDocument = this.FindAndUpdate(filter, ObjectId.Empty, ObjectId.GenerateNewId());
				// Did we lock it?
				done = this.Locked;
				if (!done) {
					// Nope, let's wait then, and try again!
					await Task.Delay(delay);
				}

				count++;
			}

			return done;
		}

		public Task<Boolean> Lock(FilterDefinition<TDocument> filter) {
			return this.Lock(filter, 3, 100);
		}

		public Task<Boolean> Lock(ObjectId id) {
			return this.Lock(id, 3, 100);
		}

		public Task<Boolean> Lock(ObjectId id, Int32 attempts, Int32 delay) {
			return this.Lock(Builders<TDocument>.Filter.Eq(d => d.Id, id), attempts, delay);
		}
	}
}
