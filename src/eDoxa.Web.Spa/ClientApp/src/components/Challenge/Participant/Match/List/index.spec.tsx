import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import List from ".";
import { ChallengesState } from "store/root/challenge/types";

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
        participants: [
          { id: "id1", user: null, challengeId: "123", score: 0, matches: [] }
        ],
        timeline: {
          createdAt: 123123123,
          startedAt: null,
          endedAt: null,
          closedAt: null
        },
        scoring: new Map<string, string>(),
        payout: {
          challengeId: "123",
          entryFee: { currency: "token", amount: 0 },
          prizePool: { currency: "token", amount: 200000 },
          buckets: []
        }
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

  const participant = challenges.data
    .find(challenge => challenge.id === "123")
    .participants.find(participant => participant.id === "id1");

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <List participantId="123" matches={participant.matches} />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
