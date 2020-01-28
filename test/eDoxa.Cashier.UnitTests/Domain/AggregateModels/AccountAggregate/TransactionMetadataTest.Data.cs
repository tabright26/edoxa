// Filename: TransactionMetadataTestData.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

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
                            [TransactionMetadata.ChallengeKey] = challengeId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId,
                            [TransactionMetadata.UserKey] = userId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId,
                            [TransactionMetadata.UserKey] = userId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId,
                            [TransactionMetadata.UserKey] = userId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId,
                            [TransactionMetadata.UserKey] = userId
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
                            [TransactionMetadata.ChallengeKey] = challengeId
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId
                        },
                        new TransactionMetadata()
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId,
                            [TransactionMetadata.UserKey] = userId
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId
                        }
                    },
                    {
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId
                        },
                        new TransactionMetadata
                        {
                            [TransactionMetadata.ChallengeKey] = challengeId,
                            [TransactionMetadata.ChallengeParticipantKey] = participantId,
                            [TransactionMetadata.UserKey] = userId
                        }
                    }
                };
            }
        }
    }
}
