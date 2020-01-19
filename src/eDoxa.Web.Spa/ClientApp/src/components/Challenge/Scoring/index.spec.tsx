import React from "react";
import renderer from "react-test-renderer";
import Scoring from ".";
import { ChallengesState } from "store/root/challenge/types";
import { MemoryRouter } from "react-router-dom";

it("renders without crashing", () => {
  //Arrange
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
          challengeId: null,
          entryFee: { currency: "token", amount: 0 },
          prizePool: { currency: "token", amount: 200000 },
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
          challengeId: null,
          entryFee: { currency: "token", amount: 0 },
          prizePool: { currency: "token", amount: 200000 },
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
          entryFee: { currency: "token", amount: 0 },
          challengeId: null,
          prizePool: { currency: "token", amount: 200000 },
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

  // Arrange
  const scoring = new Map<string, string>();

  //Act
  const tree = renderer
    .create(
      <MemoryRouter>
        <Scoring scoring={scoring} />
      </MemoryRouter>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
