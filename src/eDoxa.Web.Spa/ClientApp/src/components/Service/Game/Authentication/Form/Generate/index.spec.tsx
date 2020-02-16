import React from "react";
import Generate from ".";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { GAME_LEAGUE_OF_LEGENDS } from "types";

it("renders correctly", () => {
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Generate
          game={GAME_LEAGUE_OF_LEGENDS}
          setAuthenticationFactor={() => {}}
        />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
