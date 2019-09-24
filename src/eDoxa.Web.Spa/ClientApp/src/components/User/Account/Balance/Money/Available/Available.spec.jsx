import React, { Suspense } from "react";
import Loading from "components/Shared/Loading";
import Available from "./Available";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider
        store={{
          getState: () => {
            return {
              user: {
                account: {
                  balance: {
                    money: {
                      available: 10,
                      pending: 10
                    }
                  }
                }
              }
            };
          },
          dispatch: action => {},
          subscribe: () => {}
        }}
      >
        <Suspense fallback={<Loading.Default />}>
          <Available />
        </Suspense>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
