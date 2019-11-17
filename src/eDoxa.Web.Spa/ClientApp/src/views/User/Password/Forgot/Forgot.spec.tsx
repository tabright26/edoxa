import React from "react";
import renderer from "react-test-renderer";
import Forgot from "./Forgot";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const store: any = {
    getState: (): any => {
      return {
        oidc: {
          user: null
        },
        user: {
          account: {
            money: { balance: { available: 0, pending: 0 } },
            token: { balance: { available: 0, pending: 0 } }
          }
        }
      };
    },
    dispatch: (action: any): any => {},
    subscribe: (): any => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Forgot />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
