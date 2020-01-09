import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import Item from "./Item";
import { ChallengesState } from "store/root/challenge/types";
import {
  CHALLENGE_STATE_ENDED,
  CURRENCY_MONEY,
  GAME_LEAGUE_OF_LEGENDS
} from "types";

it("renders without crashing", () => {
  //Arrange
  const challenges: ChallengesState = {
    data: [
      {
        id: "123",
        name: "challengeName",
        game: GAME_LEAGUE_OF_LEGENDS,
        state: CHALLENGE_STATE_ENDED,
        bestOf: 3,
        entries: 100,
        payoutEntries: 50,
        scoring: new Map<string, string>(),
        timeline: {
          createdAt: 0,
          startedAt: 0,
          endedAt: 0
        },
        payout: {
          challengeId: "789",
          entryFee: { amount: 10, currency: CURRENCY_MONEY },
          prizePool: {
            amount: 10000,
            currency: CURRENCY_MONEY
          },
          buckets: []
        },
        participants: [
          {
            id: "id1",
            challengeId: "challengeId",
            score: 20,
            user: { id: "userId", doxatag: { name: "test", code: 123 } },
            matches: [
              {
                id: "3",
                score: 20,
                participantId: "participantId",
                challengeId: "challengeId",
                stats: []
              }
            ]
          },
          {
            id: "id2",
            challengeId: "challengeId",
            score: 20,
            user: { id: "userId", doxatag: { name: "test", code: 123 } },
            matches: [
              {
                id: "3",
                score: 20,
                participantId: "participantId",
                challengeId: "challengeId",
                stats: []
              }
            ]
          },
          {
            id: "id3",
            challengeId: "challengeId",
            score: 20,
            user: { id: "userId", doxatag: { name: "test", code: 123 } },
            matches: [
              {
                id: "3",
                score: 20,
                participantId: "participantId",
                challengeId: "challengeId",
                stats: []
              }
            ]
          }
        ]
      },
      {
        id: "456",
        name: "challengeName",
        game: GAME_LEAGUE_OF_LEGENDS,
        state: CHALLENGE_STATE_ENDED,
        bestOf: 3,
        entries: 100,
        payoutEntries: 50,
        scoring: new Map<string, string>(),
        timeline: {
          createdAt: 0,
          startedAt: 0,
          endedAt: 0
        },
        payout: {
          challengeId: "789",
          entryFee: { amount: 10, currency: CURRENCY_MONEY },
          prizePool: {
            amount: 10000,
            currency: CURRENCY_MONEY
          },
          buckets: []
        },
        participants: [
          {
            id: "id1",
            challengeId: "challengeId",
            score: 20,
            user: { id: "userId", doxatag: { name: "test", code: 123 } },
            matches: [
              {
                id: "3",
                score: 20,
                participantId: "participantId",
                challengeId: "challengeId",
                stats: []
              }
            ]
          },
          {
            id: "id2",
            challengeId: "challengeId",
            score: 20,
            user: { id: "userId", doxatag: { name: "test", code: 123 } },
            matches: [
              {
                id: "3",
                score: 20,
                participantId: "participantId",
                challengeId: "challengeId",
                stats: []
              }
            ]
          },
          {
            id: "id3",
            challengeId: "challengeId",
            score: 20,
            user: { id: "userId", doxatag: { name: "test", code: 123 } },
            matches: [
              {
                id: "3",
                score: 20,
                participantId: "participantId",
                challengeId: "challengeId",
                stats: []
              }
            ]
          }
        ]
      },
      {
        id: "789",
        name: "challengeName",
        game: GAME_LEAGUE_OF_LEGENDS,
        state: CHALLENGE_STATE_ENDED,
        bestOf: 3,
        entries: 100,
        payoutEntries: 50,
        scoring: new Map<string, string>(),
        timeline: {
          createdAt: 0,
          startedAt: 0,
          endedAt: 0
        },
        payout: {
          challengeId: "789",
          entryFee: { amount: 10, currency: CURRENCY_MONEY },
          prizePool: {
            amount: 10000,
            currency: CURRENCY_MONEY
          },
          buckets: []
        },
        participants: [
          {
            id: "id1",
            challengeId: "challengeId",
            score: 20,
            user: { id: "userId", doxatag: { name: "test", code: 123 } },
            matches: [
              {
                id: "3",
                score: 20,
                participantId: "participantId",
                challengeId: "challengeId",
                stats: []
              }
            ]
          },
          {
            id: "id2",
            challengeId: "challengeId",
            score: 20,
            user: { id: "userId", doxatag: { name: "test", code: 123 } },
            matches: [
              {
                id: "3",
                score: 20,
                participantId: "participantId",
                challengeId: "challengeId",
                stats: []
              }
            ]
          },
          {
            id: "id3",
            challengeId: "challengeId",
            score: 20,
            user: { id: "userId", doxatag: { name: "test", code: 123 } },
            matches: [
              {
                id: "3",
                score: 20,
                participantId: "participantId",
                challengeId: "challengeId",
                stats: []
              }
            ]
          }
        ]
      }
    ],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };

  const challenge = challenges.data.find(challenge => challenge.id === "123");
  const participant = challenge.participants.find(
    participant => participant.id === "id1"
  );
  const match = participant.matches.find(match => match.id === "1");

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Item match={match} position={1} />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
