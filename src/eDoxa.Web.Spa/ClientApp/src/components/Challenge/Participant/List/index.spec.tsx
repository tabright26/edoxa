import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import List from ".";

it("renders without crashing", () => {
  //Arrange
  // const challenges: ChallengesState = {
  //   data: [
  //     {
  //       id: "123",
  //       name: "",
  //       state: "Inscription",
  //       bestOf: 3,
  //       entries: 10,
  //       game: "LeagueOfLegends",
  //       payoutEntries: 5,
  //       entryFee: { currency: "token", amount: 0 },
  //       participants: [],
  //       timeline: {
  //         createdAt: 123123123,
  //         startedAt: null,
  //         endedAt: null,
  //         closedAt: null
  //       },
  //       scoring: new Map<string, string>(),
  //       payout: {
  //         prizePool: { currency: "token", amount: 200000 },
  //         buckets: []
  //       }
  //     },
  //     {
  //       id: "456",
  //       name: "",
  //       state: "Inscription",
  //       bestOf: 3,
  //       entries: 10,
  //       game: "LeagueOfLegends",
  //       payoutEntries: 5,
  //       entryFee: { currency: "token", amount: 0 },
  //       participants: [],
  //       timeline: {
  //         createdAt: 123123123,
  //         startedAt: null,
  //         endedAt: null,
  //         closedAt: null
  //       },
  //       scoring: new Map<string, string>(),
  //       payout: {
  //         prizePool: { currency: "token", amount: 200000 },
  //         buckets: []
  //       }
  //     },
  //     {
  //       id: "678",
  //       name: "",
  //       state: "Inscription",
  //       bestOf: 3,
  //       entries: 10,
  //       game: "LeagueOfLegends",
  //       payoutEntries: 5,
  //       entryFee: { currency: "token", amount: 0 },
  //       participants: [],
  //       timeline: {
  //         createdAt: 123123123,
  //         startedAt: null,
  //         endedAt: null,
  //         closedAt: null
  //       },
  //       scoring: new Map<string, string>(),
  //       payout: {
  //         entryFee: { currency: "token", amount: 0 },
  //         prizePool: { currency: "token", amount: 200000 },
  //         buckets: []
  //       }
  //     }
  //   ],
  //   loading: false,
  //   error: null
  // };

  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <List />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
