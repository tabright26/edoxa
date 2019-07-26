// Filename: AssemblyInfo.cs
// Date Created: 2019-07-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.VisualStudio.TestTools.UnitTesting;

#if DEBUG
[assembly: Parallelize(Scope = ExecutionScope.ClassLevel, Workers = 0)]
#else
[assembly: DoNotParallelize]
#endif