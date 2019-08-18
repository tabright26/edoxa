// Filename: AssemblyInfo.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.VisualStudio.TestTools.UnitTesting;

#if DEBUG
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel, Workers = 0)]
#else
[assembly: DoNotParallelize]
#endif
