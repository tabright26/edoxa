import React from "react";
import Delete from ".";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { GAME_LEAGUEOFLEGENDS } from "types/games";
import store from "store";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={store}>
        <Delete game={GAME_LEAGUEOFLEGENDS} handleCancel={() => {}} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
