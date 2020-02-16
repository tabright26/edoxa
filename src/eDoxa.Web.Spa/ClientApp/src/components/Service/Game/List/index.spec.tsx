// import React from "react";
// import renderer from "react-test-renderer";
// import { Provider } from "react-redux";
// import List from "./List";
// import { GamesState } from "store/root/game/types";
// import { GameOption, Game, GameServiceName } from "types";

it("renders without crashing", () => {
  // // Arrange
  // var data = new Map<Game, GameOption>();
  // const option: GameOption = {
  //   name: "LeagueOfLegends",
  //   displayName: "League of legends",
  //   displayed: true,
  //   verified: true,
  //   services: new Map<GameServiceName, boolean>()
  // };
  // data.set("LeagueOfLegends", option);

  // const games: GamesState = {
  //   data: data,
  //   loading: false,
  //   error: null
  // };

  // const store: any = {
  //   getState: () => {
  //     return {
  //       root: {
  //         user: {
  //           game: games
  //         }
  //       }
  //     };
  //   },
  //   dispatch: action => {},
  //   subscribe: () => {}
  // };

  // // Act
  // const tree = renderer
  //   .create(
  //     <Provider store={store}>
  //       <List />
  //     </Provider>
  //   )
  //   .toJSON();

  // // Assert
  // expect(tree).toMatchSnapshot();
});
