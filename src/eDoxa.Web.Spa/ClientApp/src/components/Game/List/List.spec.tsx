import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import List from "./List";
import { GameAccountCredentialState } from "store/root/user/game/credential/types";

it("renders without crashing", () => {
  //Arrange
  const games: GameAccountCredentialState = {
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
