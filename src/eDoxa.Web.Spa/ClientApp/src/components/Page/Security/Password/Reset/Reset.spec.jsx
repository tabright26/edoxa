import React from "react";
import renderer from "react-test-renderer";
import Reset from "./Reset";
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
              }
            };
          },
          dispatch: action => {},
          subscribe: () => {}
        }}
      >
        <MemoryRouter>
          <Reset location={{ search: "?code=test" }} />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
