// import React from "react";
// import renderer from "react-test-renderer";
// import { Provider } from "react-redux";
// import { MemoryRouter } from "react-router-dom";
// import Scoreboard from "./Scoreboard";
// import { ChallengesState } from "store/root/challenge/types";

it("renders without crashing", () => {
  // //Arrange
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
  //       participants: [
  //         { id: "id1", challengeId: "123", score: 0, matches: [] }
  //       ],
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
  //       participants: [
  //         { id: "id2", challengeId: "456", score: 0, matches: [] }
  //       ],
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
  //       participants: [
  //         { id: "id3", challengeId: "678", score: 0, matches: [] }
  //       ],
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
  //     }
  //   ],
  //   loading: false,
  //   error: null
  // };

  // const store: any = {
  //   getState: () => {
  //     return {
  //       root: {
  //         challenge: challenges
  //       }
  //     };
  //   },
  //   dispatch: action => {},
  //   subscribe: () => {}
  // };

  // //Act
  // const tree = renderer
  //   .create(
  //     <Provider store={store}>
  //       <MemoryRouter>
  //         <Scoreboard />
  //       </MemoryRouter>
  //     </Provider>
  //   )
  //   .toJSON();

  // //Assert
  // expect(tree).toMatchSnapshot();
});
