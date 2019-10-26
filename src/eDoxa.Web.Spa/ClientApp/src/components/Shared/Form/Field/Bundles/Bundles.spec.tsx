import React from "react";
import renderer from "react-test-renderer";
import { Form, reduxForm } from "redux-form";
import { Provider } from "react-redux";
import Bundles from "./Bundles";
import { TOKEN, MONEY } from "types";
import { UserAccountDepositBundlesState } from "store/root/user/account/deposit/bundles/types";

it("renders without crashing", () => {
  const bundles: UserAccountDepositBundlesState = {
    data: [{ amount: 100, price: 100 }],
    error: null,
    loading: false
  };

  //Arrange
  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            account: {
              deposit: {
                bundles: {
                  money: bundles
                }
              }
            }
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  const FormWrapper = () => (
    <Form>
      <Bundles currency={MONEY} bundles={bundles.data} />
    </Form>
  );

  const ReduxForm = reduxForm({ form: "TEST_FORM" })(FormWrapper);

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <ReduxForm />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});

it("renders without crashing", () => {
  const bundles: UserAccountDepositBundlesState = {
    data: [{ amount: 100, price: 100 }],
    error: null,
    loading: false
  };

  //Arrange
  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            account: {
              deposit: {
                bundles: {
                  token: bundles
                }
              }
            }
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  const FormWrapper = () => (
    <Form>
      <Bundles currency={TOKEN} bundles={bundles.data} />
    </Form>
  );

  const ReduxForm = reduxForm({ form: "TEST_FORM" })(FormWrapper);

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <ReduxForm />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
