using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace MongoDB.DocumentLocking {

	public class DocumentLock<TDocument> : DocumentLockBase<TDocument> where TDocument : class, ILockableDocument {

		public DocumentLock(IMongoCollection<TDocument> dataStore, ObjectId id)
			: this(dataStore, Builders<TDocument>.Filter.Eq(d => d.Id, id)) {
		}

		public DocumentLock(IMongoCollection<TDocument> dataStore, FilterDefinition<TDocument> filter) : base(dataStore) {
			// Let's lock the document! Empty ObjectId is the "unlocked" indicator.
			this.lockedDocument = this.FindAndUpdate(filter, ObjectId.Empty, ObjectId.GenerateNewId());
		}
	}
}
