using CreativeMinds.MongoDBDocumentLocking;
using CreativeMinds.MongoDBDocumentLocking.Sync;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using Xunit;

namespace CreativeMinds.Tests.MongoDBDocumentLocking {

	public class SyncTests {

		[Fact]
		public void ObtainLock() {
			IMongoCollection<TestClass> collection = Common.GetCollection();
			String originalName = "Original Name";
			TestClass initialObj = new TestClass {
				LockId = ObjectId.Empty,
				Name = originalName
			};

			collection.InsertOne(initialObj);
			ObjectId id = initialObj.Id;

			TestClass obj = null;
			using (DocumentLock<TestClass> docLock = new DocumentLock<TestClass>(collection, id)) {
				docLock.Locked.Should().Be(true, "Newly created object, only this code accessing it");
				docLock.Document.Name.Should().Be(originalName);
				docLock.Document.Id.Should().Be(id);
				docLock.Document.LockId.Should().NotBe(ObjectId.Empty);

				obj = docLock.Document;
				docLock.Release();
			}

			obj = collection.Find(c => c.Id == id).Single();
			obj.LockId.Should().Be(ObjectId.Empty);
		}

		[Fact]
		public void ObtainLockAmongMultiple() {
			IMongoCollection<TestClass> collection = Common.GetCollection();
			String originalName = "Original Name";
			TestClass initialObj = new TestClass {
				LockId = ObjectId.Empty,
				Name = originalName
			};
			collection.InsertOne(initialObj);
			ObjectId id = initialObj.Id;

			initialObj = new TestClass {
				LockId = ObjectId.Empty,
				Name = originalName
			};
			collection.InsertOne(initialObj);

			initialObj = new TestClass {
				LockId = ObjectId.Empty,
				Name = originalName
			};
			collection.InsertOne(initialObj);

			initialObj = new TestClass {
				LockId = ObjectId.Empty,
				Name = originalName
			};
			collection.InsertOne(initialObj);

			TestClass obj = null;
			using (DocumentLock<TestClass> docLock = new DocumentLock<TestClass>(collection, id)) {
				docLock.Locked.Should().Be(true, "Newly created object, only this code accessing it");
				docLock.Release();
			}

			obj = collection.Find(c => c.Id == id).Single();
			obj.LockId.Should().Be(ObjectId.Empty);
		}

		[Fact]
		public void NoMatch() {
			IMongoCollection<TestClass> collection = Common.GetCollection();

			ObjectId id = ObjectId.GenerateNewId();

			using (DocumentLock<TestClass> docLock = new DocumentLock<TestClass>(collection, id)) {
				docLock.Locked.Should().Be(false, "No object in the database/no document with that id");
				docLock.Release();
			}
		}

		[Fact]
		public void MatchWithoutId() {
			IMongoCollection<TestClass> collection = Common.GetCollection();
			String originalName = "Original Name";
			TestClass initialObj = new TestClass {
				LockId = ObjectId.Empty,
				Name = originalName
			};
			collection.InsertOne(initialObj);
			ObjectId id = initialObj.Id;

			String matchName = "Special Name";
			initialObj = new TestClass {
				LockId = ObjectId.Empty,
				Name = matchName
			};
			collection.InsertOne(initialObj);

			FilterDefinition<TestClass> filter = Builders<TestClass>
				.Filter
				.Eq(c => c.Name, matchName);

			using (DocumentLock<TestClass> docLock = new DocumentLock<TestClass>(collection, filter)) {
				docLock.Locked.Should().Be(true, "Matched on name");
				docLock.Release();
			}
		}

		[Fact]
		public void LockAlreadyLocked() {
			IMongoCollection<TestClass> collection = Common.GetCollection();
			String originalName = "Original Name";
			TestClass initialObj = new TestClass {
				LockId = ObjectId.Empty,
				Name = originalName
			};
			collection.InsertOne(initialObj);
			ObjectId id = initialObj.Id;

			using (DocumentLock<TestClass> docLock = new DocumentLock<TestClass>(collection, id)) {
				docLock.Locked.Should().Be(true, "Only document");

				using (DocumentLock<TestClass> docLock2 = new DocumentLock<TestClass>(collection, id)) {
					docLock2.Locked.Should().Be(false, "already locked");

					UpdateDefinition<TestClass> update = Builders<TestClass>
						.Update
						.Set(d => d.Name, "New name");

					Action action = () => docLock2.Update(update);
					action.Should().Throw<NoDocumentLockedException>();
				}

				docLock.Release();
			}
		}
	}
}
