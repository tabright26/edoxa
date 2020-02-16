import React from "react";
import Score from ".";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { CHALLENGE_MATCH_SCORE_MODAL } from "utils/modal/constants";

it("renders correctly", () => {
  const store: any = {
    getState: () => {
      return {
        modal: {
          name: CHALLENGE_MATCH_SCORE_MODAL
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Score />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
