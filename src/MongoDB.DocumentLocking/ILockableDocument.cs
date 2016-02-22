using MongoDB.Bson;
using System;

namespace MongoDB.DocumentLocking {

	public interface ILockableDocument : ILockableDocument<ObjectId> { }

	public interface ILockableDocument<TLockId> {
		ObjectId Id { get; set; }
		TLockId LockId { get; set; }
	}
}