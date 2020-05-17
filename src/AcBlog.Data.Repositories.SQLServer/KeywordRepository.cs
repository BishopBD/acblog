﻿using AcBlog.Data.Models;
using AcBlog.Data.Models.Actions;
using AcBlog.Data.Repositories.SQLServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AcBlog.Data.Repositories.SQLServer
{
    public class KeywordRepository : IKeywordRepository
    {
        public KeywordRepository(DataContext data)
        {
            Data = data;
        }

        DataContext Data { get; set; }

        public RepositoryAccessContext Context { get; set; }

        public async Task<IEnumerable<string>> All(CancellationToken cancellationToken = default) => await Data.Keywords.Select(x => x.Id).ToArrayAsync(cancellationToken);

        public Task<bool> CanRead(CancellationToken cancellationToken = default) => Task.FromResult(true);

        public Task<bool> CanWrite(CancellationToken cancellationToken = default) => Task.FromResult(true);

        public async Task<string> Create(Keyword value, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(value.Id))
                value.Id = Guid.NewGuid().ToString();
            Data.Keywords.Add(value);
            await Data.SaveChangesAsync(cancellationToken);
            return value.Id;
        }

        public async Task<bool> Delete(string id, CancellationToken cancellationToken = default)
        {
            var item = await Data.Keywords.FindAsync(new object[] { id }, cancellationToken);
            if (item == null)
                return false;
            Data.Keywords.Remove(item);
            await Data.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Exists(string id, CancellationToken cancellationToken = default)
        {
            var item = await Data.Keywords.FindAsync(new object[] { id }, cancellationToken);
            return item != null;
        }

        public async Task<Keyword> Get(string id, CancellationToken cancellationToken = default)
        {
            var item = await Data.Keywords.FindAsync(new object[] { id }, cancellationToken);
            return item;
        }

        public async Task<QueryResponse<string>> Query(KeywordQueryRequest query, CancellationToken cancellationToken = default)
        {
            var qr = Data.Keywords.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
                qr = qr.Where(x => x.Name.Contains(query.Name));

            Pagination pagination = new Pagination
            {
                TotalCount = await qr.CountAsync(cancellationToken),
            };

            if (query.Pagination != null)
            {
                qr = qr.Skip(query.Pagination.Offset).Take(query.Pagination.CountPerPage);
                pagination.PageNumber = query.Pagination.PageNumber;
                pagination.CountPerPage = query.Pagination.CountPerPage;
            }
            else
            {
                pagination.PageNumber = 0;
                pagination.CountPerPage = pagination.TotalCount;
            }

            return new QueryResponse<string>(await qr.Select(x => x.Id).ToArrayAsync(cancellationToken), pagination);
        }

        public async Task<bool> Update(Keyword value, CancellationToken cancellationToken = default)
        {
            var to = value;

            var item = await Data.Keywords.FindAsync(new object[] { to.Id }, cancellationToken);
            if (item == null)
                return false;

            item.Name = to.Name;

            Data.Keywords.Update(item);
            await Data.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
