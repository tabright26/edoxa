import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Connections from "./Connections";
import { UserGamesState } from "store/root/user/games/types";

it("renders without crashing", () => {
  //Arrange
  const games: UserGamesState = {
    data: [{ name: "League of legends", id: "accountID" }],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            games
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Connections />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
