import React from "react";
import renderer from "react-test-renderer";
import Forgot from ".";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";

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
        <MemoryRouter>
          <Forgot />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
