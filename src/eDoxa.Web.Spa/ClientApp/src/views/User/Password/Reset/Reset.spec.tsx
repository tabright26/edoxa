import React from "react";
import renderer from "react-test-renderer";
import Reset from "./Reset";
import { MemoryRouter } from "react-router-dom";
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
      <Provider
        store={store}
      >
        <MemoryRouter>
          <Reset location={{ search: "?code=test" }} />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
