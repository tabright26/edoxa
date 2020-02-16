// import React from "react";
// import renderer from "react-test-renderer";
// import { Provider } from "react-redux";
// import { MemoryRouter } from "react-router-dom";
// import Item from "./Item";
// import { ChallengesState } from "store/root/challenge/types";

it("renders without crashing", () => {
  // // Arrange
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
  //       entryFee: { type: CURRENCY_TYPE_TOKEN, amount: 0 },
  //       participants: [
  //         { id: "id1", challengeId: "123", score: 0, matches: [], user: null }
  //       ],
  //       timeline: {
  //         createdAt: 123123123,
  //         startedAt: null,
  //         endedAt: null,
  //         closedAt: null
  //       },
  //       scoring: new Map<string, string>(),
  //       payout: {
  //         prizePool: { type: CURRENCY_TYPE_TOKEN, amount: 200000 },
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
  //       entryFee: { type: CURRENCY_TYPE_TOKEN, amount: 0 },
  //       participants: [],
  //       timeline: {
  //         createdAt: 123123123,
  //         startedAt: null,
  //         endedAt: null,
  //         closedAt: null
  //       },
  //       scoring: new Map<string, string>(),
  //       payout: {
  //         prizePool: { type: CURRENCY_TYPE_TOKEN, amount: 200000 },
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
  //       entryFee: { type: CURRENCY_TYPE_TOKEN, amount: 0 },
  //       participants: [],
  //       timeline: {
  //         createdAt: 123123123,
  //         startedAt: null,
  //         endedAt: null,
  //         closedAt: null
  //       },
  //       scoring: new Map<string, string>(),
  //       payout: {
  //         prizePool: { type: CURRENCY_TYPE_TOKEN, amount: 200000 },
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
  // const participant = challenges.data
  //   .find(challenge => challenge.id === "123")
  //   .participants.find(participant => participant.id === "id1");
  // // Act
  // const tree = renderer
  //   .create(
  //     <Provider store={store}>
  //       <MemoryRouter>
  //         <Item
  //           participant={participant}
  //           position={1}
  //         />
  //       </MemoryRouter>
  //     </Provider>
  //   )
  //   .toJSON();
  // // Assert
  // expect(tree).toMatchSnapshot();
});
