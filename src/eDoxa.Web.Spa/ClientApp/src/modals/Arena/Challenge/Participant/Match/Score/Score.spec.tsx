import React from "react";
import Score from "./Score";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MATCH_SCORE_MODAL } from "modals";

it("renders correctly", () => {
  const store: any = {
    getState: () => {
      return {
        modal: {
          name: MATCH_SCORE_MODAL
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
