using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CreativeMinds.MongoDBDocumentLocking.Async {

	public abstract class DocumentLockBase<TDocument> : Sync.DocumentLockBase<TDocument> where TDocument : class, ILockableDocument {

		protected DocumentLockBase(IMongoCollection<TDocument> dataStore) : base(dataStore) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="update"></param>
		/// <exception cref="NoDocumentLockedException"></exception>
		/// <returns></returns>
		public virtual async Task<TDocument> Update(UpdateDefinition<TDocument> update, CancellationToken cancellationToken) {
			// Do we have a document?
			if (!this.Locked) {
				throw new NoDocumentLockedException();
			}

			// Let's make sure we get the right, locked document
			FilterDefinition<TDocument> filter = Builders<TDocument>
				.Filter
				.And(
					Builders<TDocument>.Filter.Eq(d => d.Id, this.lockedDocument.Id),
					Builders<TDocument>.Filter.Eq(d => d.LockId, this.lockedDocument.LockId)
				);

			// Find the document, and update with the user's update definition.
			this.lockedDocument = await this.FindOneAndUpdate(filter, update, cancellationToken);
			return this.lockedDocument;
		}

		protected virtual Task<TDocument> FindOneAndUpdate(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, CancellationToken cancellationToken) {
			return this.dataStore.FindOneAndUpdateAsync<TDocument>(filter, update, new FindOneAndUpdateOptions<TDocument, TDocument> {
				ReturnDocument = ReturnDocument.After,
				IsUpsert = false
			}, cancellationToken);
		}

		protected virtual Task<TDocument> FindOneAndUpdate(Expression<Func<TDocument, Boolean>> filter, UpdateDefinition<TDocument> update, CancellationToken cancellationToken) {
			return this.dataStore.FindOneAndUpdateAsync<TDocument>(filter, update, new FindOneAndUpdateOptions<TDocument, TDocument> {
				ReturnDocument = ReturnDocument.After,
				IsUpsert = false
			}, cancellationToken);
		}

		public virtual async Task Release(CancellationToken cancellationToken) {
			// Do we have a lock?
			if (!this.Locked) {
				// No, let's just return then, no kitties harmed!
				return;
			}

			// Let's release the document but settings lockId back to empty!
			TDocument returned = await this.FindAndUpdate(Builders<TDocument>.Filter.Eq(d => d.Id, this.lockedDocument.Id), this.lockedDocument.LockId, ObjectId.Empty, cancellationToken);
			if (!(returned != null && returned.LockId == ObjectId.Empty)) {
				// TODO:
				throw new ApplicationException("did not release?");
			}
			// We're done, we no longer have the lock!
			this.lockedDocument = null;
		}

		protected Task<TDocument> FindAndUpdate(FilterDefinition<TDocument> filter, ObjectId initialLockId, ObjectId endLockId, CancellationToken cancellationToken) {
			// We need a filter to make sure we get the document with the correct lock Id.
			FilterDefinition<TDocument> lockIdFilter = Builders<TDocument>
				.Filter
				.Eq(d => d.LockId, initialLockId);

			// And then we need to combine that with whatever filter the user wants applied.
			FilterDefinition<TDocument> internalFilter = Builders<TDocument>
				.Filter
				.And(filter, lockIdFilter);

			// And if we get the document, we'll update it with the new lock id.
			UpdateDefinition<TDocument> update = Builders<TDocument>
				.Update
				.Set(d => d.LockId, endLockId);

			// Update and return!
			return this.FindOneAndUpdate(internalFilter, update, cancellationToken);
		}

		public override void Dispose() {
			// Do we still have a lock?
			if (this.Locked) {
				// Let's release it!
				this.Release(CancellationToken.None).Wait();
			}
		}
	}
}
