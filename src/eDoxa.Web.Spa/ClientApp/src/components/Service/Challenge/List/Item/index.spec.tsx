import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import Item from ".";
import { ChallengesState } from "store/root/challenge/types";
import { CURRENCY_TYPE_TOKEN } from "types/cashier";

it("renders without crashing", () => {
  // Arrange
  const challenges: ChallengesState = {
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
          challengeId: "456",
          entryFee: { type: "token", amount: 0 },
          prizePool: { type: "token", amount: 200000 },
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
          entryFee: { type: "token", amount: 0 },
          prizePool: { type: "token", amount: 200000 },
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
          challenge: challenges
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  const challenge = challenges.data.find(challenge => challenge.id === "123");

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Item challenge={challenge} />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
