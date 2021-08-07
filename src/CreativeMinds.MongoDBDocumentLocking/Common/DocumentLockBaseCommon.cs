using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace CreativeMinds.MongoDBDocumentLocking.Common {

	public abstract class DocumentLockBaseCommon<TDocument> : IDisposable where TDocument : class, ILockableDocument {
		protected readonly IMongoCollection<TDocument> dataStore;
		protected TDocument lockedDocument = null;

		protected DocumentLockBaseCommon(IMongoCollection<TDocument> dataStore) {
			this.dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
		}

		public virtual TDocument Document {
			get {
				return this.lockedDocument;
			}
		}

		public virtual Boolean Locked {
			get {
				// Do we have a document? And the lock Id isn't the empty Id?
				return this.Document != null && this.Document.LockId != ObjectId.Empty;
			}
		}

		public abstract void Dispose();
	}
}
