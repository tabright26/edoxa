// Filename: AssemblyInfo.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.CompilerServices;

// Required by Moq.
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

[assembly: InternalsVisibleTo("eDoxa.Arena.Challenges.UnitTests")]
[assembly: InternalsVisibleTo("eDoxa.Arena.Challenges.IntegrationTests")]

[assembly: InternalsVisibleTo("eDoxa.FunctionalTests")]
