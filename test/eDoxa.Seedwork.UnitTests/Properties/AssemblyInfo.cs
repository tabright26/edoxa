// Filename: AssemblyInfo.cs
// Date Created: 2019-07-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

// TODO: This test set does not comply with the eDoxa test conventions.

using Microsoft.VisualStudio.TestTools.UnitTesting;

#if DEBUG
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel, Workers = 0)]
#else
[assembly: DoNotParallelize]
#endif