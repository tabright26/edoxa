import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import BankAccount from "./BankAccount";
import { StripeBankAccountState } from "store/root/payment/stripe/bankAccount/types";

it("renders without crashing", () => {
  //Arrange
  const bankAccount: StripeBankAccountState = {
    data: {
      bankName: "RBC",
      country: "Canada",
      currency: "CAD",
      last4: "4242",
      status: "verified",
      defaultForCurrency: true
    },
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          payment: {
            stripe: {
              bankAccount
            }
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <BankAccount />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
