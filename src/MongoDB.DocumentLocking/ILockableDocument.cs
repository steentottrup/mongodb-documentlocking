using MongoDB.Bson;
using System;

namespace MongoDB.DocumentLocking {
	public interface ILockableDocument {
		ObjectId Id { get; set; }
		ObjectId LockId { get; set; }
	}
}