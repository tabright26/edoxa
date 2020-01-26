import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Games from ".";
import { GamesState } from "store/root/game/types";
import { Game, GameOption } from "types";
import { LINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";

it("renders without crashing", () => {
  //Arrange
  const game: GamesState = {
    data: new Map<Game, GameOption>(),
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        modal: {
          name: LINK_GAME_CREDENTIAL_MODAL
        },
        root: {
          game
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
        <Games />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
