﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace CreativeMinds.MongoDBDocumentLocking.Sync {

	public abstract class DocumentLockBase<TDocument> : Common.DocumentLockBaseCommon<TDocument>, IDisposable where TDocument : class, ILockableDocument {

		protected DocumentLockBase(IMongoCollection<TDocument> dataStore) : base(dataStore) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="update"></param>
		/// <exception cref="NoDocumentLockedException"></exception>
		/// <returns></returns>
		public virtual TDocument Update(UpdateDefinition<TDocument> update) {
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
			this.lockedDocument = this.FindOneAndUpdate(filter, update);
			return this.lockedDocument;
		}

		protected virtual TDocument FindOneAndUpdate(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update) {
			return this.dataStore.FindOneAndUpdate<TDocument>(filter, update, new FindOneAndUpdateOptions<TDocument, TDocument> {
				ReturnDocument = ReturnDocument.After,
				IsUpsert = false
			});
		}

		protected virtual TDocument FindOneAndUpdate(Expression<Func<TDocument, Boolean>> filter, UpdateDefinition<TDocument> update) {
			return this.dataStore.FindOneAndUpdate<TDocument>(filter, update, new FindOneAndUpdateOptions<TDocument, TDocument> {
				ReturnDocument = ReturnDocument.After,
				IsUpsert = false
			});
		}

		public virtual void Release() {
			// Do we have a lock?
			if (!this.Locked) {
				// No, let's just return then, no kitties harmed!
				return;
			}

			// Let's release the document by settings lockId back to empty!
			TDocument returned = this.FindAndUpdate(Builders<TDocument>.Filter.Eq(d => d.Id, this.lockedDocument.Id), this.lockedDocument.LockId, ObjectId.Empty);
			if (!(returned != null && returned.LockId == ObjectId.Empty)) {
				// TODO:
				throw new ApplicationException("did not release?");
			}
			// We're done, we no longer have the lock!
			this.lockedDocument = null;
		}

		protected TDocument FindAndUpdate(FilterDefinition<TDocument> filter, ObjectId initialLockId, ObjectId endLockId) {
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
			return this.FindOneAndUpdate(internalFilter, update);
		}

		public override void Dispose() {
			// Do we still have a lock?
			if (this.Locked) {
				// Let's release it!
				this.Release();
			}
		}
	}
}
