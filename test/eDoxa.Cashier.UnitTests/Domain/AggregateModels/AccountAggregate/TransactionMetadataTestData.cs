// Filename: TransactionMetadataTestData.cs
// Date Created: 2019-12-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Misc;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    public sealed partial class TransactionMetadataTest
    {
        public static TheoryData<TransactionMetadata, TransactionMetadata> ValidTransactionMetadataTestData
        {
            get
            {
                var challengeId = new ChallengeId();
                var participantId = new ParticipantId();
                var userId = new UserId();

                return new TheoryData<TransactionMetadata, TransactionMetadata>
                {
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString()
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString()
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString(),
                            [TransactionMetadata.UserKey] = userId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString()
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString()
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString(),
                            [TransactionMetadata.UserKey] = userId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString()
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString(),
                            [TransactionMetadata.UserKey] = userId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString(),
                            [TransactionMetadata.UserKey] = userId.ToString()
                        }
                    }
                };
            }
        }

        public static TheoryData<TransactionMetadata, TransactionMetadata> InvalidTransactionMetadataTestData
        {
            get
            {
                var challengeId = new ChallengeId();
                var participantId = new ParticipantId();
                var userId = new UserId();

                return new TheoryData<TransactionMetadata, TransactionMetadata>
                {
                    {
                        new TransactionMetadata(), new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString()
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString()
                        },
                        new TransactionMetadata()
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString()
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString(),
                            [TransactionMetadata.UserKey] = userId.ToString()
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString()
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString()
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId.ToString(),
                            [TransactionMetadata.ChallengeParticipantKey] = participantId.ToString(),
                            [TransactionMetadata.UserKey] = userId.ToString()
                        }
                    }
                };
            }
        }
    }
}
