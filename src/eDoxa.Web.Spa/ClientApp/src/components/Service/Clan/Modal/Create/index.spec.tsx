import React from "react";
import Create from ".";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { CREATE_CLAN_MODAL } from "utils/modal/constants";

it("renders correctly", () => {
  const store: any = {
    getState: () => {
      return {
        modal: {
          name: CREATE_CLAN_MODAL
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Create />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
