import React from "react";
import renderer from "react-test-renderer";
import Confirm from "./Confirm";
import { MemoryRouter } from "react-router-dom";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider
        store={{
          getState: () => {
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
          dispatch: action => {},
          subscribe: () => {}
        }}
      >
        <MemoryRouter>
          <Confirm location={{ search: "?code=test" }} />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
