import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Challenges from ".";
import { ChallengesState } from "store/root/challenge/types";
import { CURRENCY_TYPE_TOKEN } from "types";
import { MemoryRouter } from "react-router-dom";

it("renders without crashing", () => {
  // Arrange
  const challenge: ChallengesState = {
    data: [
      {
        id: "123",
        name: "",
        state: "Inscription",
        bestOf: 3,
        entries: 10,
        game: "LeagueOfLegends",
        payoutEntries: 5,
        participants: [],
        timeline: {
          createdAt: 123123123,
          startedAt: null,
          endedAt: null,
          closedAt: null
        },
        scoring: new Map<string, string>(),
        payout: {
          challengeId: "123",
          entryFee: { type: CURRENCY_TYPE_TOKEN, amount: 0 },
          prizePool: { type: CURRENCY_TYPE_TOKEN, amount: 200000 },
          buckets: []
        }
      },
      {
        id: "456",
        name: "",
        state: "Inscription",
        bestOf: 3,
        entries: 10,
        game: "LeagueOfLegends",
        participants: [],
        timeline: {
          duration: 84000,
          createdAt: 123123123,
          startedAt: null,
          endedAt: null,
          closedAt: null
        },
        scoring: new Map<string, string>(),
        payout: {
          challengeId: "456",
          entries: 10,
          entryFee: { type: CURRENCY_TYPE_TOKEN, amount: 0 },
          prizePool: { type: CURRENCY_TYPE_TOKEN, amount: 200000 },
          buckets: []
        }
      },
      {
        id: "678",
        name: "",
        state: "Inscription",
        bestOf: 3,
        entries: 10,
        game: "LeagueOfLegends",
        payoutEntries: 5,
        participants: [],
        timeline: {
          createdAt: 123123123,
          startedAt: null,
          endedAt: null,
          closedAt: null
        },
        scoring: new Map<string, string>(),
        payout: {
          challengeId: "678",
          entryFee: { type: CURRENCY_TYPE_TOKEN, amount: 0 },
          prizePool: { type: CURRENCY_TYPE_TOKEN, amount: 200000 },
          buckets: []
        }
      }
    ],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          challenge
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Challenges />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
