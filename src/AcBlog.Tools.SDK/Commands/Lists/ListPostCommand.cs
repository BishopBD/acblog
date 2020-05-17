﻿using AcBlog.Data.Models;
using AcBlog.Tools.SDK.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AcBlog.Tools.SDK.Commands
{
    public class ListPostCommand : BaseCommand<ListPostCommand.CArgument>
    {
        public override string Name => "post";

        public override string Description => "List posts.";

        public override async Task<int> Handle(CArgument argument, IConsole console, InvocationContext context, CancellationToken cancellationToken)
        {
            Workspace workspace = Program.Current();
            using var client = new HttpClient();
            await workspace.Connect(client);
            var service = workspace.Remote!.PostService;
            var list = (await service.All(cancellationToken)).ToList();
            console.Out.WriteLine($"Founded {list.Count} posts.");
            foreach (var id in list)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var item = (await service.Get(id, cancellationToken))!;
                console.Out.WriteLine(item.Title);
            }
            return 0;
        }

        public class CArgument
        {
        }
    }
}
