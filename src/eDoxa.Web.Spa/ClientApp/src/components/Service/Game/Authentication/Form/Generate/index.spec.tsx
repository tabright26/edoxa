import React from "react";
import Generate from ".";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import store from "store";
import { GAME_LEAGUEOFLEGENDS } from "types/games";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={store}>
        <Generate
          game={GAME_LEAGUEOFLEGENDS}
          setAuthenticationFactor={() => {}}
        />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
