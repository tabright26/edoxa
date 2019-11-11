import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import List from "./List";
import { GameCredentialsState } from "store/root/user/games/types";

it("renders without crashing", () => {
  //Arrange
  const games: GameCredentialsState = {
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
        <List />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
