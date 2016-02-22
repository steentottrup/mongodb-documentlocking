using MongoDB.Driver;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MongoDB.DocumentLocking.Tests {

	public class MockCollection : IMongoCollection<TestClass> {
		public CollectionNamespace CollectionNamespace {
			get {
				throw new NotImplementedException();
			}
		}

		public IMongoDatabase Database {
			get {
				throw new NotImplementedException();
			}
		}

		public IBsonSerializer<TestClass> DocumentSerializer {
			get {
				throw new NotImplementedException();
			}
		}

		public IMongoIndexManager<TestClass> Indexes {
			get {
				throw new NotImplementedException();
			}
		}

		public MongoCollectionSettings Settings {
			get {
				throw new NotImplementedException();
			}
		}

		public IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<TestClass, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<TestClass, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public BulkWriteResult<TestClass> BulkWrite(IEnumerable<WriteModel<TestClass>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<BulkWriteResult<TestClass>> BulkWriteAsync(IEnumerable<WriteModel<TestClass>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public long Count(FilterDefinition<TestClass> filter, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<long> CountAsync(FilterDefinition<TestClass> filter, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public DeleteResult DeleteMany(FilterDefinition<TestClass> filter, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<DeleteResult> DeleteManyAsync(FilterDefinition<TestClass> filter, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public DeleteResult DeleteOne(FilterDefinition<TestClass> filter, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<DeleteResult> DeleteOneAsync(FilterDefinition<TestClass> filter, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public IAsyncCursor<TField> Distinct<TField>(FieldDefinition<TestClass, TField> field, FilterDefinition<TestClass> filter, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<TestClass, TField> field, FilterDefinition<TestClass> filter, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<TestClass> filter, FindOptions<TestClass, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public TProjection FindOneAndDelete<TProjection>(FilterDefinition<TestClass> filter, FindOneAndDeleteOptions<TestClass, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<TestClass> filter, FindOneAndDeleteOptions<TestClass, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public TProjection FindOneAndReplace<TProjection>(FilterDefinition<TestClass> filter, TestClass replacement, FindOneAndReplaceOptions<TestClass, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<TestClass> filter, TestClass replacement, FindOneAndReplaceOptions<TestClass, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public TProjection FindOneAndUpdate<TProjection>(FilterDefinition<TestClass> filter, UpdateDefinition<TestClass> update, FindOneAndUpdateOptions<TestClass, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<TestClass> filter, UpdateDefinition<TestClass> update, FindOneAndUpdateOptions<TestClass, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<TestClass> filter, FindOptions<TestClass, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public void InsertMany(IEnumerable<TestClass> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task InsertManyAsync(IEnumerable<TestClass> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public void InsertOne(TestClass document, InsertOneOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task InsertOneAsync(TestClass document, CancellationToken _cancellationToken) {
			throw new NotImplementedException();
		}

		public Task InsertOneAsync(TestClass document, InsertOneOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TestClass, TResult> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TestClass, TResult> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>() where TDerivedDocument : TestClass {
			throw new NotImplementedException();
		}

		public ReplaceOneResult ReplaceOne(FilterDefinition<TestClass> filter, TestClass replacement, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TestClass> filter, TestClass replacement, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public UpdateResult UpdateMany(FilterDefinition<TestClass> filter, UpdateDefinition<TestClass> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<UpdateResult> UpdateManyAsync(FilterDefinition<TestClass> filter, UpdateDefinition<TestClass> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public UpdateResult UpdateOne(FilterDefinition<TestClass> filter, UpdateDefinition<TestClass> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public Task<UpdateResult> UpdateOneAsync(FilterDefinition<TestClass> filter, UpdateDefinition<TestClass> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) {
			throw new NotImplementedException();
		}

		public IMongoCollection<TestClass> WithReadConcern(ReadConcern readConcern) {
			throw new NotImplementedException();
		}

		public IMongoCollection<TestClass> WithReadPreference(ReadPreference readPreference) {
			throw new NotImplementedException();
		}

		public IMongoCollection<TestClass> WithWriteConcern(WriteConcern writeConcern) {
			throw new NotImplementedException();
		}
	}
}
