using MongoDB.Bson;

namespace CreativeMinds.MongoDBDocumentLocking {

	public interface ILockableDocument : ILockableDocument<ObjectId> { }

	public interface ILockableDocument<TLockId> {
		ObjectId Id { get; set; }
		TLockId LockId { get; set; }
	}
}
